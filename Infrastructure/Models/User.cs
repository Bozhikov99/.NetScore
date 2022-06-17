using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Id = Guid.NewGuid()
                .ToString();

            FavoritePlayers = new List<Player>();
            FavoriteTeams = new List<Team>();
        }

        public virtual ICollection<Team> FavoriteTeams { get; set; }

        public virtual ICollection<Player> FavoritePlayers { get; set; }
    }
}
