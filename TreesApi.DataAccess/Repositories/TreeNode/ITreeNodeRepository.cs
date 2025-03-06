using TreesApi.DataAccess.Entities;
using TreesApi.DataAccess.Repositories.Generic;

namespace TreesApi.DataAccess.Repositories.TreeNode;

public interface ITreeNodeRepository : IRepository<TreeNodeEntity>
{
    Task<TreeNodeEntity?> GetTreeWithAllChildAsync(string treeName);

    Task<TreeNodeEntity?> GetNodeWithSiblingsAndRootAsync(long nodeId);
}
