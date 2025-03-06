namespace TreesApi.DataAccess.Entities;

public class TreeNodeEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public long? ParentId { get; set; }

    public TreeNodeEntity? Parent { get; set; }
    
    public long? RootId { get; set; }
    
    public TreeNodeEntity? Root { get; set; }

    public ICollection<TreeNodeEntity> Children { get; set; } = new List<TreeNodeEntity>();
}
