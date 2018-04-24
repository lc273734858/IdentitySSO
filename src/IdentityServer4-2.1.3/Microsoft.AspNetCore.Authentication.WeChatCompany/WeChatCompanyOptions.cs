using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Microsoft.AspNetCore.Authentication.WeChatCompany
{
    public class WeChatCompanyOptions : OAuthOptions
    {
        public WeChatCompanyOptions()
        {
            CallbackPath = new PathString("/signin-WeChatCompany");
            AuthorizationEndpoint = WeChatCompanyDefaults.AuthorizationEndpoint;
            TokenEndpoint = WeChatCompanyDefaults.TokenEndpoint;
            UserInformationEndpoint = WeChatCompanyDefaults.UserInformationEndpoint;
            OpenIdEndpoint = WeChatCompanyDefaults.OpenIdEndpoint;

            //StateDataFormat = 
            //Scope 表示用户授权时向用户显示的可进行授权的列表。
            //Scope.Add("snsapi_base"); //默认只请求对get_user_info进行授权
            ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "user_info:userid");
			ClaimActions.MapJsonKey(ClaimTypes.Name, "user_info:name");
            //ClaimActions.MapJsonKey(ClaimTypes.Gender, "gender");
            //ClaimActions.MapJsonKey("sub", "user_info:userid");
            //ClaimActions.MapJsonKey("usertype", "usertype");
            //ClaimActions.MapJsonKey("name", "nickname");
            //ClaimActions.MapJsonKey("urn:qq:figure", "figureurl_qq_1");
            AccessTokenEndpoint = "https://qyapi.weixin.qq.com/cgi-bin/gettoken";
        }
        public string AccessTokenEndpoint { get; set; }
        public string OpenIdEndpoint { get; }
        public string Agentid { get; set; }
        /// <summary>
        /// WeChatCompany互联 APP ID https://connect.qq.com
        /// </summary>
        public string AppId
        {
            get { return ClientId; }
            set { ClientId = value; }
        }
        /// <summary>
        /// WeChatCompany互联 APP Key https://connect.qq.com
        /// </summary>
        public string AppKey
        {
            get { return ClientSecret; }
            set { ClientSecret = value; }
        }
    }
}
