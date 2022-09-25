using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Core.DTOs
{
    public class JwtSettings
    {
        public string SigninKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public long JwtTokenDurationHours { get; set; }
        public long RefreshTokenDurationDays { get; set; }
    }
}
