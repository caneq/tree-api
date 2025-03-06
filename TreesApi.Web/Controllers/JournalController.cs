using Microsoft.AspNetCore.Mvc;
using TreesApi.BusinessLogic.Services.Journal;
using TreesApi.BusinessLogic.Services.Journal.Parameters;

namespace TreesApi.Web.Controllers;

[Route("")]
public class JournalController : ControllerBase
{
    private readonly IJournalService _journalService;

    public JournalController(IJournalService journalService)
    {
        _journalService = journalService;
    }
    
    [HttpPost("/api.user.journal.getRange")]
    public async Task<IActionResult> GetRange([FromQuery] GetRangeParameters parameters, [FromBody] GetRangeFilterParameters filter)
    {
        var result = await _journalService.GetRangeAsync(parameters, filter);
        return Ok(result);
    }

    [HttpPost("/api.user.journal.getSingle")]
    public async Task<IActionResult> GetSingle([FromQuery] GetSingleParameters parameters)
    {
        var result = await _journalService.GetSingleAsync(parameters);
        return Ok(result);
    }
}
