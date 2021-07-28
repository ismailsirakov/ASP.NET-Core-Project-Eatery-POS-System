namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Store
    {
        public Store()
        {
            StoreProducts = new HashSet<StoreProduct>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(StoreNameMaxLength)]
        public string Name { get; set; }

        public int TablesInStore { get; set; }

        public ICollection<StoreProduct> StoreProducts { get; set; }
    }
}
