using Infrastructure.Models;
using Infrastructure.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Player
{
    public class PlayerDetailsModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Height { get; set; }

        public string PreferredFoot { get; set; }

        public Position Position { get; set; }

        public string Country { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? BirthDate { get; set; }

        public int Goals { get; set; }

        public int Assists { get; set; }

        public int Passes { get; set; }

        public int Tackles { get; set; }

        public int Saves { get; set; }

        public int Fouls { get; set; }

        public int Fans { get; set; }
    }
}
