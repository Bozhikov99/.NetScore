using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ErrorMessageConstants
{
    public class TournamentErrorConstants
    {
        public const string UNEXPECTED_CREATE = "Unexpected error creating {0} tournament";
        public const string UNEXPECTED_LOADING_MATCH = "Unexpected error loading match";
        public const string UNEXPECTED_SCHEDULE = "Unexpected error scheduling the tournament's fixture";
    }
}
