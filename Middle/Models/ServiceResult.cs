using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middle.Models
{
   public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }
    }
}
