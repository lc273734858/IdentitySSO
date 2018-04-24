using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Host.Views.WeChat
{
    public class WeChatAccessModal
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string access_token { get; set; }
    }
}
