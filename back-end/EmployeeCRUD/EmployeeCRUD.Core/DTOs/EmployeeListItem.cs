using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Core.DTOs
{
    public class EmployeeListItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public float BasicSalary { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
