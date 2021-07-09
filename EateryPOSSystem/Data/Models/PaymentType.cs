namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PaymentType
    {
        public PaymentType()
        {
            Bills = new HashSet<Bill>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public ICollection<Bill> Bills { get; set; }
    }
}
