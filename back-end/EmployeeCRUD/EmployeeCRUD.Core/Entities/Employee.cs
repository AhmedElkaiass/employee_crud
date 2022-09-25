using EmployeeCRUD.Core.Entities.Common;
using EmployeeCRUD.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Core.Entities
{
    public class Employee : ICreationAuditable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Address { get; set; }

        public float BasicSalary { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime DateOfBirth { get; set; }

        #region ICreationAuditable Members
        [ForeignKey("CreatedBy")]
        public ApplicationUser AuditCreatedBy { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        #endregion
    }
}
