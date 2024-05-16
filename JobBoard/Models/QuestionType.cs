using Newtonsoft.Json;

namespace JobBoard.Models
{
    public class QuestionType
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
