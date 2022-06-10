﻿using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid()
                .ToString();

            FavoritePlayers = new List<Player>();
            FavoriteTeams = new List<Team>();
        }

        [Key]
        public string Id { get; set; }

        public virtual ICollection<Team> FavoriteTeams { get; set; }

        public virtual ICollection<Player> FavoritePlayers { get; set; }
    }
}
