using TreesApi.BusinessLogic.Exceptions;
using TreesApi.BusinessLogic.Services.Tree.Parameters;
using TreesApi.DataAccess.Entities;

namespace TreesApi.BusinessLogic.Validators;

public class TreeUpdateValidator : ITreeUpdateValidator
{
    public void ValidateCreateNode(TreeNodeEntity? parent, CreateNodeParameters parameters)
    {
        ValidateNodeFoundAndBelongsTreeWithName(parent, parameters.TreeName);

        if (parent!.Children.Any(x => x.Name == parameters.NodeName))
        {
            throw new SecureException("Name should be unique between siblings");
        }
    }

    public void ValidateRenameNode(TreeNodeEntity? node, RenameNodeParameters parameters)
    {
        ValidateNodeFoundAndBelongsTreeWithName(node, parameters.TreeName);

        if (node!.Parent == null)
        {
            throw new SecureException("Couldn't rename root node");
        }
        if (node.Parent.Children.Where(x => x.Id != node.Id).Any(x => x.Name == parameters.NewNodeName))
        {
            throw new SecureException("Name should be unique between siblings");
        }
    }

    public void ValidateDeleteNode(TreeNodeEntity? node, DeleteNodeParameters parameters)
    {
        ValidateNodeFoundAndBelongsTreeWithName(node, parameters.TreeName);

        if (node!.Children.Any())
        {
            throw new SecureException("You have to delete all children first");
        }
    }

    private static void ValidateNodeFoundAndBelongsTreeWithName(TreeNodeEntity? node, string treeName)
    {
        if (node == null)
        {
            throw new SecureException("Node with the specified ID was not found");
        }
        if (GetRootNode(node).Name != treeName)
        {
            throw new SecureException("Node found, but it does not belong to your tree");
        }
    }

    private static TreeNodeEntity GetRootNode(TreeNodeEntity node)
    {
        return node.Root ?? node;
    }
}
