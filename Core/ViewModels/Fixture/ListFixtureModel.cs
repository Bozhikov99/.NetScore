using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Fixture
{
    public class ListFixtureModel
    {
        public string HomeTeamId { get; set; }

        public string AwayTeamId { get; set; }

        public string TournamentId { get; set; }

        public bool IsEligible { get; set; }
    }
}
