using AutoMapper;
using DynamicForms.Dtos;
using DynamicForms.Entities;

namespace DynamicForms.Mappers;

public class ProgramDetailsMapper:Profile
{
	public ProgramDetailsMapper()
	{
        CreateMap<ProgramDetail, ProgramDetailsDto>().ReverseMap();
        CreateMap<AddProgramDetails, ProgramDetail>();
        CreateMap<UpdateProgramDetails, ProgramDetail>();
        CreateMap<AddCustomQuestion, CustomQuestion>();
    }
}
