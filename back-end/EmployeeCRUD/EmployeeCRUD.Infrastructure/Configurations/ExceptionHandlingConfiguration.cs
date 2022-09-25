
using EmployeeCRUD.Core.DTOs.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeCRUD.Infrastructure.Configurations
{
    public static class ExceptionHandlingConfiguration
    {
        public static void UseApiExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    //if any exception then report it and log it
                    if (contextFeature != null)
                    {

                        //Technical Exception for troubleshooting
                        var logger = loggerFactory.CreateLogger("GlobalException");
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        string response = "";
                        if (contextFeature.Error is DbUpdateException || contextFeature.Error is DbUpdateConcurrencyException)
                        {
                            response = JsonConvert.SerializeObject(new ServiceResponse(contextFeature.Error, GetDescriptiveDbExceptionMessage(contextFeature.Error)));
                        }
                        else
                        {
                            response = JsonConvert.SerializeObject(new ServiceResponse(contextFeature.Error, "Something went wrongs.Please try again later"));
                        }

                        //Business exception - exit gracefully
                        await context.Response.WriteAsync(response);
                    }
                });
            });
        }

        public static string GetDescriptiveDbExceptionMessage(Exception dbException)
        {
            var s1 = dbException.InnerException;
            var s2 = dbException.InnerException?.InnerException;
            if (s2 != null)
            {
                return s2.Message;
            }
            else if (s1 != null)
            {
                return s1.Message;
            }
            else if (dbException is DbUpdateException)
            {
                return "An error occured while Save Changes please contact your system administrator.";
            }
            else if (dbException is DbUpdateConcurrencyException)
            {
                return "An error occured while Concurrently confilct  when Save Changes, Please try again later.";
            }
            else
                return "Something went wrongs when save changes. Please try again later";
        }
    }
}
