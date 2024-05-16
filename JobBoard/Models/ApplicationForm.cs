using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace JobBoard.Models
{
    public class ApplicationForm
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [JsonProperty(PropertyName = "formId")]
        public string FormId { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty(PropertyName = "programTitle")]
        public string ProgramTitle { get; set; }

        [JsonProperty(PropertyName = "programDescription")]
        public string ProgramDescription { get; set; }
        
        [JsonProperty(PropertyName = "questions")]
        public List<Question> Questions { get; set; }

        [JsonProperty(PropertyName = "additionalQuestions")]
        public List<Question>? AdditionalQuestions { get; set; }
    }
}
