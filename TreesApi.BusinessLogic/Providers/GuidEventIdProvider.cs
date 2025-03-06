namespace TreesApi.BusinessLogic.Providers;

public class GuidEventIdProvider : IEventIdProvider
{
    public string EventId { get; } = Guid.NewGuid().ToString();
}
