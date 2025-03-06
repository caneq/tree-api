using System.Text;
using TreesApi.BusinessLogic.Factories;
using TreesApi.BusinessLogic.Services.Journal;

namespace TreesApi.Web.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IJournalService _journalService;
    private readonly IErrorDetailsFactory _errorDetailsFactory;
    
    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, IJournalService journalService, IErrorDetailsFactory errorDetailsFactory)
    {
        _logger = logger;
        _journalService = journalService;
        _errorDetailsFactory = errorDetailsFactory;
    }
 
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            var errorDetails = _errorDetailsFactory.GetErrorDetails(ex);
            await AddJournalEntry(ex, context);
            await context.Response.WriteAsJsonAsync(errorDetails);
        }
    }

    private async Task AddJournalEntry(Exception ex, HttpContext context)
    {
        try
        {
            var requestParametersText = await GetJournalEntryRequestParametersText(context);
            await _journalService.AddEntryAsync(ex, requestParametersText);
        }
        catch (Exception journalAddingException)
        {
            _logger.LogError(journalAddingException, "Failed to add journal entry");
        }
    }

    private static async Task<string> GetJournalEntryRequestParametersText(HttpContext context)
    {
        var requestParameters = new StringBuilder();
        requestParameters.Append("Path = ");
        requestParameters.AppendLine(context.Request.Path.ToString());

        foreach (var queryEntry in context.Request.Query)
        {
            requestParameters.Append(queryEntry.Key);
            requestParameters.Append('=');
            requestParameters.AppendLine(queryEntry.Value.ToString());
        }
  
        requestParameters.Append("Body = ");
        var requestBody = await GetRequestBody(context);
        requestParameters.AppendLine(requestBody);
        return requestParameters.ToString();
    }

    private static async Task<string> GetRequestBody(HttpContext context)
    {
        context.Request.EnableBuffering();
        var bodyStream = new StreamReader(context.Request.Body);
        bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
        var bodyText = await bodyStream.ReadToEndAsync();
        return bodyText;
    }
}
