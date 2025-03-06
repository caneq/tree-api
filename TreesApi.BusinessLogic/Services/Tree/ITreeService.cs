using TreesApi.BusinessLogic.Models;
using TreesApi.BusinessLogic.Services.Tree.Parameters;

namespace TreesApi.BusinessLogic.Services.Tree;

public interface ITreeService
{
    Task<TreeNodeModel?> GetOrCreateTreeAsync(GetOrCreateTreeParameters parameters);
    Task CreateNodeAsync(CreateNodeParameters parameters);
    Task DeleteNodeAsync(DeleteNodeParameters parameters);
    Task RenameNodeAsync(RenameNodeParameters parameters);
}
