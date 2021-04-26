using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WEB_API.Exceptions
{
    public class ApiExceptions : Exception
    {
        public ApiExceptions(HttpStatusCode httpStatusCode, string message)
        {
            Code = httpStatusCode;
        }

        public HttpStatusCode Code { get; internal set; }
    }
}
