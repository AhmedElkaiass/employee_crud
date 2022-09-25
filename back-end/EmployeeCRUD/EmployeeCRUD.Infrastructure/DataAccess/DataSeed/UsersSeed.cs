using EmployeeCRUD.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Infrastructure.DataAccess.DataSeed
{
    public class UsersSeed : IEntitySeed<ApplicationUser>
    {
        public IEnumerable<ApplicationUser> GetEntitiesToSeed()
        {
            return new List<ApplicationUser> {
            new ApplicationUser
            {
                Id=1,
                UserName="admin",
                ConcurrencyStamp="a1dc9265-83bb-46bf-a482-336984154108",
                Email ="am310471@gmail.com",
                PhoneNumber ="01063930538",
                NormalizedEmail="AM3610471@GMAIL.COM",
                NormalizedUserName ="ADMIN",
                PasswordHash="AQAAAAEAACcQAAAAEIqNXupOSGwEf9MnPO/QXZncOF3wqOQK9wFNolSkPzTUhI5cDeqE5zQLyrTk+9mLKQ==", // => P@ssw0rd,
                SecurityStamp="a1dc9265-83bb-46bf-a482-336984154996",
            }
            };
        }
    }
}
