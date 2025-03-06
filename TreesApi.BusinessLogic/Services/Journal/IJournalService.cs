using TreesApi.BusinessLogic.Models;
using TreesApi.BusinessLogic.Services.Journal.Parameters;
using TreesApi.DataAccess.Models;

namespace TreesApi.BusinessLogic.Services.Journal;

public interface IJournalService
{
    public Task AddEntryAsync(Exception exception, string requestParametersText);
    Task<JournalEntryModel> GetSingleAsync(GetSingleParameters parameters);
    Task<PageableResult<JournalEntryPageableModel>> GetRangeAsync(GetRangeParameters parameters, GetRangeFilterParameters filter);
}
