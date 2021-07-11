namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class DocumentType
    {
        public DocumentType()
        {
            MaterialReceipts = new HashSet<MaterialReceipt>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(DocumentTypeNameMaxLength)]
        public string Name { get; set; }

        public ICollection<MaterialReceipt> MaterialReceipts { get; set; }
    }
}
