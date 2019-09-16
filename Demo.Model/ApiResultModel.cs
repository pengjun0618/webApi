using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Model
{
  public  class ApiResultModel
    {
        public  int Code { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }
    }
}
