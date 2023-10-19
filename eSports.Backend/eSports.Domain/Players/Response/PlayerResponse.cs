using eSports.Domain.Enum;
using eSports.Domain.Teams.Entity;

namespace eSports.Backend.Domain.Players.Response
{
    public class PlayerResponse<T> : IPlayerResponse<T>
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public int Age { get; set; }
        public TeamEntity Team { get; set; }
        public string Description { get; set; }
        public StatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }

    public interface IPlayerResponse<T>
    {
        string Name { get; }
        string NickName { get; }
        int Age { get; }
        TeamEntity Team { get; }
        string Description { get; }
        public StatusCode StatusCode { get; }
        T Data { get; }
    }
}
