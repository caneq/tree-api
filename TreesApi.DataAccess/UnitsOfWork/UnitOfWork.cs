using TreesApi.DataAccess.Entities;
using TreesApi.DataAccess.Repositories.Generic;
using TreesApi.DataAccess.Repositories.TreeNode;

namespace TreesApi.DataAccess.UnitsOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context,
        ITreeNodeRepository treeNodeEntityRepository,
        IRepository<JournalEntryEntity> journalExceptionEntityRepository)
    {
        _context = context;
        TreeNodeEntityRepository = treeNodeEntityRepository;
        JournalExceptionEntityRepository = journalExceptionEntityRepository;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public ITreeNodeRepository TreeNodeEntityRepository { get; }

    public IRepository<JournalEntryEntity> JournalExceptionEntityRepository { get; }
}
