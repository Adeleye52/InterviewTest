using AutoMapper;
using Azure.Core;
using DynamicForms.Data;
using DynamicForms.Dtos;
using DynamicForms.Entities;
using DynamicForms.Entities.Enums;
using DynamicForms.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DynamicForms.Services;

public class ApplicationFormService: IApplicationFormService
{
    private readonly IRepository<ApplicationForm> _applicationFormRepository;
    private readonly IRepository<ProgramDetail> _programDetailsRepository;
    private readonly IMapper _mapper;
    public ApplicationFormService(IRepository<ApplicationForm> applicationFormRepository, IMapper mapper, IRepository<ProgramDetail> programDetailsRepository)
    {
        _applicationFormRepository = applicationFormRepository;
        _mapper = mapper;
        _programDetailsRepository = programDetailsRepository;
    }
    public async Task<SuccessResponse<ApplicationFormDto>> SubmitApplicationForm(CreateApplicationForm model)
    {
        if (model == null)
            throw new RestException(HttpStatusCode.BadRequest, "Form cannot be null or empty.");

        if (model.ProgramDetailsId == Guid.Empty)
            throw new RestException(HttpStatusCode.BadRequest, "Program details Id cannot be null or empty");

        var programDetails = await _programDetailsRepository.GetByIdAsync(model.ProgramDetailsId);
        if (programDetails == null)
            throw new RestException(HttpStatusCode.NotFound, "Program Details not foud");

        if (programDetails.CustomQuestions != null && model.Answers != null)
        {
            foreach (var answer in model.Answers)
            {
                var question = programDetails.CustomQuestions.FirstOrDefault(x => x.Id == answer.QuestionId);

                if (question == null)
                    throw new RestException(HttpStatusCode.NotFound, $"Question not found for Id: {answer.QuestionId}");

                if (question.Type == EQuestionType.MultipleChoice.ToString().ToLower())
                {
                    if (answer.SelectedChoices == null || answer.SelectedChoices.Count == 0)
                    {
                        throw new RestException(HttpStatusCode.BadRequest, "At least one choice must be selected for multiple-choice questions");
                    }

                    if (answer.SelectedChoices.Count > question.MaxChoicesAllowed)
                    {
                        throw new RestException(HttpStatusCode.BadRequest, $"Maximum {question.MaxChoicesAllowed} choices allowed for this question");
                    }

                }
            }
        }

        var applicationForm = _mapper.Map<ApplicationForm>(model);
        await _applicationFormRepository.CreateAsync(applicationForm);
        var response = _mapper.Map<ApplicationFormDto>(applicationForm);

        return new SuccessResponse<ApplicationFormDto>()
        {
            Data = response,
            Message = "Request Created Successfully"
        };
    }


    public async Task<SuccessResponse<ApplicationFormDto>> GetApplicationFormById(Guid id)
    {
        var applicationForm = await _applicationFormRepository.GetByIdAsync(id);
        var response = _mapper.Map<ApplicationFormDto>(applicationForm);

        return new SuccessResponse<ApplicationFormDto>()
        {
            Data = response,
            Message = "Request Updated Successfully"
        };
    }
    public async Task<bool> ValidateDetails(ProgramDetail programDetail, CreateApplicationForm model)
    {
        if(programDetail.)
    }

}
