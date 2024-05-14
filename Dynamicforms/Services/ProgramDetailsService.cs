using AutoMapper;
using DynamicForms.Data;
using DynamicForms.Dtos;
using DynamicForms.Entities;
using DynamicForms.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DynamicForms.Services;

public class ProgramDetailsService: IProgramDetailsService
{
    private readonly IRepository<ProgramDetail> _programDetailsRepository;
    private readonly IMapper _mapper;

    public ProgramDetailsService(IMapper mapper, IRepository<ProgramDetail> programDetailsRepository)
    {
        _mapper = mapper;
        _programDetailsRepository = programDetailsRepository;
    }

    public async Task<SuccessResponse<ProgramDetailsDto>> AddProgramDetail(AddProgramDetails model)
    {
        var programDetail = _mapper.Map<ProgramDetail>(model);
        await _programDetailsRepository.CreateAsync(programDetail);
        var response = _mapper.Map<ProgramDetailsDto>(programDetail);

        return new SuccessResponse<ProgramDetailsDto>()
        {
            Data = response,
            Message = "Request Created Successfully"
        };
    }

    public async Task<SuccessResponse<ProgramDetailsDto>> UpdateProgramDetail(Guid id, UpdateProgramDetails model)
    {
        var programDetail = await _programDetailsRepository.GetByIdAsync(id);
        _mapper.Map(model, programDetail);
        programDetail.UpdatedAt = DateTime.Now;
        await _programDetailsRepository.UpdateAsync(id, programDetail);
        var response = _mapper.Map<ProgramDetailsDto>(programDetail);

        return new SuccessResponse<ProgramDetailsDto>()
        {
            Data = response,
            Message = "Request Updated Successfully"
        };
    }

    public async Task<SuccessResponse<ProgramDetailsDto>> GetProgramDetailById(Guid id)
    {
        var programDetail = await _programDetailsRepository.GetByIdAsync(id);

        if (programDetail == null)
            throw new RestException(HttpStatusCode.NotFound, "Program detail not found");

        var response = _mapper.Map<ProgramDetailsDto>(programDetail);
        return new SuccessResponse<ProgramDetailsDto>()
        {
            Data = response,
            Message = "Request Updated Successfully"
        };
    }

    public async Task<PagedResponse<IEnumerable<ProgramDetailsDto>>> GetProgramDetails(ResourceParameter parameter, string name, IUrlHelper urlHelper)
    {
        var query = await _programDetailsRepository.GetAllAsync();
        query = query.OrderByDescending(x => x.CreatedAt);

        var programDetailsQuery = _mapper.Map<IList<ProgramDetailsDto>>(query);
        var programDetails = await PagedList<ProgramDetailsDto>.Create(programDetailsQuery, parameter.PageNumber, parameter.PageSize, parameter.Sort);
        var programDetailsParameters = PageUtility<ProgramDetailsDto>.GenerateResourceParameters(parameter, programDetails);
        var page = PageUtility<ProgramDetailsDto>.CreateResourcePageUrl(programDetailsParameters, name, programDetails, urlHelper);

        return new PagedResponse<IEnumerable<ProgramDetailsDto>>
        {
            Message = "Data retrieved successfully",
            Data = programDetails,
            Meta = new Meta
            {
                Pagination = page
            }
        };

    }

    public async Task<SuccessResponse<IEnumerable<CustomQuestion>>> GetQuestionByType(Guid programDetailsId, string type)
    {
        var programDetail = await _programDetailsRepository.GetByIdAsync(programDetailsId);

        if (programDetail == null)
            throw new RestException(HttpStatusCode.NotFound, "Program detail not found");

        var questions = programDetail.CustomQuestions.Where(x => x.Type == type).ToList();

        return new SuccessResponse<IEnumerable<CustomQuestion>>()
        {
            Data = questions,
            Message = "Data retrieved successfully"
        };
    }
}
