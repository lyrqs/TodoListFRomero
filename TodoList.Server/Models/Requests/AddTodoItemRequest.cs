public class AddTodoItemRequest
{
    /// <example>Complete project report</example>
    public string Title { get; set; }

    /// <example>Finish the final document</example>
    public string Description { get; set; }

    /// <example>Work</example>
    /// <remarks>Allowed values: Work, Personal, Shopping, Urgent</remarks>
    public string Category { get; set; }

}
