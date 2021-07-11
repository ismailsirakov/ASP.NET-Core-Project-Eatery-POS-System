namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class ProductType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ProductTypeNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
