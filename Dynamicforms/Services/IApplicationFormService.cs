using DynamicForms.Dtos;
using DynamicForms.Helpers;

namespace DynamicForms.Services;

public interface IApplicationFormService
{
    Task<SuccessResponse<ApplicationFormDto>> SubmitApplicationForm(CreateApplicationForm model);
    Task<SuccessResponse<ApplicationFormDto>> GetApplicationFormById(Guid id);
}