using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Errlake.Crosscutting
{
    public static class RegisterError
    {
        public static IServiceCollection AddError(this IServiceCollection service)
        {
            service.AddScoped<Error.Interface.IError, Error.Service.ErrorService>();
            return service;
        }
    }
}
