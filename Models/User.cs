namespace POLL.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Token { get; set; } // Add this line for the Token property

    }
}
