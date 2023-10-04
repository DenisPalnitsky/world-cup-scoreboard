using System;

namespace WorldCupScoreboard
{
    public enum MatchState
    {
        NotStarted,
        Running,
        Finished
    }

    public class Match 
    {
        public Guid Id { get; private set; }
        public byte HomeScore { get; private set; }
        public byte AwayScore { get; private set; }
        public DateTime TimeStarted { get; private set; }
        public MatchState State { get; private set; }
        public Team Home { get; }
        public Team Away { get; }

        public Match(Team home, Team away)
        {
            Id = Guid.NewGuid();
            this.Home = home;
            this.Away = away;
        }

        public void SetScore(byte home, byte away)
        {
            if (State!= MatchState.Running)
                throw new InvalidOperationException("Match is not running");

            if (home < HomeScore || away < AwayScore)
                throw new ArgumentException("Cannot set score to lower value");

            HomeScore = home;
            AwayScore = away;
        }

        public void Start()
        {
            if (State != MatchState.NotStarted)
                throw new InvalidOperationException("Match is already running");

            State = MatchState.Running;
            TimeStarted = DateTime.Now;
        }

        public void Finish()
        {
            if (State != MatchState.Running)
                throw new InvalidOperationException("Match is not running");

            State = MatchState.Finished;
        }

        public override string ToString()
        {
            return $"{Home.Name} {HomeScore} - {AwayScore} {Away.Name}";
        }
    }
}
