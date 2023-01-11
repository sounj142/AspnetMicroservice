namespace Ordering.Api.Models;

public class ValidationError
{
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}