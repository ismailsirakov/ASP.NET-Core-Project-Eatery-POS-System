namespace EateryPOSSystem.Areas.Admin.Models.Role
{
    using System.ComponentModel.DataAnnotations;
    using static AdminConstants;

    public class RoleFormModel
    {
        [Required]
        [StringLength(RoleNameMaxLength, MinimumLength = RoleNameMinLength)]
        public string Name { get; set; }
    }
}
