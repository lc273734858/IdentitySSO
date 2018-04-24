using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Authentication.WeChatCompany
{
    public class WeChatUserData
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string UserId { get; set; }
    }

}
