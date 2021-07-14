namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Material
    {
        public Material()
        {
            Recipes = new HashSet<Recipe>();
            WarehouseMaterials = new HashSet<WarehouseMaterial>();
            WarehouseReceipts = new HashSet<WarehouseReceipt>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(MaterialNameMaxLength)]
        public string Name { get; set; }

        public int MeasurementId { get; set; }

        public Measurement Measurement { get; set; }

        public ICollection<Recipe> Recipes { get; set; }

        public ICollection<WarehouseMaterial> WarehouseMaterials { get; set; }

        public ICollection<WarehouseReceipt> WarehouseReceipts { get; set; }
    }
}
