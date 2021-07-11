namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class User
    {
        public User()
        {
            Bills = new HashSet<Bill>();
            MaterialReceipts = new HashSet<MaterialReceipt>();
        }
        public int UserId { get; set; }

        [Required]
        [MaxLength(UserFirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(UserLastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(UserPasswordMaxLength)]
        public string Password { get; set; }

        public int PositionId { get; set; }

        public Position Position { get; set; }

        public ICollection<Bill> Bills { get; set; }

        public ICollection<MaterialReceipt> MaterialReceipts { get; set; }

    }
}
