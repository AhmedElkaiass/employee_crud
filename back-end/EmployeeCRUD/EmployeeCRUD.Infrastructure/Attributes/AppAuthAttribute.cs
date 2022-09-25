using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Infrastructure.Attributes
{
    public class AppAuthAttribute : AuthorizeAttribute
    {
        public AppAuthAttribute(params string[] Roles) : base()
        {
            base.Roles = (Roles != null && Roles.Any() ? Roles.Aggregate((cur, next) => $"{cur},{next}") : null);
        }
    }
}
