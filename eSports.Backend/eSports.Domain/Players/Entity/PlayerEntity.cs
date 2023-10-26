using eSports.Domain.Teams.Entity;

namespace eSports.Domain.Players.Entity
{
    public class PlayerEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public int Age { get; set; }
        public int TeamId { get; set; }
        public TeamEntity Team { get; set; }
        public string Description { get; set; }
    }
}
