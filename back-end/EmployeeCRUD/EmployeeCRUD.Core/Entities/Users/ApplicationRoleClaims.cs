using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Entities.Users
{
    public class ApplicationRoleClaims : IdentityRoleClaim<int>
    {
    }
    public class ApplicationUserClaim : IdentityUserClaim<int> { }
    public class ApplicationUserLogin : IdentityUserLogin<int> { }
    public class ApplicationUserToken : IdentityUserToken<int> { }
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public ApplicationUserRole() { }
        //public ApplicationUserRole(int userId, int roleId)
        //{
        //    UserId = userId;
        //    RoleId = roleId;
        //}

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public override int UserId { get; set; }

        [ForeignKey("RoleId")]
        public ApplicationRole Role { get; set; }
        public override int RoleId { get; set; }


    }
}
