using eSports.Domain.Players.Entity;

namespace eSports.Domain.Teams.ViewModels
{
    public class CreateTeamViewModel
    {
        public string Name { get; set; }
        public string Country { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException(Name, "Укажите название");
            }

            if (string.IsNullOrWhiteSpace(Country))
            {
                throw new ArgumentNullException(Country, "Укажите страну");
            }
        }
    }
}
