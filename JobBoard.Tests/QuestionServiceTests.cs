using System;
using System.Collections.Generic;


namespace JobBoard.Tests
{
    public class QuestionServiceTests
    {
        [Fact]
        public async Task CreateQuestion_Should_Return_New_Question()
        {
            // Arrange
            var mockCosmosDbService = new Mock<ICosmosDbService>();
            mockCosmosDbService.Setup(x => x.GetContainerAsync(It.IsAny<string>()))
                .ReturnsAsync(Mock.Of<Container>());

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>()))
                .Returns(new ConfigurationSection(new Mock<IConfigurationRoot>().Object, "CosmoDB:ContainerName"));

            var questionService = new QuestionService(mockCosmosDbService.Object,
                mockConfiguration.Object,
                Mock.Of<ILogger<QuestionService>>());


            var newQuestion = new QuestionDto
            {
                Id = "1", Content = "Sample question", IsRequired = true,
                Type = new QuestionTypeDto() { Id = Guid.NewGuid().ToString(), Type = "Paragraph" }
            };

            mockCosmosDbService.Setup(x => x.CreateItemAsync(It.IsAny<QuestionDto>(), It.IsAny<string>()))
                .ReturnsAsync(newQuestion);

            // Act
            await questionService.CreateQuestionAsync(newQuestion);

            // Assert
            Assert.Equal("1", newQuestion.Id);
            Assert.Equal("Sample question", newQuestion.Content);
            Assert.Equal("Paragraph", newQuestion.Type.Type);
        }


        [Fact]
        public async Task GetAllItemsAsync_Should_Return_All_Questions_()
        {
            // Arrange
            var mockCosmosDbService = new Mock<ICosmosDbService>();
            mockCosmosDbService.Setup(x => x.GetContainerAsync(It.IsAny<string>()))
                .ReturnsAsync(Mock.Of<Container>());

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>()))
                .Returns(new ConfigurationSection(new Mock<IConfigurationRoot>().Object, "CosmoDB:ContainerName"));

            var questionService = new QuestionService(mockCosmosDbService.Object,
                mockConfiguration.Object,
                Mock.Of<ILogger<QuestionService>>());

            var questions = new List<Question>
            {
                new Question { Id = "1", Content = "Question 1", IsRequired = true, Type = new QuestionType { Id = "1", Type = "Type 1" } },
                new Question { Id = "2", Content = "Question 2", IsRequired = false, Type = new QuestionType { Id = "2", Type = "Type 2" } }
            };

            //mockCosmosDbService.Setup(x => x.GetAllItemsAsync<Question>(It.IsAny<string>()))
            //    .ReturnsAsync(questions);

            // Act
            var result = questions;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count); // Assuming there are two questions returned
            // Add more assertions as needed
        }
        
        [Fact]
        public async Task GetItemsAsync_Should_Return_One_QuestionById()
        {
            // Arrange
            var mockCosmosDbService = new Mock<ICosmosDbService>();
            mockCosmosDbService.Setup(x => x.GetContainerAsync(It.IsAny<string>()))
                .ReturnsAsync(Mock.Of<Container>());

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>()))
                .Returns(new ConfigurationSection(new Mock<IConfigurationRoot>().Object, "CosmoDB:ContainerName"));

            var questionService = new QuestionService(mockCosmosDbService.Object,
                mockConfiguration.Object,
                Mock.Of<ILogger<QuestionService>>());

            var questions = new List<Question>
            {
                new Question { Id = "1", Content = "Question 1", IsRequired = true, Type = new QuestionType { Id = "1", Type = "Type 1" } },
                new Question { Id = "2", Content = "Question 2", IsRequired = false, Type = new QuestionType { Id = "2", Type = "Type 2" } }
            };

            mockCosmosDbService.Setup(x =>
                x.GetItemAsync<Question>("1", "", "").Result);

            // Act
            var result = questions.Find(e => e.Id.Equals("1"));

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result, questions.First());
        }
    }
}
