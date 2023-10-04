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
        public void GetSummary()
        {
            const string argentina = "Argentina";           
            const string brasil = "Brazil";
            const string moldova = "Moldova";

            var scoreboard = new Scoreboard();
            
            var argFra = new Match(new Team(argentina), new Team("France"));
            scoreboard.RegisterMatch(argFra);

            var braCro = new Match(new Team(brasil), new Team("Croatia"));
            scoreboard.RegisterMatch(braCro);

            var mdlPol = new Match(new Team(moldova), new Team("Poland"));
            scoreboard.RegisterMatch(mdlPol);

            Assert.AreEqual(0, scoreboard.GetInProgress().Matches.Count());
            
            argFra.Start();
            var summary = scoreboard.GetInProgress(); 
            Assert.AreEqual(1, summary.Matches.Count());
            Assert.AreEqual(argentina, summary.Matches.First().Home.Name);

            // two matches started with 0:0 score
            braCro.Start();
            summary = scoreboard.GetInProgress();
            Assert.AreEqual(2, summary.Matches.Count());
            Assert.AreEqual(brasil, summary.Matches.First().Home.Name);
            Assert.AreEqual(argentina, summary.Matches.Last().Home.Name);

            // Argentina match shuold be higher because of score
            argFra.SetScore(1, 0);
            summary = scoreboard.GetInProgress();
            Assert.AreEqual(2, summary.Matches.Count());
            Assert.AreEqual(argentina, summary.Matches.First().Home.Name);
            Assert.AreEqual(brasil, summary.Matches.Last().Home.Name);

            // Third match started, it should be second because of start time
            mdlPol.Start();
            summary = scoreboard.GetInProgress();
            Assert.AreEqual(3, summary.Matches.Count());
            Assert.AreEqual(argentina, summary.Matches.First().Home.Name);
            Assert.AreEqual(moldova, summary.Matches.Skip(1).First().Home.Name);

            // Moldova match should be on top because of score
            mdlPol.SetScore(50, 17);
            Assert.AreEqual(moldova, scoreboard.GetInProgress().Matches.First().Home.Name); 

            // Brazil match should be second because of time
            braCro.SetScore(1, 0);
            Assert.AreEqual(brasil, scoreboard.GetInProgress().Matches.Skip(1).First().Home.Name);


            // hide finished matches
            argFra.Finish();           
            summary = scoreboard.GetInProgress();
            Assert.AreEqual(2, summary.Matches.Count());
            Assert.AreEqual(moldova, summary.Matches.First().Home.Name);
            Assert.AreEqual(brasil, summary.Matches.Last().Home.Name);            
        }
    }
}
