namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            Bills = new HashSet<Bill>();
            MaterialReceipts = new HashSet<MaterialReceipt>();
        }
        public int UserId { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        public int PositionId { get; set; }

        public Position Position { get; set; }

        public ICollection<Bill> Bills { get; set; }

        public ICollection<MaterialReceipt> MaterialReceipts { get; set; }

    }
}
