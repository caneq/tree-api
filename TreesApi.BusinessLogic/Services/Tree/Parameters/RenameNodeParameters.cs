namespace TreesApi.BusinessLogic.Services.Tree.Parameters;

public record RenameNodeParameters(string TreeName, long NodeId, string NewNodeName);
