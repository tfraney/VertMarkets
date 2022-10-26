using System.Text.Json.Serialization;

namespace MagazineStore.DataEntities
{
    [Serializable]
    public class Answer
    {
        [JsonPropertyName("totalTime")]
        public string TotalTime { get; set; } = string.Empty;

        [JsonPropertyName("answerCorrect")]
        public bool AnswerCorrect { get; set; } = false;

        [JsonPropertyName("shouldBe")]
        public List<string> ShouldBe { get; set; } =  new List<string>();
    }


    [Serializable]
    public class AnswerResultEntity : Entity
    {
        [JsonPropertyName("data")]
        public Answer Data { get; set; } = new Answer();
    }
}