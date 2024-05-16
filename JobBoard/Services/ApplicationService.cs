using JobBoard.Dtos;
using JobBoard.Services.Interface;

namespace JobBoard.Services
{
    public class ApplicationService : IApplicationService
    {
        public async Task<ApplicationFormDto> SubmitApplication(ApplicationFormDto application)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationFormDto> GetApplicationById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
