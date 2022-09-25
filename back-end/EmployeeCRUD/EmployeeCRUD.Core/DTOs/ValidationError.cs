using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Core.DTOs
{
    public class ValidationError
    {
        public ValidationError(string field, string message)
        {
            Field = field;
            Message = message;
        }
        public string Field { get; set; }
        public string Message { get; set; }
    }
}
