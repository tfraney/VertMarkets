using System.Text.Json.Serialization;

namespace MagazineStore.DataEntities
{
    [Serializable]
    public class Subscriber
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("magazineIds")]
        public List<string> MagazineIds { get; set; } = new List<string>();

    }

    [Serializable]
    public class SubscribersEntity : Entity
    {
        [JsonPropertyName("data")]
        public List<Subscriber> Data { get; set; } = new List<Subscriber>();
    }

    [Serializable]
    public class SubscriberList
    {
        [JsonPropertyName("subscribers")]
        public List<string> Subscribers { get; set; } = new List<string>();

        public SubscriberList(IEnumerable<string>? list) { if (list?.Any()??false) Subscribers.AddRange(list); }
    }
}