using EmployeeCRUD.Core.Entities;
using EmployeeCRUD.Core.Interfaces.Genaric;
using EmployeeCRUD.Core.Reopsitories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUD.Infrastructure.DataAccess.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext Db) : base(Db)
        {

        }
    }
}
