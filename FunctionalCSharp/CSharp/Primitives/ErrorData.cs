namespace CSharp.Lessons.Primitives;

/// <summary>
/// I don't want to go into a rabbit hole here for the time being...
/// </summary>
public record ErrorData(string ErrorMessage)
{
    public static ErrorData GetValueCannotBeNull(string className) =>
        new ErrorData($"Value of {className} cannot be null.");
}
