using Microsoft.EntityFrameworkCore;
using TreesApi.DataAccess.Entities;
using TreesApi.DataAccess.Repositories.Generic;

namespace TreesApi.DataAccess.Repositories.TreeNode;

public class TreeNodeRepository : Repository<TreeNodeEntity>, ITreeNodeRepository
{
    private readonly DbSet<TreeNodeEntity> _dbSet;

    public TreeNodeRepository(ApplicationDbContext context)
        : base(context)
    {
        _dbSet = context.Set<TreeNodeEntity>();
    }

    public async Task<TreeNodeEntity?> GetTreeWithAllChildAsync(string treeName)
    {
        var result = await _dbSet.FromSqlRaw(
            """
            WITH RECURSIVE TreeNodesCte AS (
                SELECT *
                FROM public."TreeNodes" nodes
                WHERE nodes."Id" = (SELECT "Id" FROM public."TreeNodes" WHERE "Name" = {0} AND "ParentId" IS NULL)      
                UNION ALL
                SELECT childNodes.*
                FROM public."TreeNodes" childNodes
                INNER JOIN TreeNodesCte nodes  
                    ON nodes."Id" = childNodes."ParentId"
            )
            SELECT * FROM TreeNodesCte
            """,
            treeName).ToListAsync();

         return result.FirstOrDefault(e => e.ParentId == null);
    }

    public Task<TreeNodeEntity?> GetNodeWithSiblingsAndRootAsync(long nodeId)
    {
        return _dbSet
            .Where(node => node.Id == nodeId)
            .Include(x => x.Root)
            .Include(x => x.Parent)
            .ThenInclude(x => x.Children)
            .SingleOrDefaultAsync();
    }
}
