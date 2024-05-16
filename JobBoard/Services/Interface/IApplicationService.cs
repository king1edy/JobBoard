using JobBoard.Dtos;

namespace JobBoard.Services.Interface
{
    public interface IApplicationService
    {
        Task<ApplicationFormDto> SubmitApplication(ApplicationFormDto application);
        Task<ApplicationFormDto> GetApplicationById(string id);
    }
}
