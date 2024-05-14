using DynamicForms.Entities;

namespace DynamicForms.Dtos;

public record ProgramDetailsDto
{
    public Guid Id { get; set; }
    public string ProgramTitle { get; set; }
    public string ProgramDescription { get; set; }
    public bool IsPhoneInternal { get; set; }
    public bool IsPhoneVisible { get; set; }
    public bool IsCurrentResidenceInternal { get; set; }
    public bool IsCurrentResidenceVisible { get; set; }
    public bool IsIdNumberInternal { get; set; }
    public bool IsIdNumberVisible { get; set; }
    public bool IsDateOfBirthInternal { get; set; }
    public bool IsDateOfBirthVisible { get; set; }
    public IEnumerable<CustomQuestion> CustomQuestions { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public record AddProgramDetails
{
    public string ProgramTitle { get; set; }
    public string ProgramDescription { get; set; }
    public bool IsPhoneInternal { get; set; }
    public bool IsPhoneVisible { get; set; }
    public bool IsCurrentResidenceInternal { get; set; }
    public bool IsCurrentResidenceVisible { get; set; }
    public bool IsIDNumberInternal { get; set; }
    public bool IsIDNumberVisible { get; set; }
    public bool IsDateOfBirthInternal { get; set; }
    public bool IsDateOfBirthVisible { get; set; }
    public IEnumerable<AddCustomQuestion> CustomQuestions { get; set; }
}

public record UpdateProgramDetails : AddProgramDetails { }
public record AddCustomQuestion
{
    public string Question { get; set; }
    public string Type { get; set; }
    public List<Choice> Choices { get; set; } // For Dropdown and MultipleChoice types
    public int MaxChoicesAllowed { get; set; } // For MultipleChoice type

}