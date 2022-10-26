using System.Text.Json.Serialization;

namespace MagazineStore.DataEntities
{
   
    [Serializable]
    public class CategoriesEntity : Entity
    {
        [JsonPropertyName("data")]
        public List<string> Data { get; set; } = new List<string>();
    }
}