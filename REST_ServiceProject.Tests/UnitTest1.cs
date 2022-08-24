using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace REST_ServiceProject.Tests
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
    }

    public class Tests
    {
        HttpClient client;

        // Called before every Test
        [SetUp]
        public void Setup()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5171/api/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        [Test]
        public async Task AddNewUser()
        {
            // Call Get Token API (token)
            var tokenResult = await client.GetAsync("Token/user1%40testuser.com/userPass1");
            var tokenJson = await tokenResult.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<Token>(tokenJson);

            // Add the token to the Authorization header (client)
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.TokenString);

            var newUser = new User
            {
                Email = "unitTestUser@testuser.com",
                Password = "UnitPass1"
            };

            var newJson = System.Text.Json.JsonSerializer.Serialize(newUser);

            var postContent = new StringContent(newJson,
                Encoding.UTF8, "application/json");

            var postResult = await client.PostAsync("user", postContent);

            Assert.AreEqual(HttpStatusCode.Created, postResult.StatusCode);
        }

        [Test]
        public async Task DeleteUser()
        {
            var result = await client.GetAsync("User");

            var json = await result.Content.ReadAsStringAsync();

            var list = JsonConvert.DeserializeObject<List<User>>(json);

            var id = list[list.Count - 1].Id;

            result = await client.DeleteAsync($"User/{id}");

            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }
    }
    public class Token
    {
        [JsonProperty("token")]
        public string TokenString { get; set; }
    }
}