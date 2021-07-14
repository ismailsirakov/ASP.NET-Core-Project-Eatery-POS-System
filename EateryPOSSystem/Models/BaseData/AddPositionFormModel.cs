namespace EateryPOSSystem.Models.Storekeeper
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class AddPositionFormModel
    {
        [Required]
        [StringLength(PositionNameMaxLength, MinimumLength = PositionNameMinLength)]
        public string Name { get; init; }
    }
}
