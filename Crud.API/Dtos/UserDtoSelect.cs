using System.Text.Json.Serialization;

namespace Crud.API.Dtos
{
    public class UserDtoSelectOrDelete
    {
        [JsonPropertyName("nome")]
        public string Email { get; set; }

        public UserDtoSelectOrDelete(string email)
        {
            Email = email;
        }
    }
}
