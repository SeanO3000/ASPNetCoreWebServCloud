using Newtonsoft.Json;

var client = new HttpClient();

client.BaseAddress = new Uri("http://localhost:5171/api/");
client.DefaultRequestHeaders.Add("Accept", "application/json");

// Call Get Token API (token)
var tokenResult = await client.GetAsync("Token/user1%40testuser.com/userPass1");
var tokenJson = await tokenResult.Content.ReadAsStringAsync();
var token = JsonConvert.DeserializeObject<Token>(tokenJson);

// Add the token to the Authorization header (client)
client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.TokenString);

var newUser = new User
{
    Email = "New Name",
    Password = "NewPassword"
};

var newJson = JsonConvert.SerializeObject(newUser);

var postContent = new StringContent(
    newJson,
    System.Text.Encoding.UTF8,
    "application/json");

var postResult = await client.PostAsync("User", postContent);

Console.WriteLine(postResult.StatusCode);

var result = await client.GetAsync("User");

var json = await result.Content.ReadAsStringAsync();

Console.WriteLine(json);

var list = JsonConvert.DeserializeObject<List<User>>(json);

var id = list[list.Count-1].Id;

result = await client.DeleteAsync($"User/{id}");

Console.ReadLine();

public class Token // ADD ME
{
    [JsonProperty("token")]
    public string TokenString { get; set; }
}


public class User
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("date_added")]
    public DateTime DateAdded { get; set; }
}