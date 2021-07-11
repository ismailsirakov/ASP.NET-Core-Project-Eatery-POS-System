namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Material
    {
        public Material()
        {
            WarehouseMaterials = new HashSet<WarehouseMaterial>();
            MaterialReceipts = new HashSet<MaterialReceipt>();
            Recipes = new HashSet<Recipe>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(MaterialNameMaxLength)]
        public string Name { get; set; }

        public int MeasurementId { get; set; }

        public Measurement Measurement { get; set; }

        public ICollection<WarehouseMaterial> WarehouseMaterials { get; set; }

        public ICollection<Recipe> Recipes { get; set; }

        public ICollection<MaterialReceipt> MaterialReceipts { get; set; }
    }
}
