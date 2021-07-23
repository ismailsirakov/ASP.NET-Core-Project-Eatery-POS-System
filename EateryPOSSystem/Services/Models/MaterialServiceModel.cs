namespace EateryPOSSystem.Services.Models
{
    using System.Collections.Generic;

    public class MaterialServiceModel
    {
        public int Id { get; set; }

        public string Name { get; init; }

        public int MeasurementId { get; set; }

        public string MeasurementName { get; set; }

        public IEnumerable<MeasurementServiceModel> Measurements { get; set; }
    }
}
