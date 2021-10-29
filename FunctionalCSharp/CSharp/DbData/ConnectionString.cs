namespace CSharp.Lessons.DbData;

public record ConnectionString : OpenSetBase<ConnectionString, string, ErrorData>
{
    /// <summary>
    /// For simplicity of the example, any values are allowed so far.
    /// </summary>
    public ConnectionString(string value) : base(value)
    {
    }
}
