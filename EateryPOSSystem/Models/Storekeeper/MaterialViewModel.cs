using System.Collections.Generic;

namespace EateryPOSSystem.Models.Storekeeper
{
    public class MaterialViewModel
    {
        public int Id { get; set; }

        public string Name { get; init; }

        public int MeasurementId { get; set; }

        public ICollection<MeasurementViewModel> Measurements { get; set; }
    }
}
