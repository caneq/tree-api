using Microsoft.AspNetCore.Mvc;
using TreesApi.BusinessLogic.Services.Tree;
using TreesApi.BusinessLogic.Services.Tree.Parameters;

namespace TreesApi.Web.Controllers;

[ApiController]
[Route("")]
public class TreeController : ControllerBase
{
    private readonly ITreeService _treeService;

    public TreeController(ITreeService treeService)
    {
        _treeService = treeService;
    }
    
    [HttpPost("/api.user.tree.get")]
    public async Task<IActionResult> GetOrCreateTree([FromQuery] GetOrCreateTreeParameters parameters, CancellationToken ct = default)
    {
        var result = await _treeService.GetOrCreateTreeAsync(parameters);
        return Ok(result);
    }
    
    [HttpPost("/api.user.tree.node.create")]
    public async Task<IActionResult> CreateNode([FromQuery] CreateNodeParameters parameters, CancellationToken ct = default)
    {
        await _treeService.CreateNodeAsync(parameters);
        return Ok();
    }
    
    [HttpPost("/api.user.tree.node.delete")]
    public async Task<IActionResult> DeleteNode([FromQuery] DeleteNodeParameters parameters, CancellationToken ct = default)
    {
        await _treeService.DeleteNodeAsync(parameters);
        return Ok();
    }
    
    [HttpPost("/api.user.tree.node.rename")]
    public async Task<IActionResult> RenameNode([FromQuery] RenameNodeParameters parameters, CancellationToken ct = default)
    {
        await _treeService.RenameNodeAsync(parameters);
        return Ok();
    }
}
