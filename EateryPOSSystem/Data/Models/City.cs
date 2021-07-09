namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class City
    {
        public City()
        {
            Addresses = new HashSet<Address>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int? PostalCode { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
