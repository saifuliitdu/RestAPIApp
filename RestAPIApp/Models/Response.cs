using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIApp.Models
{
    public class Response
    {
        public bool IsSuccess { set; get; }
        public string Message { set; get; }
        //public object Result { get; set; }
    }
}
