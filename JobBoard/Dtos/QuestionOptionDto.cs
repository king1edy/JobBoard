using Newtonsoft.Json;

namespace JobBoard.Dtos
{
    public class QuestionOptionDto
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        [JsonProperty(PropertyName = "option")]
        public string Option { get; set; }
    }
}
