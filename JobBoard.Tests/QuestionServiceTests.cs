using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Dtos;
using JobBoard.Models;
using JobBoard.Services;
using JobBoard.Services.Interface;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace JobBoard.Tests
{
    public class QuestionServiceTests
    {
        [Fact]
        public async Task CreateQuestion_Should_Return_New_Question()
        {
            //// Arrange
            //var mockCosmosDbService = new Mock<ICosmosDbService>();
            //var mockConfig = new Mock<IConfiguration>();
            //var mockLogger = new Mock<ILogger<QuestionService>>();
            //var questionService = new QuestionService(mockCosmosDbService.Object, mockConfig.Object, mockLogger.Object);
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
        public async Task GetAllItemsAsync_Should_Return_All_Questions()
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
            
            // Act
            var result = await questionService.GetAllItemsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Question>>(result);
            // Add more assertions as needed
        }
    }
}
