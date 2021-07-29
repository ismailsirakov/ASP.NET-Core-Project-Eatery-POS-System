namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Provider
    {
        public Provider()
        {
            WarehouseReceipts = new HashSet<WarehouseReceipt>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(ProviderNameMaxLength)]
        public string Name { get; set; }
        
        public int Number { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        public IEnumerable<WarehouseReceipt> WarehouseReceipts { get; set; }
    }
}
