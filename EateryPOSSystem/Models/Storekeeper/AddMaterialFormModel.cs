namespace EateryPOSSystem.Models.Storekeeper
{
    using EateryPOSSystem.Services.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class AddMaterialFormModel
    {
        [Required]
        [StringLength(MaterialNameMaxLength, MinimumLength = MaterialNameMinLength)]
        public string Name { get; init; }

        public int MeasurementId { get; init; }

        public IEnumerable<MeasurementServiceModel> Measurements { get; set; }
    }
}
