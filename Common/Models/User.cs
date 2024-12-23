namespace Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Login { get; set; } = "Guest";
        public string PasswordHash { get; set; } = "";
        public string Salt { get; set; } = "";
        public List<Task> Tasks { get; set; } = new List<Task>();
    }
}
