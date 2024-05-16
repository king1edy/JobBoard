using System.Reflection;
using JobBoard.Dtos;
using JobBoard.Models;
using static System.Net.Mime.MediaTypeNames;

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
                Type = model.Type is null ? null : new QuestionType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = model.Type.Type,
                },
                Options = model.Options is null ? null : model.Options.Select(o => new QuestionOption()
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
                Id = Guid.NewGuid().ToString(),
                Type = model.Type
            };
        }

        public static List<QuestionTypeDto> MapQuestionTypesToDtos(this List<QuestionType> models)
        {
            return models.Select(o => new QuestionTypeDto()
            {
                Id = o.Id,
                Type = o.Type,
            }).ToList();
        }

        public static ApplicationForm MapDtoToApplicationForm(this ApplicationFormDto model)
        {
            var form = new ApplicationForm()
            {
                Id = Guid.NewGuid().ToString(),
                FormId = Guid.NewGuid().ToString(),
                ProgramTitle = model.ProgramTitle,
                ProgramDescription = model.ProgramDescription,
                Questions = model.Questions.Select(q => new Question()
                {
                    Id = Guid.NewGuid().ToString(),
                    IsRequired = q.IsRequired,
                    Content = q.Content,
                    Type = new QuestionType()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Type = q.Type.Type,
                    },
                    Options = q.Options is null ? null : q.Options.Select(op => new QuestionOption()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Choice = op.Option
                    }).ToList(),
                }).ToList(),
                AdditionalQuestions = model.AdditionalQuestions.Select(q => new Question()
                {
                    Id = Guid.NewGuid().ToString(),
                    IsRequired = q.IsRequired,
                    Content = q.Content,
                    Type = new QuestionType()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Type = q.Type.Type,
                    },
                    Options = q.Options is null ? null : q.Options.Select(o => new QuestionOption()
                    {
                        Id = o.Id,
                        Choice = o.Option

                    }).ToList()
                }).ToList()
            };

            return form;
        }

        public static ApplicationFormDto MapDtoToApplicationForm(this ApplicationForm model)
        {
            var formDto = new ApplicationFormDto()
            {
                ProgramTitle = model.ProgramTitle,
                ProgramDescription = model.ProgramDescription,
                Questions = model.Questions.Select(q => new QuestionDto()
                {
                    Id = q.Id,
                    Content = q.Content,
                    Type = new QuestionTypeDto()
                    {
                        Id = q.Type.Id,
                        Type = q.Type.Type,
                    },
                    IsRequired = q.IsRequired,
                    Options = q.Options.Select(o => new QuestionOptionDto()
                    {
                        Id = o.Id,
                        Option = o.Choice
                    }).ToList()
                }).ToList(),
                AdditionalQuestions = model.AdditionalQuestions.Select(q => new QuestionDto()
                {
                    Id = q.Id,
                    Content = q.Content,
                    Type = new QuestionTypeDto()
                    {
                        Id = q.Type.Id,
                        Type = q.Type.Type,
                    },
                    IsRequired = q.IsRequired,
                    Options = q.Options.Select(o => new QuestionOptionDto()
                    {
                        Id = o.Id,
                        Option = o.Choice
                    }).ToList()
                }).ToList()
            };

            return formDto;
        }
    }
}
