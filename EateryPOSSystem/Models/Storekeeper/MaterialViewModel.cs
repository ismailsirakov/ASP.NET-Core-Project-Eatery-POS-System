namespace EateryPOSSystem.Models.Storekeeper
{
    using System.Collections.Generic;

    public class MaterialViewModel
    {
        public int Id { get; set; }

        public string Name { get; init; }

        public int MeasurementId { get; set; }

        public string MeasurementName { get; set; }

        public ICollection<MeasurementViewModel> Measurements { get; set; }
    }
}
