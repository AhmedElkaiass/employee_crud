using EmployeeCRUD.Core.DTOs;
using EmployeeCRUD.Core.Services.Contracts;
using EmployeeCRUD.Entities.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Core.Services.Fucntions;

public class JWTProvider: IJwtTokenProvider
{
    readonly IOptions<JwtSettings> _jwtSettingsOptions;
    public JWTProvider(IOptions<JwtSettings> jwtSettingsOptions
                            )
    {
        _jwtSettingsOptions = jwtSettingsOptions;
    }
    public (string Token, DateTime Expiration) GenerateJwtToken(ApplicationUser User, IList<string> Roles)
    {
        var TokenHandler = new JwtSecurityTokenHandler();
        byte[] SignInKey = Encoding.ASCII.GetBytes(_jwtSettingsOptions.Value.SigninKey);
        var SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SignInKey), SecurityAlgorithms.HmacSha256);
        var TokenCliams = new List<Claim>
        {
                new Claim(ClaimTypes.NameIdentifier ,User.Id.ToString()),
                new Claim(ClaimTypes.Name,User.UserName),
        };

        if (Roles != null)
            foreach (var role in Roles)
                TokenCliams.Add(new Claim(ClaimTypes.Role, role));

        var ExpirationTime = DateTime.Now.AddDays(30);
        var JWTToken = new JwtSecurityToken(issuer: _jwtSettingsOptions.Value.Issuer,
                                            audience: _jwtSettingsOptions.Value.Audience,
                                            claims: TokenCliams,
                                            expires: ExpirationTime,
                                            signingCredentials: SigningCredentials);

        string JwtToken = TokenHandler.WriteToken(JWTToken);
        return (JwtToken, ExpirationTime);
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true, //you might want to validate the audience and issuer depending on your use case
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettingsOptions.Value.SigninKey)),
            ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            return null;
        return principal;
    }
}
