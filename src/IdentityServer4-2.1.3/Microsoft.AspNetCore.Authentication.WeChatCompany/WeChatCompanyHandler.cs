using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Authentication.WeChatCompany
{
    internal class WeChatCompanyHandler : OAuthHandler<WeChatCompanyOptions>
    {
        private ILogger logger;
        IMemoryCache cache;
        public WeChatCompanyHandler(IOptionsMonitor<WeChatCompanyOptions> options, ILoggerFactory _logger, UrlEncoder encoder, ISystemClock clock,IMemoryCache _cache)
            : base(options, _logger, encoder, clock)
        {
            logger = _logger.CreateLogger("WeChatHandler");
            cache = _cache;
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(
            ClaimsIdentity identity,
            AuthenticationProperties properties,
            OAuthTokenResponse tokens)
        {
            try
            {
                again:
                //获取用户信息
                var parameters = new Dictionary<string, string>
                {
                    {  "access_token", GetAccessToken() },
                    {  "code", tokens.Response["code"].ToString() }
                };
                //var code = tokens.Response["code"].ToString();
                //_logger.LogDebug("code : " + code);
                //var content = new StringContent("{\"auth_code\":\"" + code + "\"}");                
                // {  "auth_code", tokens.Response.GetValue("code").ToString() }
                var userInformationEndpoint = QueryHelpers.AddQueryString(Options.UserInformationEndpoint, parameters);
                logger.LogDebug("userInformationEndpoint : " + userInformationEndpoint);
                var userInformationResponse =await Backchannel.GetAsync(userInformationEndpoint);

                if (!userInformationResponse.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"未能检索WeChatCompany Connect的用户信息(返回状态码:{userInformationResponse.StatusCode})，请检查参数是否正确。");
                }

                var userinforesult = await userInformationResponse.Content.ReadAsStringAsync();
                logger.LogDebug(userinforesult);
                var payload = JObject.Parse(userinforesult);
                var userinfo = Newtonsoft.Json.JsonConvert.DeserializeObject<WeChatUserData>(userinforesult);
                logger.LogDebug("success");
                if (userinfo.errcode==40014)
                {
                    goto again;
                }
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userinfo.UserId, Options.ClaimsIssuer));
                identity.AddClaim(new Claim(ClaimTypes.Name, userinfo.UserId, Options.ClaimsIssuer));
                //identity.AddClaim(new Claim("sub", userinfo.UserId, Options.ClaimsIssuer));
                //identity.AddClaim(new Claim(ClaimTypes, payload.Value<string>("user_info:name"), Options.ClaimsIssuer));

                //payload.Last.AddAfterSelf(new JProperty("sub", userinfo.UserId));
                var context = new OAuthCreatingTicketContext(new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens, payload);
                context.RunClaimActions();

                await Events.CreatingTicket(context);
                logger.LogDebug("success2");
                logger.LogDebug("context.Principal:", Newtonsoft.Json.JsonConvert.SerializeObject(context.Principal.Claims.GetEnumerator().Current));
                logger.LogDebug("context.Properties:", Newtonsoft.Json.JsonConvert.SerializeObject(context.Properties));
                logger.LogDebug("Scheme.Name:", Newtonsoft.Json.JsonConvert.SerializeObject(Scheme.Name));
                return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
        protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
        {
            return QueryHelpers.AddQueryString(Options.AuthorizationEndpoint, new Dictionary<string, string>
            {
                ["appid"] = Options.AppId,
                //["scope"] = FormatScope(),
                ["agentid"] = Options.Agentid,
                ["redirect_uri"] = redirectUri,
                ["state"] = Options.StateDataFormat.Protect(properties),
                ["usertype"]= "member"
            });
        }
        /// <summary>
        /// 微信登录
        /// </summary>
        /// <returns></returns>
        private string GetAccessToken()
        {
            var parameters = new Dictionary<string, string>
                {
                    {  "corpid", Options.AppId },
                    {  "corpsecret", Options.AppKey }
                };

            var loginurl =QueryHelpers.AddQueryString("https://qyapi.weixin.qq.com/cgi-bin/gettoken",parameters);
            var result = Backchannel.GetAsync(loginurl).Result.Content.ReadAsStringAsync().Result;
            var res = Newtonsoft.Json.JsonConvert.DeserializeObject<WeChatCompanyAccessTokenResult>(result);
            return res.access_token;
        }
        private const string TOKENCACHEKEY= "tokencachekey";
        private string GetAccessTokenFromCache()
        {
            var token=cache.Get<string>(TOKENCACHEKEY);
            if (string.IsNullOrEmpty(token))
            {
                return RefreshToken();
            }
            return token;
        }
        private string RefreshToken()
        {
            var token = GetAccessToken();
            cache.Set(TOKENCACHEKEY,token);
            return token;
        }
        /// <summary>
        /// 通过Authorization Code获取Access Token。
        /// </summary>
        protected override async Task<OAuthTokenResponse> ExchangeCodeAsync(string code, string redirectUri)
        {
            //var parameters = new Dictionary<string, string>
            //{
            //    {  "corpsecret", Options.ClientSecret},
            //    { "corpid",Options.AppId}
            //};

            //var endpoint = QueryHelpers.AddQueryString(Options.TokenEndpoint, parameters);

            //var response = await Backchannel.GetAsync(endpoint, Context.RequestAborted);
            //if (response.IsSuccessStatusCode)
            //{
            //    var result = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine(result);
            //    logger.LogDebug("ExchangeCodeAsync : " + result);
            //    var payload = JObject.Parse(result);

            //    payload.Last.AddAfterSelf(new JProperty("code", code));
            //    logger.LogDebug("payload : " + payload.ToString());
            //    return OAuthTokenResponse.Success(payload);
            //}
            //else
            //{
            //    return OAuthTokenResponse.Failed(new Exception("获取WeChatCompany Connect Access Token出错。"));
            //}
            JObject payload = JObject.Parse("{\"access_token\":\""+GetAccessTokenFromCache()+"\",\"code\":\""+code+"\"}");
            logger.LogDebug("payload : " + payload.ToString());
            return OAuthTokenResponse.Success(payload);
        }

        private static string GetOpenId(JObject json)
        {
            return json.Value<string>("openid");
        }
    }
}
