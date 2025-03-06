namespace TreesApi.BusinessLogic.Models;

public class JournalEntryModel
{
    public long? Id { get; set; }

    public string? EventId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Text { get; set; }
}
