using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Core.DTOs.User;

public class TokenDTO
{
    public string Token { get; set; }
    public bool IsAuthenticated { get { return !string.IsNullOrWhiteSpace(Token); } }
    public List<int> Roles { get; set; }
    public string Email { get; set; }
    public int Id { get; set; }
}
