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

            var scoreboard = new Scoreboard();
            
            var argFra = new Match(new Team(argentina), new Team("France"), MatchStage.Group);
            scoreboard.RegisterMatch(argFra);

            var braCro = new Match(new Team(brasil), new Team("Croatia"));
            scoreboard.RegisterMatch(braCro);

            var mdlPol = new Match(new Team(moldova), new Team("Poland"));
            scoreboard.RegisterMatch(mdlPol);

            Assert.AreEqual(0, scoreboard.ListInProgress().Count());
            
            argFra.Start();
            var summary = scoreboard.ListInProgress(); 
            Assert.AreEqual(1, summary.Count());
            Assert.AreEqual(argentina, summary.First().Home.Name);

            // two matches started with 0:0 score
            braCro.Start();
            summary = scoreboard.ListInProgress();
            Assert.AreEqual(2, summary.Count());
            Assert.AreEqual(brasil, summary.First().Home.Name);
            Assert.AreEqual(argentina, summary.Last().Home.Name);

            // Argentina match shuold be higher because of score
            argFra.SetScore(1, 0);
            summary = scoreboard.ListInProgress();
            Assert.AreEqual(2, summary.Count());
            Assert.AreEqual(argentina, summary.First().Home.Name);
            Assert.AreEqual(brasil, summary.Last().Home.Name);

            // Third match started, it should be second because of start time
            mdlPol.Start();
            summary = scoreboard.ListInProgress();
            Assert.AreEqual(3, summary.Count());
            Assert.AreEqual(argentina, summary.First().Home.Name);
            Assert.AreEqual(moldova, summary.Skip(1).First().Home.Name);

            // Moldova match should be on top because of score
            mdlPol.SetScore(50, 17);
            Assert.AreEqual(moldova, scoreboard.ListInProgress().First().Home.Name); 

            // Brazil match should be second because of time
            braCro.SetScore(1, 0);
            Assert.AreEqual(brasil, scoreboard.ListInProgress().Skip(1).First().Home.Name);


            // hide finished matches
            argFra.Finish();           
            summary = scoreboard.ListInProgress();
            Assert.AreEqual(2, summary.Count());
            Assert.AreEqual(moldova, summary.First().Home.Name);
            Assert.AreEqual(brasil, summary.Last().Home.Name);            
        }
    
        [Test]
        public void AddMatch (){
            var competition = new Scoreboard();
            var argFra = new Match(new Team("Argentina"), new Team("France"), MatchStage.Group);
            competition.RegisterMatch(argFra);                        
            Assert.Throws<InvalidOperationException>(() => competition.RegisterMatch(argFra));            
            var argFraPlayOff = new Match(new Team("Argentina"), new Team("France"), MatchStage.PlayOff);
            // does not throw exception because it is a different stage
            competition.RegisterMatch(argFraPlayOff);
            
            var argFra3 = new Match(new Team("Argentina"), new Team("France"), MatchStage.Group);
            Assert.Throws<InvalidOperationException>(() => competition.RegisterMatch(argFra3));
        }

        [Test]
        public void TestFlow()
        {
            var mexCan = new Match(new Team("Mexico"), new Team("Canada"));
            var spaBra = new Match(new Team("Spain"), new Team("Brazil"));
            var gerFra = new Match(new Team("Germany"), new Team("France"));
            var urgIta = new Match(new Team("Uruguay"), new Team("Italy"));
            var argAus = new Match(new Team("Argentina"), new Team("Australia"));
            mexCan.Start();
            spaBra.Start();
            gerFra.Start();
            urgIta.Start();
            argAus.Start();


            mexCan.SetScore(0, 5);
            spaBra.SetScore(10, 2);
            gerFra.SetScore(2, 2);
            urgIta.SetScore(6, 6);
            argAus.SetScore(3, 1);

            Scoreboard scoreboard = new Scoreboard();
            scoreboard.RegisterMatch(mexCan);
            scoreboard.RegisterMatch(spaBra);
            scoreboard.RegisterMatch(gerFra);
            scoreboard.RegisterMatch(urgIta);
            scoreboard.RegisterMatch(argAus);

            var matches = scoreboard.ListInProgress().ToArray();

            Assert.AreEqual(matches[0].Id, urgIta.Id);
            Assert.AreEqual(matches[1].Id, spaBra.Id);
            Assert.AreEqual(matches[2].Id, mexCan.Id);
            Assert.AreEqual(matches[3].Id, argAus.Id);
            Assert.AreEqual(matches[4].Id, gerFra.Id);
        }
    }
}
