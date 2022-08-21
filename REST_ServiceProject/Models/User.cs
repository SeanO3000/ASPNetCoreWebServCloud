using System.Text.Json.Serialization;

namespace REST_ServiceProject.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("date_added")]
        public DateTime DateAdded { get; set; }

        public User(int id, string email, string password)
        {
            this.Id = id;
            this.Email = email;
            this.Password = password;
            this.DateAdded = DateTime.Now;
        }
    }
}
