using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCRUD.Infrastructure.Configurations
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection AddConfiguredAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                //var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                //    JwtBearerDefaults.AuthenticationScheme);

                //defaultAuthorizationPolicyBuilder =
                //    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

                //options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });
            return services;
        }
    }
}
