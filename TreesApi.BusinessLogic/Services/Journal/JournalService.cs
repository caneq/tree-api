using System.Linq.Expressions;
using AutoMapper;
using TreesApi.BusinessLogic.Models;
using TreesApi.BusinessLogic.Providers;
using TreesApi.BusinessLogic.Services.Journal.Parameters;
using TreesApi.DataAccess.Entities;
using TreesApi.DataAccess.Models;
using TreesApi.DataAccess.UnitsOfWork;

namespace TreesApi.BusinessLogic.Services.Journal;

public class JournalService : IJournalService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventIdProvider _eventIdProvider;
    private readonly IMapper _mapper;

    public JournalService(IUnitOfWork unitOfWork, IEventIdProvider eventIdProvider, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _eventIdProvider = eventIdProvider;
        _mapper = mapper;
    }

    public async Task<PageableResult<JournalEntryPageableModel>> GetRangeAsync(GetRangeParameters parameters, GetRangeFilterParameters? filter)
    {
        Expression<Func<JournalEntryEntity, bool>>? filterExpression = null;
        if (filter != null)
        {
            filterExpression = e =>
                (filter.Search == null || e.Text.Contains(filter.Search))
                && (filter.To == null || e.CreatedAt <= filter.To)
                && (filter.From == null || e.CreatedAt >= filter.From);
        }
        
        var result = await _unitOfWork.JournalExceptionEntityRepository.GetPageableAsync(parameters.Skip, parameters.Take, filterExpression);
        return _mapper.Map<PageableResult<JournalEntryPageableModel>>(result);
    }

    public async Task<JournalEntryModel> GetSingleAsync(GetSingleParameters parameters)
    {
        var entry = await _unitOfWork.JournalExceptionEntityRepository.GetByIdAsync(parameters.Id);
        return _mapper.Map<JournalEntryModel>(entry);
    }

    public async Task AddEntryAsync(Exception exception, string requestParametersText)
    {
        var newEntry = new JournalEntryEntity
        {
            EventId = _eventIdProvider.EventId,
            CreatedAt = DateTime.UtcNow,
            Text = $"""
                    EventId = {_eventIdProvider.EventId}
                    {requestParametersText}
                    StackTrace:
                    {exception.StackTrace}
                    """
        };
        await _unitOfWork.JournalExceptionEntityRepository.InsertAsync(newEntry);
        await _unitOfWork.SaveAsync();
    }
}
