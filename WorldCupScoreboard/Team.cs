namespace WorldCupScoreboard
{
    // Represents a team
    // It is enough to identify it by name based on the requirements    
    public class Team
    {
        public string Name { get; private set; }

        public Team(string name)
        {
            Name = name;
        }     
    }
}
