using EmployeeCRUD.Core.DTOs.Common;
using EmployeeCRUD.Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Core.Services.Contracts;

public interface IAuthService
{
    Task<ServiceResponse<TokenDTO>> AuthenticateJwt(LoginDTO Login);
}
