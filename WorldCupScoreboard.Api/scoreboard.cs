

namespace WorldCupScoreboard.Api
{
    public class Score
    {
        public byte Home { get; set; }
        public byte Away { get; set; }
    }

    public class ScoreboardFactory
    {
        public static Scoreboard Create()
        {
            const string argentina = "Argentina";
            const string brasil = "Brazil";
            const string moldova = "Moldova";

            var competition = new Scoreboard(); 
            var argFra = new Match(new Team(argentina), new Team("France"), MatchStage.Group);
            competition.RegisterMatch(argFra);

            var braCro = new Match(new Team(brasil), new Team("Croatia"));
            competition.RegisterMatch(braCro);

            var mdlPol = new Match(new Team(moldova), new Team("Poland"));
            competition.RegisterMatch(mdlPol);

            argFra.Start();
            braCro.Start();
            mdlPol.Start();

            argFra.SetScore(1, 0);
            mdlPol.SetScore(50, 17);
            braCro.SetScore(1, 0);
            return competition;
        }
    }
}
