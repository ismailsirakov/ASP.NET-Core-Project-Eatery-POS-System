namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Warehouse
    {
        public Warehouse()
        {
            WarehouseMaterials = new HashSet<WarehouseMaterial>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public ICollection<WarehouseMaterial> WarehouseMaterials { get; set; }
    }
}
