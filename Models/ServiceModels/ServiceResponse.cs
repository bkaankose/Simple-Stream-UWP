using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Stream_UWP.Models.ServiceModels
{
    public class ServiceResponse
    {
        public Exception ExceptionContainer { get; set; }
        public bool IsSuccess
        {
            get
            {
                return ExceptionContainer == null ? true : false;
            }
        }
        public object Data { get; set; }
    }
}
