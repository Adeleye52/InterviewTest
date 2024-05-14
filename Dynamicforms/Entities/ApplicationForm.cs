namespace DynamicForms.Entities;

public class ApplicationForm
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProgramDetailsId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string CurrentResidence { get; set; }
    public string IdNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<Answer> Answers { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}
