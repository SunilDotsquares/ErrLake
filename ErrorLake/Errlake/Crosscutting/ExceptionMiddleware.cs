using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using System.IO;

namespace Errlake.Crosscutting
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionMiddlewareHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var exception = contextFeature.Error;
                        await Task.Run(() =>
                        {
                            var errorRequest = new Error.Model.ErrorRequest(context.Request);
                            Error.Interface.IError error = new Error.Service.ErrorService();
                            error.SaveError(exception, errorRequest);
                        });
                    }
                });
            });
        }
    }
}
