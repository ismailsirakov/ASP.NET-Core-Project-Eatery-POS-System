namespace EateryPOSSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Position
    {
        public Position()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(PositionNameMaxLength)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
