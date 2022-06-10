using Common.ValidationConstants;
using Infrastructure.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Player
    {
        public Player()
        {
            Id = Guid.NewGuid()
                .ToString();

            Fans = new List<User>();
            Statistics = new List<PlayerMatchStatistic>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(PlayerConstants.NAME_MAXLENGTH)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(PlayerConstants.NAME_MAXLENGTH)]
        public string LastName { get; set; }

        [Range(PlayerConstants.HEIGHT_MIN, PlayerConstants.HEIGHT_MAX)]
        public int Height { get; set; }

        public string ImageUrl { get; set; }

        public string PreferredFoot { get; set; }

        public Position Position { get; set; }

        [Required]
        public string Country { get; set; }

        public DateTime? BirthDate { get; set; }

        [ForeignKey(nameof(Team))]
        public string TeamId { get; set; }

        public virtual Team Team { get; set; }

        public virtual ICollection<PlayerMatchStatistic> Statistics { get; set; }

        public virtual ICollection<User> Fans { get; set; }
    }
}
