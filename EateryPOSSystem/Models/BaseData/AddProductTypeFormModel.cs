namespace EateryPOSSystem.Models.Storekeeper
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class AddProductTypeFormModel
    {
        [Required]
        [StringLength(ProductTypeNameMaxLength, MinimumLength = ProductTypeNameMinLength)]
        public string Name { get; init; }
    }
}
