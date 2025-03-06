using TreesApi.BusinessLogic.Exceptions;
using TreesApi.BusinessLogic.Models;
using TreesApi.BusinessLogic.Providers;

namespace TreesApi.BusinessLogic.Factories;

public class ErrorDetailsFactory : IErrorDetailsFactory
{
    private readonly IEventIdProvider _eventIdProvider;

    public ErrorDetailsFactory(IEventIdProvider eventIdProvider)
    {
        _eventIdProvider = eventIdProvider;
    }
    
    public ErrorDetails GetErrorDetails(Exception exception)
    {
        return exception is SecureException
            ? GetSecureErrorDetails(exception, _eventIdProvider.EventId)
            : GetExceptionErrorDetails(_eventIdProvider.EventId);
    }
    
    private static ErrorDetails GetExceptionErrorDetails(string eventId)
    {
        return GetErrorDetails("Exception", eventId, $"Internal server error ID = {eventId}");
    }

    private static ErrorDetails GetSecureErrorDetails(Exception ex, string eventId)
    {
        var type = ex.GetType().Name;
        if (type.EndsWith("Exception"))
        {
            type = type.Substring(0, type.Length - "Exception".Length);
        }
        return GetErrorDetails(type, eventId, ex.Message);
    }
    
    private static ErrorDetails GetErrorDetails(string type, string eventId, string message)
    {
        return new ErrorDetails
        {
            Type = type,
            Id = eventId,
            Data = new ErrorDetailsData
            {
                Message = message
            }
        };
    }
}
