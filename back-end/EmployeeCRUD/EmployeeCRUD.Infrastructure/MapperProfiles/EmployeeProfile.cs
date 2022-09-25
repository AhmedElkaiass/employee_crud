using AutoMapper;
using EmployeeCRUD.Core.DTOs;
using EmployeeCRUD.Core.DTOs.Common;
using EmployeeCRUD.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Infrastructure.MappMagicStorerofiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeListItem, Employee>().ReverseMap();
            CreateMap<EmployeeDTO, Employee>().ReverseMap();
        }
    }
}
