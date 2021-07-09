namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DocumentType
    {
        public DocumentType()
        {
            MaterialReceipts = new HashSet<MaterialReceipt>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public ICollection<MaterialReceipt> MaterialReceipts { get; set; }
    }
}
