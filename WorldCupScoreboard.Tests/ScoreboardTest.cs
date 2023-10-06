using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WorldCupScoreboard;

namespace Tests
{
    class  ScoreboardTest
    {
        void TestFlow(){
            var mexCan = new Match(new Team("Mexico"), new Team("Canada"));
            var spaBra = new Match(new Team("Spain"), new Team("Brazil"));
            var gerFra = new Match(new Team("Germany"), new Team("France"));
            var urgIta = new Match(new Team("Uruguay"), new Team("Italy"));
            var argAus = new Match(new Team("Argentina"), new Team("Australia"));

            mexCan.SetScore(0,5);
            spaBra.SetScore(10,2);
            gerFra.SetScore(2,2);
            urgIta.SetScore(6,6);
            argAus.SetScore(3,1);

            Scoreboard scoreboard = Scoreboard.BuildScoreboard(new List<Match> { mexCan, spaBra, gerFra, urgIta, argAus });            
            var matches = scoreboard.Matches.ToList();
            
            Assert.AreEqual(matches[0].Id, urgIta.Id);
            Assert.AreEqual(matches[1].Id, spaBra.Id);
            Assert.AreEqual(matches[2].Id, mexCan.Id);
            Assert.AreEqual(matches[3].Id, argAus.Id);
            Assert.AreEqual(matches[4].Id, gerFra.Id);                        
        }
        
    }
}