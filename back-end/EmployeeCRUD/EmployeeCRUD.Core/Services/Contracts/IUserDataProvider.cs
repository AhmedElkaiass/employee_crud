using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Core.Services.Contracts
{
    public interface IUserDataProvider
    {
        public bool IsAuthenticated { get; }
        public int UserId { get; }
        public IEnumerable<int> Roles { get; }
        bool IsInRole(int Role);

    }
}
