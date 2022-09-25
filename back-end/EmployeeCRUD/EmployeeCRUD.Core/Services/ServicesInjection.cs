using EmployeeCRUD.Core.Services.Contracts;
using EmployeeCRUD.Core.Services.Fucntions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Core.Services
{
    public static class ServicesInjection
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<IJwtTokenProvider, JWTProvider>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
        }
    }
}
