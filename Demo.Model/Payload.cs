using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Model
{
    /// <summary>
    /// 身份验证信息 模拟JWT的payload
    /// </summary>
    public class Payload
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        /// <summary>
        /// 口令过期时间
        /// </summary>
        public DateTime ExpiryDateTime { get; set; }
    }
}
