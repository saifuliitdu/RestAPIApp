using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIApp.Models
{
    public class GlobalResponse<T> : BaseGlobalResponse
    {
        public T Result { get; set; }
    }
    public class BaseGlobalResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ActionName { get; set; }
    }
}
