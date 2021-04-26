using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API.Exceptions
{
    public class ApiExceptionsFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if(context.Exception is ApiExceptions apiExceptions)
            {
                context.Result = new ObjectResult(apiExceptions.Message) { StatusCode = (int)apiExceptions.Code };
            }
        }
    }
}
