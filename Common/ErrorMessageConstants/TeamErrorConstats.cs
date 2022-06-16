using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ErrorMessageConstants
{
    public class TeamErrorConstats
    {
        public const string TEAM_EXISTS = "A team with name {0} already exists";
        public const string UNEXPECTED_CREATING = "Unexpected error creating a team";
        public const string UNEXPECTED_EDITING = "Unexpected error editing a team";
        public const string UNEXPECTED_ADDING_PLAYERS = "Unexpected error adding players to the squad";
    }
}
