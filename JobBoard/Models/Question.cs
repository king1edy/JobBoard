using Newtonsoft.Json;

namespace JobBoard.Models
{
    public class Question
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty(PropertyName = "isRequired")]
        public bool IsRequired { get; set; }

        [JsonProperty("type")]
        public QuestionType Type { get; set; }  // Paragraph, YesNo, Dropdown, etc.
        
        [JsonProperty("options")]
        public List<QuestionOption>? Options { get; set; } // Used for Dropdown/MultipleChoice

    }
}
