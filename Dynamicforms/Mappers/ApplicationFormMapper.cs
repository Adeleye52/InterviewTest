using AutoMapper;
using DynamicForms.Dtos;
using DynamicForms.Entities;

namespace DynamicForms.Mappers;

public class ApplicationFormMapper:Profile
{
	public ApplicationFormMapper()
	{

        CreateMap<ApplicationForm, ApplicationFormDto>().ReverseMap();
        CreateMap<CreateApplicationForm, ApplicationForm>();
        CreateMap<UpdateApplicationForm, ApplicationForm>();
    }
}
