using Host.Views.WeChat;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Host.Views.WeChat
{
    [Route("/qq/[action]")]
    public class LoginController : Controller
    {
        private static WeChatAccessModal res;
        static LoginController()
        {
            res = WeChartLogin();
        }
        [HttpGet]
        public async Task<IActionResult> Login(string state, string code)
        {
            if (string.IsNullOrEmpty(code) == false)
            {
again:
                var url = "https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token=" + res.access_token + "&code=" + code;
                var result = GetResult<ResultModal>(url);
                if (result.errcode == 40014)
                {
                    res = WeChartLogin();
                    goto again;
                }
                if (result.errmsg == "ok")
                {
                    var wechatuser = await HttpContext.AuthenticateAsync("QQ");

                    var externalUser = wechatuser.Principal;
                    var claims = externalUser.Claims.ToList();
                    // try to determine the unique id of the external user (issued by the provider)
                    // the most common claim type for that are the sub claim and the NameIdentifier
                    // depending on the external provider, some other claim type might be used
                    var userIdClaim = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject);
                    if (userIdClaim == null)
                    {
                        userIdClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                    }
                    if (userIdClaim == null)
                    {
                        throw new Exception("Unknown userid");
                    }

                    // remove the user id claim from the claims collection and move to the userId property
                    // also set the name of the external authentication provider
                    claims.Remove(userIdClaim);
                    var provider = result.Properties.Items["scheme"];
                    var userId = userIdClaim.Value;

                    // this is where custom logic would most likely be needed to match your users from the
                    // external provider's authentication result, and provision the user as you see fit.
                    // 
                    // check if the external user is already provisioned
                    var user = _users.FindByExternalProvider(provider, userId);
                    if (user == null)
                    {
                        // this sample simply auto-provisions new external user
                        // another common approach is to start a registrations workflow first
                        user = _users.AutoProvisionUser(provider, userId, claims);
                    }

                    var additionalClaims = new List<Claim>();

                    // if the external system sent a session id claim, copy it over
                    // so we can use it for single sign-out
                    var sid = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
                    if (sid != null)
                    {
                        additionalClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));
                    }

                    // if the external provider issued an id_token, we'll keep it for signout
                    AuthenticationProperties props = null;
                    var id_token = result.Properties.GetTokenValue("id_token");
                    if (id_token != null)
                    {
                        props = new AuthenticationProperties();
                        props.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = id_token } });
                    }

                    // issue authentication cookie for user
                    await _events.RaiseAsync(new UserLoginSuccessEvent(provider, userId, user.SubjectId, user.Username));
                    await HttpContext.SignInAsync(user.SubjectId, user.Username, provider, props, additionalClaims.ToArray());

                    // delete temporary cookie used during external authentication
                    await HttpContext.SignOutAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);

                    // validate return URL and redirect back to authorization endpoint or a local page
                    var returnUrl = result.Properties.Items["returnUrl"];
                    if (_interaction.IsValidReturnUrl(returnUrl) || Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                }

            }
            //40014
            return View(vm);
        }
        static private WeChatAccessModal WeChartLogin()
        {

            var loginurl = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=ww85a6abac8a4faac6&corpsecret=IEGbvmlHoC8m_XxNvYSXaaAEh0QGrbWKACavOElYG_c";
            var client = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            client.Encoding = Encoding.UTF8;
            string result = client.DownloadString(loginurl);
            var res = JsonConvert.DeserializeObject<WeChatAccessModal>(result);
            return res;
        }
        static private T GetResult<T>(string url, string contenttype = "application/x-www-form-urlencoded")
        {
            var client = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            client.Headers[HttpRequestHeader.ContentType] = contenttype;
            string result = client.DownloadString(url);
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
