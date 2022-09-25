using EmployeeCRUD.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Infrastructure.DataAccess.DataSeed
{
    public class ApplicationRoleSeed : IEntitySeed<ApplicationRole>
    {
        public IEnumerable<ApplicationRole> GetEntitiesToSeed()
        {
            return new List<ApplicationRole>
            {
                new ApplicationRole
            {
                Id =1,
                Name = "Admin",
                NormalizedName = "Admin".ToUpper(),
                ConcurrencyStamp="a9978a15-e91e-46bb-b388-34123ee97074"
            },

            };
        }
    }
}
