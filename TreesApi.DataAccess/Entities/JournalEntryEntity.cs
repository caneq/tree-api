namespace TreesApi.DataAccess.Entities;

public class JournalEntryEntity : BaseEntity
{
    public string? EventId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Text { get; set; }
}
