using Newtonsoft.Json;

namespace JobBoard.Dtos
{
    public class QuestionTypeDto
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        public string Type { get; set; }
    }
}
