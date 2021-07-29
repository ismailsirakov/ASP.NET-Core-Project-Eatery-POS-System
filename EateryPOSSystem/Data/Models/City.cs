namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class City
    {
        public City()
        {
            Providers = new HashSet<Provider>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(CityNameMaxLength)]
        public string Name { get; set; }

        public int? PostalCode { get; set; }

        public IEnumerable<Provider> Providers { get; set; }
    }
}
