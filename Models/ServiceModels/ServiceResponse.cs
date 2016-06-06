using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Stream_UWP.Models.ServiceModels
{
    public class ServiceResponse
    {
        // For exception handling purposes.
        public Exception ExceptionContainer { get; set; } 
        public bool IsSuccess
        {
            get
            {
                return ExceptionContainer == null ? true : false;
            }
        }

        // Raw json data fetched from server.
        public object Data { get; set; }
    }
}
