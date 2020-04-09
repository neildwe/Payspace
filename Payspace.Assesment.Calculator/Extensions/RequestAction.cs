using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payspace.Assesment
{
    public class RequestAction : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // TODO : use for Full Audit Log
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            // TODO : use for Full Audit Log
        }
    }
}
