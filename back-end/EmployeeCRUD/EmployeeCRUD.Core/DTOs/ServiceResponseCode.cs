using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Core.DTOs.Common
{
    public enum ServiceResponseCode
    {
        Success = 200,
        ValidationErrors = 400,
        Failed = 500,
        NotFoundData = 103,
    }
}
