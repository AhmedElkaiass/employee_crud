using EmployeeCRUD.Core.DTOs.Common;
using EmployeeCRUD.Core.DTOs.User;
using EmployeeCRUD.Core.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCRUD.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service) => _service = service;

    [HttpPost]
    public async Task<ActionResult<ServiceResponse>> POST(LoginDTO dto)
        => Ok(await _service.AuthenticateJwt(dto));
}
