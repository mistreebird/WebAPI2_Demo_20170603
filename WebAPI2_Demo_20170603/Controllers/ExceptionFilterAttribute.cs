using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;

namespace WebAPI2_Demo_20170603.Controllers
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string msg = "Exception By WebApiExceptionFilterAttribute:"
                       + actionExecutedContext.Exception.GetBaseException().Message;
            actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, msg);

        }
    }
}