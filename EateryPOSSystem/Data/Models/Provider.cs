namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Provider
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ProviderNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ProviderNumberMaxLength)]
        public string ProviderNumber { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        public ICollection<MaterialReceipt> MaterialReceipts { get; set; }
    }
}
