using EmployeeCRUD.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Core.Services.Contracts;

public interface IJwtTokenProvider
{
    public (string Token, DateTime Expiration) GenerateJwtToken(ApplicationUser user, IList<string> Roles);
    public ClaimsPrincipal GetPrincipalFromToken(string token);
}
