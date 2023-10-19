using eSports.Domain.Players.Entity;

namespace eSports.Domain.Teams.ViewModels
{
    public class CreateTeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public List<PlayerEntity> Players { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException(Name, "Укажите имя");
            }

            if (string.IsNullOrWhiteSpace(Country))
            {
                throw new ArgumentNullException(Country, "Укажите страну");
            }

            //if (Players.Count < 1)
            //{
            //    throw new ArgumentNullException(Players.ToString(), "Укажите игроков");
            //}
        }
    }
}
