using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WEB_API.Exceptions
{
    public class ConflictException : ApiExceptions
    {
        public ConflictException(string message) : base(HttpStatusCode.Conflict, message)
        {

        }
    }
}
