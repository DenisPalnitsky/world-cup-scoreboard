using NUnit.Framework;
using System;
using WorldCupScoreboard;

namespace Tests
{
    public class MatchTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MatchWorkflow_HappyPath()
        {
            var match = new Match(new Team("Argentina"), new Team("Germany"));
            Assert.AreEqual(match.State, MatchState.NotStarted);
            match.Start();
            Assert.AreEqual(match.State, MatchState.Running);
            Assert.That(match.TimeStarted, Is.EqualTo(DateTime.Now).Within(1).Seconds);

            match.SetScore(1, 0);

            Assert.That(match.HomeScore, Is.EqualTo(1));
            Assert.That(match.AwayScore, Is.EqualTo(0));

            match.Finish();

            Assert.AreEqual(match.State, MatchState.Finished);
        }

        [Test]
        public void Match_Errors()
        {
            var match = new Match(new Team("Argentina"), new Team("Germany"));
            Assert.Throws<InvalidOperationException>(() => match.Finish());
            Assert.Throws<InvalidOperationException>(() => match.SetScore(1, 0));
            match.Start();

            Assert.Throws<InvalidOperationException>(() => match.Start());
            match.SetScore(1, 0);

            Assert.Throws<ArgumentException>(() => match.SetScore(0, 0));
            Assert.Throws<ArgumentException>(() => match.SetScore(0, 1));
            Assert.Throws<InvalidOperationException>(() => match.Start());

            match.Finish();
            Assert.Throws<InvalidOperationException>(() => match.Finish());
            Assert.Throws<InvalidOperationException>(() => match.SetScore(1, 0));
            Assert.Throws<InvalidOperationException>(() => match.Start());
        }
    }
}