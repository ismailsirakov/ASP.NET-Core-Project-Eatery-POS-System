namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Warehouse
    {
        public Warehouse()
        {
            WarehouseMaterials = new HashSet<WarehouseMaterial>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(WarehouseNameMaxLength)]
        public string Name { get; set; }

        public ICollection<WarehouseMaterial> WarehouseMaterials { get; set; }
    }
}
