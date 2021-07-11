namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Store
    {
        public Store()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(StoreNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
