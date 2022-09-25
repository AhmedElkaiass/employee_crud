using EmployeeCRUD.Core.DTOs;
using EmployeeCRUD.Core.DTOs.Common;
using EmployeeCRUD.Core.Services.Contracts;
using EmployeeCRUD.Infrastructure.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCRUD.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AppAuth]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service) => _service = service;

        [HttpPost("Paging")]
        public async Task<ActionResult<ServiceResponse>> Paging(DataPagingRequest request)
            => Ok(await _service.Get(request));
        
        [HttpGet("{Id}")]
        public async Task<ActionResult<ServiceResponse>> Get(int Id)
           => Ok(await _service.Get(Id));
        
        [HttpDelete("{Id}")]
        public async Task<ActionResult<ServiceResponse>> Delete(int Id)
          => Ok(await _service.Delete(Id));

        [HttpPost()]
        public async Task<ActionResult<ServiceResponse>> Save(EmployeeDTO dto)
            => Ok(await _service.Save(dto));
    }
}
