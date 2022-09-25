using EmployeeCRUD.Core.DTOs.Common;
using EmployeeCRUD.Core.DTOs.User;
using EmployeeCRUD.Core.Services.Contracts;
using EmployeeCRUD.Core.Utilities.LinqBuilder.Extentions;
using EmployeeCRUD.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Core.Services.Fucntions;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenProvider _jwtTokenProvider;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        IJwtTokenProvider jwtTokenProvider)
    {
        _userManager = userManager;
        _jwtTokenProvider = jwtTokenProvider;
    }
    public async Task<ServiceResponse<TokenDTO>> AuthenticateJwt(LoginDTO Login)
    {
        var user = await _userManager.FindByNameAsync(Login.UserName);
        if (user == null)
        {
            var checkPassword = await _userManager.CheckPasswordAsync(user, Login.Password);
            if (checkPassword)
            {
                var token = await _GenerateUserToken(user);
                return new ServiceResponse<TokenDTO>(ServiceResponseCode.Success, token);
            }
            else
                return new ServiceResponse<TokenDTO>(ServiceResponseCode.NotFoundData, "Invalid password , please  your password ");
        }
        else
            return new ServiceResponse<TokenDTO>(ServiceResponseCode.NotFoundData, "Invalid Login Data , please check your user name ");

    }
    private async Task<TokenDTO> _GenerateUserToken(ApplicationUser User)
    {

        TokenDTO TokenResult = new TokenDTO();
        var userRoles = await _userManager.GetRolesAsync(User);
        var JwtTokenInfo = _jwtTokenProvider.GenerateJwtToken(User, userRoles);
        TokenResult.Email = User.Email;
        TokenResult.Id = User.Id;
        TokenResult.Token = JwtTokenInfo.Token;
        TokenResult.Roles = userRoles.Select(x => Convert.ToInt32(x)).ToList();
        TokenResult.Id = User.Id;
        return TokenResult;
    }
}
