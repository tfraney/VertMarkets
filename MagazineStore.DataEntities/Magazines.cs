using System.Text.Json.Serialization;

namespace MagazineStore.DataEntities
{
    [Serializable]
    public class Magazine
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

    }

    [Serializable]
    public class MagazinesEntity : Entity
    {
        [JsonPropertyName("data")]
        public List<Magazine> Data { get; set; }  = new List<Magazine>();

        public MagazinesEntity() { }
    }
}