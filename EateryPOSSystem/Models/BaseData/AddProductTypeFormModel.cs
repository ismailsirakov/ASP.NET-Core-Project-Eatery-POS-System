namespace EateryPOSSystem.Models.BaseData
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
