namespace EateryPOSSystem.Models.Storekeeper
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class AddMaterialFormModel
    {
        [Required]
        [StringLength(MaterialNameMaxLength, MinimumLength =MaterialNameMinLength)]
        public string Name { get; set; }

        public int MeasurementId { get; set; }

        public ICollection<MeasurementViewModel> Measurements { get; set; }
    }
}
