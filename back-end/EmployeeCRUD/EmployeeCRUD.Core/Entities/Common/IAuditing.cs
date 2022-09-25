using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Core.Entities.Common
{
    public interface ICreationAuditable
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public interface ISoftDeleteAuditable
    {
        public DateTime? DeletedDate { get; set; }
    }
}
