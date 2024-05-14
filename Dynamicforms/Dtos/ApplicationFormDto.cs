using DynamicForms.Entities;
using System.ComponentModel.DataAnnotations;

namespace DynamicForms.Dtos
{
    public record ApplicationFormDto: CreateApplicationForm
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public record CreateApplicationForm
    {
        public Guid ProgramDetailsId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CurrentResidence { get; set; }
        public string IdNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Answer> Answers { get; set; }

    }

    public record UpdateApplicationForm : CreateApplicationForm
    {

    }
}
