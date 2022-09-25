using  EmployeeCRUD.Core.DTOs.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  EmployeeCRUD.API.Filter
{
    public class ValidateFilter : ActionFilterAttribute
    {

        public ValidateFilter()
        {
            //this.stringLocalizer = stringLocalizer;
        }
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ObjectResult(new ServiceResponse(context.ModelState)) { StatusCode = StatusCodes.Status200OK };
            }
            return base.OnActionExecutionAsync(context, next);

        }
        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ObjectResult(new ServiceResponse(context.ModelState)) { StatusCode = StatusCodes.Status200OK };
            }
            return base.OnResultExecutionAsync(context, next);
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {

            base.OnResultExecuted(context);
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ObjectResult(new ServiceResponse(context.ModelState)) { StatusCode = StatusCodes.Status200OK };
            }
            base.OnResultExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ObjectResult(new ServiceResponse(context.ModelState)) { StatusCode = StatusCodes.Status200OK };
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ObjectResult(new ServiceResponse(context.ModelState)) { StatusCode = StatusCodes.Status200OK };
            }
        }
    }
}
