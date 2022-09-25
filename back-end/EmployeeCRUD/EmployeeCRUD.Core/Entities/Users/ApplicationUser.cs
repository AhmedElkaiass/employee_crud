using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Entities.Users
{
    public class ApplicationUser : IdentityUser<int>
    {
        // identity Data 
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}