using TreesApi.DataAccess.Entities;
using TreesApi.DataAccess.Repositories.Generic;
using TreesApi.DataAccess.Repositories.TreeNode;

namespace TreesApi.DataAccess.UnitsOfWork;

public interface IUnitOfWork
{
    Task SaveAsync();
    ITreeNodeRepository TreeNodeEntityRepository { get; }
    IRepository<JournalEntryEntity> JournalExceptionEntityRepository { get; }
}
