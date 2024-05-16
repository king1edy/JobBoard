using System.Reflection;
using JobBoard.Dtos;
using JobBoard.Models;

namespace JobBoard.Utility
{
    public static class Common
    {
        public static QuestionDto MapQuesstionToDto(this Question question)
        {
            var questionDto = new QuestionDto()
            {
                Id = question.Id,
                Content = question.Content,
                Type = new QuestionTypeDto()
                {
                    Id = question.Type.Id,
                    Type = question.Type.Type,
                },
                IsRequired = question.IsRequired,
                Options = question.Options.Select(o => new QuestionOptionDto()
                {
                    Id = o.Id,
                    Option = o.Choice
                }).ToList()
            };
            return questionDto;
        }

        public static Question MapDtoToQuestion(this QuestionDto model)
        {
            var question = new Question()
            {
                Id = Guid.NewGuid().ToString(),
                Content = model.Content,
                IsRequired = model.IsRequired,
                Type = new QuestionType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = model.Type.Type,
                },
                Options = model.Options.Select(o => new QuestionOption()
                {
                    Id = Guid.NewGuid().ToString(),
                    Choice = o.Option
                }).ToList()
            };

            return question;
        }

        public static QuestionTypeDto MapQuestionTypeToDto(this QuestionType model)
        {
            return new QuestionTypeDto()
            {
                Id = model.Id,
                Type = model.Type
            };
        }

        public static QuestionType MapDtoToQuestionType(this QuestionTypeDto model)
        {
            return new QuestionType()
            {
                Id = model.Id,
                Type = model.Type
            };
        }
    }
}
