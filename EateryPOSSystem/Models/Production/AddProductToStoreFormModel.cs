namespace EateryPOSSystem.Models.Production
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EateryPOSSystem.Services.Models;
    using static Data.DataConstants;

    public class AddProductToStoreFormModel
    {
        [Required]
        [StringLength(ProductNameMaxLength,  MinimumLength = ProductNameMinLength)]
        public string Name { get; set; }

        public int StoreId { get; set; }

        public int ProductTypeId { get; set; }

        public int MeasurementId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,3)")]

        public decimal Quantity { get; set; }

        public IEnumerable<StoreServiceModel> Stores { get; set; }
        public IEnumerable<ProductTypeServiceModel> ProductTypes { get; set; }
        public IEnumerable<MeasurementServiceModel> Measurements { get; set; }
    }
}
