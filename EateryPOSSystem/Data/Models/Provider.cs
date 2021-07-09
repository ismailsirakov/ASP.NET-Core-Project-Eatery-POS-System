namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Provider
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string ProviderNumber { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        public ICollection<MaterialReceipt> MaterialReceipts { get; set; }
    }
}
