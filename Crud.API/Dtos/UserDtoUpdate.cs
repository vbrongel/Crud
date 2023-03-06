using System.Text.Json.Serialization;

namespace Crud.API.Dtos
{
    public class UserDtoUpdate
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nome")]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }

        public UserDtoUpdate(int id, string name, string email)
        {
            Id  = id;
            Name = name;
            Email = email;
        }
    }
}
