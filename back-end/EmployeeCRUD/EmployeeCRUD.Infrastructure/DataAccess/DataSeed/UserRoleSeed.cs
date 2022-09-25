using EmployeeCRUD.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Infrastructure.DataAccess.DataSeed
{
    public class UserRoleSeed : IEntitySeed<ApplicationUserRole>
    {
        IEnumerable<ApplicationUserRole> IEntitySeed<ApplicationUserRole>.GetEntitiesToSeed()
        {
            return new List<ApplicationUserRole> {
            new ApplicationUserRole
            {
                RoleId = 1, // admin
                UserId = 1, // ahmed Elkaiass
                
            }
            };
        }
    }
}
