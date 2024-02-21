using Application.DTO.Response;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Presentation.API.Handlers
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            MessageResponse message = new MessageResponse();

            if(context.Exception is NotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                message.Message = context.Exception.Message;
            }
            else if(context.Exception is BadRequestException||
                context.Exception is EmployeeIdFormatException) 
            {
                statusCode = HttpStatusCode.BadRequest;
                message.Message = context.Exception.Message;
                message.Errors = ((BadRequestException)context.Exception).ValidationErrors;
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
                message.Message = context.Exception.Message;
            }

            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.Result = new JsonResult(message);
        }
    }
}
