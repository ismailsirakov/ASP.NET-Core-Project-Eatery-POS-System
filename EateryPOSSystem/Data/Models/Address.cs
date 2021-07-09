namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Address
    {
        public Address()
        {
            Providers = new HashSet<Provider>();
        }
        public int Id { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        [Required]
        [MaxLength(100)]
        public string AddressDetails { get; set; }

        public ICollection<Provider> Providers { get; set; }
    }
}
