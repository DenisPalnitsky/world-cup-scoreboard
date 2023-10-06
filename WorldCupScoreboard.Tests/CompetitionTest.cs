using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupScoreboard.Tests
{
    internal class ScoreboardTest
    {        
        [Test]
        public void GetScoreboard()
        {
            const string argentina = "Argentina";           
            const string brasil = "Brazil";
            const string moldova = "Moldova";

            var scoreboard = new Competition();
            
            var argFra = new Match(new Team(argentina), new Team("France"), MatchStage.Group);
            scoreboard.RegisterMatch(argFra);

            var braCro = new Match(new Team(brasil), new Team("Croatia"));
            scoreboard.RegisterMatch(braCro);

            var mdlPol = new Match(new Team(moldova), new Team("Poland"));
            scoreboard.RegisterMatch(mdlPol);

            Assert.AreEqual(0, scoreboard.GetScoreboard().Matches.Count());
            
            argFra.Start();
            var summary = scoreboard.GetScoreboard(); 
            Assert.AreEqual(1, summary.Matches.Count());
            Assert.AreEqual(argentina, summary.Matches.First().Home.Name);

            // two matches started with 0:0 score
            braCro.Start();
            summary = scoreboard.GetScoreboard();
            Assert.AreEqual(2, summary.Matches.Count());
            Assert.AreEqual(brasil, summary.Matches.First().Home.Name);
            Assert.AreEqual(argentina, summary.Matches.Last().Home.Name);

            // Argentina match shuold be higher because of score
            argFra.SetScore(1, 0);
            summary = scoreboard.GetScoreboard();
            Assert.AreEqual(2, summary.Matches.Count());
            Assert.AreEqual(argentina, summary.Matches.First().Home.Name);
            Assert.AreEqual(brasil, summary.Matches.Last().Home.Name);

            // Third match started, it should be second because of start time
            mdlPol.Start();
            summary = scoreboard.GetScoreboard();
            Assert.AreEqual(3, summary.Matches.Count());
            Assert.AreEqual(argentina, summary.Matches.First().Home.Name);
            Assert.AreEqual(moldova, summary.Matches.Skip(1).First().Home.Name);

            // Moldova match should be on top because of score
            mdlPol.SetScore(50, 17);
            Assert.AreEqual(moldova, scoreboard.GetScoreboard().Matches.First().Home.Name); 

            // Brazil match should be second because of time
            braCro.SetScore(1, 0);
            Assert.AreEqual(brasil, scoreboard.GetScoreboard().Matches.Skip(1).First().Home.Name);


            // hide finished matches
            argFra.Finish();           
            summary = scoreboard.GetScoreboard();
            Assert.AreEqual(2, summary.Matches.Count());
            Assert.AreEqual(moldova, summary.Matches.First().Home.Name);
            Assert.AreEqual(brasil, summary.Matches.Last().Home.Name);            
        }
    
        [Test]
        public void AddMatch (){
            var competition = new Competition();
            var argFra = new Match(new Team("Argentina"), new Team("France"), MatchStage.Group);
            competition.RegisterMatch(argFra);                        
            Assert.Throws<InvalidOperationException>(() => competition.RegisterMatch(argFra));            
            var argFraPlayOff = new Match(new Team("Argentina"), new Team("France"), MatchStage.PlayOff);
            // does not throw exception because it is a different stage
            competition.RegisterMatch(argFraPlayOff);
            
            var argFra3 = new Match(new Team("Argentina"), new Team("France"), MatchStage.Group);
            Assert.Throws<InvalidOperationException>(() => competition.RegisterMatch(argFra3));
        }
    }
}
