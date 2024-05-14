namespace DynamicForms.Entities
{
    public class ProgramDetail
    {
        public Guid Id { get; set; } = Guid.NewGuid();
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
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
