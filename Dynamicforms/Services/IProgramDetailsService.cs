using DynamicForms.Dtos;
using DynamicForms.Entities;
using DynamicForms.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DynamicForms.Services
{
    public interface IProgramDetailsService
    {
        Task<SuccessResponse<ProgramDetailsDto>> AddProgramDetail(AddProgramDetails model);
        Task<SuccessResponse<ProgramDetailsDto>> UpdateProgramDetail(Guid id, UpdateProgramDetails model);
        Task<SuccessResponse<ProgramDetailsDto>> GetProgramDetailById(Guid id);
        Task<PagedResponse<IEnumerable<ProgramDetailsDto>>> GetProgramDetails(ResourceParameter parameter, string name, IUrlHelper urlHelper);
        Task<SuccessResponse<IEnumerable<CustomQuestion>>> GetQuestionByType(Guid programDetailsId, string type);
    }
}