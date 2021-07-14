namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class DocumentType
    {
        public DocumentType()
        {
            MaterialReceipts = new HashSet<WarehouseReceipt>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(DocumentTypeNameMaxLength)]
        public string Name { get; set; }

        public ICollection<WarehouseReceipt> MaterialReceipts { get; set; }
    }
}
