namespace JobBoard.Dtos
{
    public class QuestionDto
    {
        public string? Id { get; set; }
        public string Content { get; set; }
        public bool IsRequired { get; set; } = false;
        public QuestionTypeDto Type { get; set; }
        public List<QuestionOptionDto>? Options { get; set; }
    }

}
