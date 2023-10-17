using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldCupScoreboard
{
    // Represents a list of matches in one competition 
    // We assume that storage of matches is not part of the requirements. Otherwise we would need to abstract it
    public class Scoreboard
    {       
        // we keep matches in a dictionary to avoid duplicates
        // In prod settings we would want to abstract the storage of matches in a repository
        // but to keep it simple we will have storage in a Competition class
        private IDictionary<string,Match> matches;

        public Scoreboard()
        {
            matches = new Dictionary<string, Match>();
        }

        public void RegisterMatch(Match match)
        {                                            
            try 
            {                
                matches.Add(match.Id, match);
            }
            catch(ArgumentException ex) // we use match ID (stage+teams) to ensure that matches are not duplicated
            {
                throw new InvalidOperationException("Match already registered", ex);
            }            
        }

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

        public IEnumerable<Match> ListInProgress()
        {
            return matches.Values.Where(x => x.State == MatchState.Running).Order(new MatchComparer());
        }
    }
}
