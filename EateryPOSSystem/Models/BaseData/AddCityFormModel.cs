namespace EateryPOSSystem.Models.Storekeeper
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class AddCityFormModel
    {
        [Required]
        [StringLength(CityNameMaxLength,  MinimumLength = CityNameMinLength)]
        public string Name { get; init; }

        public int? PostalCode { get; init; }
    }
}
