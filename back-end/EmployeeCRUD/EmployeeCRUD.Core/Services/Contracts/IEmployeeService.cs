using EmployeeCRUD.Core.DTOs;
using EmployeeCRUD.Core.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Core.Services.Contracts
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<PageingDataResponse<EmployeeListItem>>> Get(DataPagingRequest request);
        Task<ServiceResponse<EmployeeDTO>> Get(int Id);
        Task<ServiceResponse<bool>> Delete(int Id);
        Task<ServiceResponse<EmployeeDTO>> Save(EmployeeDTO dto);

    }
}
