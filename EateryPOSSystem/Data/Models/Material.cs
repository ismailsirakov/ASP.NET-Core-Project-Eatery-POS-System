namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Material
    {
        public Material()
        {
            Transfers = new HashSet<Transfer>();
            WarehouseMaterials = new HashSet<WarehouseMaterial>();
            WarehouseReceipts = new HashSet<WarehouseReceipt>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(MaterialNameMaxLength)]
        public string Name { get; set; }

        public int MeasurementId { get; set; }

        public Measurement Measurement { get; set; }

        public IEnumerable<Transfer> Transfers { get; set; }

        public IEnumerable<WarehouseReceipt> WarehouseReceipts { get; set; }

        public IEnumerable<WarehouseMaterial> WarehouseMaterials { get; set; }
    }
}
