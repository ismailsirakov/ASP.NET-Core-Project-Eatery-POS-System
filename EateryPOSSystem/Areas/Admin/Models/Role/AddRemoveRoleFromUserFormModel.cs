namespace EateryPOSSystem.Areas.Admin.Models.Role
{
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;
    using Microsoft.AspNetCore.Identity;

    public class AddRemoveRoleFromUserFormModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public IEnumerable<UserServiceModel> Users { get; set; }

        public ICollection<IdentityRole> Roles { get; set; }

        public ICollection<string> RolesNames { get; set; }
    }
}
