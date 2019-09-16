using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common
{
    public class AjaxJson
    {
        public int code { get; set; }

        public string message { get; set; }

        public object data { get; set; }

        public AjaxJson() { }

        public AjaxJson(int code, string message, object data)
        {
            this.code = code;
            this.message = message;
            this.data = data;
        }
    }
}
