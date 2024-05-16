namespace JobBoard.Dtos
{
    public class ApplicationFormDto
    {
        public string ProgramTitle { get; set; }
        public string ProgramDescription { get; set; }
        public List<QuestionDto> Questions { get; set; }
        public List<QuestionDto>? AdditionalQuestions { get; set; }
    }
}
