namespace JobBoard.Dtos
{
    public class QuestionOptionDto
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        [JsonProperty(PropertyName = "choice")]
        public string Choice { get; set; }
    }
}
