using System;
using System.Collections.Generic;

namespace WorldCupScoreboard
{
    public class Scoreboard
    {
        private List<Match> matches;

        public Scoreboard()
        {
            matches = new List<Match>();
        }

        public void RegisterMatch(Match match)
        {            
            matches.Add(match);
        }

        public Summary GetInProgress()
        {
            return Summary.BuildInProgressSummary(matches);
        }
    }

}
