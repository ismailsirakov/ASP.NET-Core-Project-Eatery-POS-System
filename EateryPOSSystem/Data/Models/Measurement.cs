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
            Products = new HashSet<Product>();
            SoldProducts = new HashSet<SoldProduct>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(MeasurementNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Material> Materials { get; set; }

        public ICollection<Product> Products { get; set; }

        public ICollection<SoldProduct> SoldProducts { get; set; }
    }
}
