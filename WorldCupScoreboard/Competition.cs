using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldCupScoreboard
{
    // Represents a list of matches in one competition 
    // We assume that storage of matches is not part of the requirements. Otherwise we would need to abstract it
    public class Competition
    {       
        // we keep matches in a dictionary to avoid duplicates
        // In prod settings we would want to abstract the storage of matches in a repository
        // but to keep it simple we will have storage in a Competition class
        private IDictionary<string,Match> matches;

        public Competition()
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

        public Scoreboard GetScoreboard()
        {
            return Scoreboard.BuildScoreboard(matches.Values.Where(x => x.State == MatchState.Running));
        }
    }
}
