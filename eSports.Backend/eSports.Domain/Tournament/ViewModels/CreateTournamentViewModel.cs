namespace eSports.Domain.Tournament.ViewModels
{
    public class CreateTournamentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Teams { get; set; }
    }
}
