namespace EateryPOSSystem.Models.BaseData
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class AddWarehouseFormModel
    {
        [Required]
        [StringLength(WarehouseNameMaxLength, MinimumLength = WarehouseNameMinLength)]
        public string Name { get; init; }
    }
}
