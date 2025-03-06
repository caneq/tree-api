namespace TreesApi.BusinessLogic.Models;

public class JournalEntryPageableModel
{
    public long? Id { get; set; }

    public string? EventId { get; set; }

    public DateTime CreatedAt { get; set; }
}
