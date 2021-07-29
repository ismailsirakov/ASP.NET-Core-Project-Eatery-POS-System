namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Measurement
    {
        public Measurement()
        {
            Materials = new HashSet<Material>();
            StoreProducts = new HashSet<StoreProduct>();
            SoldProducts = new HashSet<SoldProduct>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(MeasurementNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Material> Materials { get; set; }

        public IEnumerable<StoreProduct> StoreProducts { get; set; }

        public IEnumerable<SoldProduct> SoldProducts { get; set; }
    }
}
