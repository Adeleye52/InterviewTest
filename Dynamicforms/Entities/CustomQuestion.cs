namespace DynamicForms.Entities;
public class CustomQuestion
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Question { get; set; }
    public string Type { get; set; }
    public List<Choice> Choices { get; set; } // For Dropdown and MultipleChoice types
    public int MaxChoicesAllowed { get; set; } // For MultipleChoice type
}
public class Choice
{
    public string Value { get; set; }
    public string Text { get; set; }
}
public class Answer
{
    public Guid QuestionId { get; set; }
    public string AnswerText { get; set; }
    public List<string> SelectedChoices { get; set; }
    public string OtherChoiceText { get; set; }
}