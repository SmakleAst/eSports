namespace Stats.Domain.Entity
{
    public class StatsEntity
    {
        public int Id { get; set; }
        public string FirstTeam { get; set; }
        public string SecondTeam { get; set; }
        public double Winrate { get; set; }
    }
}
