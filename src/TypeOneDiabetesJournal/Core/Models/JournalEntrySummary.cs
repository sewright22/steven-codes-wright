namespace Core.Models;

public class JournalEntrySummary
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? CarbCount { get; set; }
    public List<string>? Tags { get; set; }
}