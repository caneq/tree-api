namespace TreesApi.BusinessLogic.Models;

public class TreeNodeModel
{
    public long? Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public ICollection<TreeNodeModel> Children { get; set; } =  Array.Empty<TreeNodeModel>();
}
