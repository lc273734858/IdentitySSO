using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Host.Models
{
    public class IdentityOptions
    {
        /// <summary>
        /// Redis连接字符串
        /// </summary>
        public string Redis { get; set; }

        /// <summary>
        /// redis的数据库
        /// </summary>
        public int DbNum { get; set; } = 2;
    }
}
