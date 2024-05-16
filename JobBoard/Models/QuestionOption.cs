using Newtonsoft.Json;

namespace JobBoard.Models
{
    public class QuestionOption
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "choice")]
        public string Choice { get; set; }
    }
}
