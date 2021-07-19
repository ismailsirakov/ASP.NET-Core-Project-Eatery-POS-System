namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Address
    {
        public Address()
        {
            Providers = new HashSet<Provider>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(AddressDetailsMaxLength)]
        public string AddressDetails { get; set; }

        public ICollection<Provider> Providers { get; set; }
    }
}
