using Infrastructure.Models.Enums;
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

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Height { get; set; }

        public string PreferredFoot { get; set; }

        public Position Position { get; set; }

        public string Country { get; set; }

        public DateTime BirthDate { get; set; }

        [ForeignKey(nameof(Team))]
        public string TeamId { get; set; }

        public virtual Team Team { get; set; }

        public virtual ICollection<PlayerMatchStatistic> Statistics { get; set; }

        public virtual ICollection<User> Fans { get; set; }
    }
}
