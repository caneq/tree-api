namespace TreesApi.BusinessLogic.Services.Journal.Parameters;

public record GetRangeFilterParameters(DateTime? From, DateTime? To, string? Search);
