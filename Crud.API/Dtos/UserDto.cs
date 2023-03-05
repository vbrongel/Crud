using System.Text.Json.Serialization;

namespace Crud.API.Dtos
{
    public class UserDto
    {
        [JsonPropertyName("nome")]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("data_nascimento")]
        public DateTime DateOfBirth { get; set; }
    }
}
