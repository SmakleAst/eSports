namespace eSports.Domain.Players.ViewModels
{
    public class CreatePlayerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public int Age { get; set; }
        public string Team { get; set; }
        public string Description { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException(Name, "Укажите имя");
            }

            if (string.IsNullOrWhiteSpace(NickName))
            {
                throw new ArgumentNullException(NickName, "Укажите никнейм");
            }

            if ((Age < 10) && (Age > 100))
            {
                throw new ArgumentNullException(Age.ToString(), "Укажите корректный возраст");
            }

            if (string.IsNullOrWhiteSpace(Team))
            {
                throw new ArgumentNullException(Team, "Укажите команду");
            }
        }
    }
}
