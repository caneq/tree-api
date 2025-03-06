namespace TreesApi.BusinessLogic.Services.Tree.Parameters;

public record CreateNodeParameters(string TreeName, long ParentNodeId, string NodeName);
