using EmployeeCRUD.Core.Services.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Core.Services.Fucntions
{
    public class UserDataProvider : IUserDataProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UserDataProvider(
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }
        public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public int UserId
        {
            get
            {
                string id = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value?.Trim();
                if (string.IsNullOrWhiteSpace(id))
                {
                    var accesstoken = _httpContextAccessor.HttpContext.Request.Query["access_token"].ToString();
                    var handler = new JwtSecurityTokenHandler();
                    if (!string.IsNullOrWhiteSpace(accesstoken))
                    {
                        var jsonToken = handler.ReadToken(accesstoken);
                        var tokenS = jsonToken as JwtSecurityToken;
                        id = tokenS.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value.ToString();
                    }
                    else
                        id = "0";
                }
                return Convert.ToInt32(id);
            }
        }

        public IEnumerable<int> Roles => _httpContextAccessor.HttpContext.User.FindAll(ClaimTypes.Role).Select(roleClaim => Convert.ToInt32(roleClaim?.Value?.Trim()));
        public bool IsInRole(int Role)
        {
            return Roles.Any(role => role == Role);
        }
    }
}
