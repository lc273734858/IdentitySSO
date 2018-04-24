namespace Microsoft.AspNetCore.Authentication.WeChatCompany
{
    //http://wiki.connect.qq.com/%E5%87%86%E5%A4%87%E5%B7%A5%E4%BD%9C_oauth2-0 WeChatCompany互联 oauth2.0文档
    public static class WeChatCompanyDefaults
    {
        public const string AuthenticationScheme = "WeChatCompany";

        public static readonly string DisplayName = "企业微信";

        /// <summary>
        /// 获取Authorization Code
        /// </summary>
        public static readonly string AuthorizationEndpoint = "https://open.work.weixin.qq.com/wwopen/sso/qrConnect";

        /// <summary>
        /// 通过Authorization Code获取Access Token
        /// </summary>
        public static readonly string TokenEndpoint = "https://qyapi.weixin.qq.com/cgi-bin/gettoken";

        /// <summary>
        ///通过获取的Access Token，得到对应用户身份的OpenID
        /// </summary>
        public static readonly string OpenIdEndpoint = "https://graph.qq.com/oauth2.0/me";
        //"https://qyapi.weixin.qq.com/cgi-bin/service/get_login_info"
        /// <summary>
        ///获取到Access Token和OpenID后，可通过调用OpenAPI来获取或修改用户个人信息
        /// </summary>
        public static readonly string UserInformationEndpoint = "https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo";


    }
}
