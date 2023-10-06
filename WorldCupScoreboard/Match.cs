using System;

namespace WorldCupScoreboard
{
    public enum MatchState
    {
        NotStarted,
        Running,
        Finished
    }

    public enum MatchStage{
        Group,
        PlayOff
    }

    public class Match 
    {
        public string Id { get {
            return $"{Stage}.{Home.Name}-{Away.Name}" ;
        } }
        public byte HomeScore { get; private set; }
        public byte AwayScore { get; private set; }
        public DateTime TimeStarted { get; private set; }
        public MatchState State { get; private set; }
        public Team Home { get; }
        public Team Away { get; }                         
        public MatchStage Stage{ get; set; }                    

        public Match(Team home, Team away, MatchStage stage = MatchStage.Group)
        {            
            this.Home = home;
            this.Away = away;
            this.Stage = stage;
        }


        // Set absolute score (pretend that penalties are not a thing)
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
            return Id;
        }
    }
}
