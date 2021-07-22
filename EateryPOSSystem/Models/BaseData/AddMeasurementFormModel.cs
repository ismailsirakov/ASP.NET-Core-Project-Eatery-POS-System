namespace EateryPOSSystem.Models.BaseData
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class AddMeasurementFormModel
    {
        [Required]
        [StringLength(MeasurementNameMaxLength, MinimumLength = MeasurementNameMinLength)]
        public string Name { get; init; }
    }
}
