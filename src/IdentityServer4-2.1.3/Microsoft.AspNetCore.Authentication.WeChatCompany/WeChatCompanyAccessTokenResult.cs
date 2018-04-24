using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Authentication.WeChatCompany
{
    public class WeChatCompanyAccessTokenResult
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }

        public string access_token { get; set; }
    }
}
