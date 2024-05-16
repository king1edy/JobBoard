namespace JobBoard.Tests
{
    public class ApplicationServiceTests
    {
        [Fact]
        public async Task SubmitApplication_Should_Return_ApplicationFormDto()
        {
            // Arrange
            var mockCosmosDbService = new Mock<ICosmosDbService>();

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>()))
                .Returns(new ConfigurationSection(new Mock<IConfigurationRoot>().Object, "CosmoDB:ContainerName"));

            var mockLogger = new Mock<ILogger<ApplicationService>>();
            var applicationService = new ApplicationService(mockCosmosDbService.Object, mockConfiguration.Object, mockLogger.Object);

            var applicationFormDto = new ApplicationFormDto {  ProgramTitle = "QA test" };
            var applicationForm = new ApplicationForm {  ProgramTitle = applicationFormDto.ProgramTitle };

            mockCosmosDbService.Setup(x => x.CreateItemAsync<ApplicationForm>(It.IsAny<ApplicationForm>(), It.IsAny<string>()))
                               .ReturnsAsync(applicationForm);

            // Act
            var result = await applicationService.SubmitApplication(applicationFormDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(applicationFormDto, result);
        }

        [Fact]
        public async Task GetApplicationById_Should_Return_ApplicationFormDto()
        {
            // Arrange
            var mockCosmosDbService = new Mock<ICosmosDbService>();
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>()))
                .Returns(new ConfigurationSection(new Mock<IConfigurationRoot>().Object, "CosmoDB:ContainerName"));
            var mockLogger = new Mock<ILogger<ApplicationService>>();
            var applicationService = new ApplicationService(mockCosmosDbService.Object, mockConfiguration.Object, mockLogger.Object);

            var applicationFormDto = new ApplicationFormDto { ProgramTitle = "QA Test" };
            var applicationForm = new ApplicationForm { Id = "1", ProgramTitle = applicationFormDto.ProgramTitle };

            mockCosmosDbService.Setup(x => x.GetItemAsync<ApplicationForm>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                               .ReturnsAsync(applicationForm);

            // Act
            var result = await applicationService.GetApplicationById("1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("1", applicationForm.Id);
            Assert.Equal("QA Test", result.ProgramTitle);
        }

        [Fact]
        public async Task GetApplicationById_Should_Return_Null_If_Application_Not_Found()
        {
            // Arrange
            var mockCosmosDbService = new Mock<ICosmosDbService>();
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>()))
                .Returns(new ConfigurationSection(new Mock<IConfigurationRoot>().Object, "CosmoDB:ContainerName"));
            var mockLogger = new Mock<ILogger<ApplicationService>>();
            var applicationService = new ApplicationService(mockCosmosDbService.Object, mockConfiguration.Object, mockLogger.Object);

            mockCosmosDbService.Setup(x => x.GetItemAsync<ApplicationForm>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                               .ReturnsAsync((ApplicationForm)null);

            // Act
            var result = await applicationService.GetApplicationById("1");

            // Assert
            Assert.Null(result);
        }
    }
}
