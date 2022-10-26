using System.Text.Json.Serialization;

namespace MagazineStore.DataEntities
{
    [Serializable]
    public class Entity
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; } = false;

        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}