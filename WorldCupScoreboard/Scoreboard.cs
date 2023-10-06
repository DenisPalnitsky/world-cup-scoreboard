using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldCupScoreboard
{
    /// <summary>
    /// Represents summary of a score table 
    /// </summary>
    public class Scoreboard
    {
        // Comparer used to sort matches by score and time
        private class MatchComparer : IComparer<Match>
        {
            public int Compare(Match x, Match y)
            {
                var xScore = x.HomeScore + x.AwayScore;
                var yScore = y.HomeScore + y.AwayScore;
                int compare = xScore.CompareTo(yScore) * -1; // invert sign to sort descending

                if (compare == 0)
                    return DateTime.Compare(x.TimeStarted, y.TimeStarted) * -1; // invert sign to sort descending

                return compare;
            }
        }


        public IEnumerable<Match> Matches { get; private set; }

        // Private constructor to prevent creating summary objects without using the factory method
        private Scoreboard()
        {           
        }
         

        // Creates a summary object with the list of matches that are in progress and sorts them by score and time
        // This method should be called by the Scoreboard class and should not be public
        internal static Scoreboard BuildScoreboard(IEnumerable<Match> matchList)
        {                                   
            return new Scoreboard() { Matches = matchList.Order(new MatchComparer()) };
        }
    }

}
