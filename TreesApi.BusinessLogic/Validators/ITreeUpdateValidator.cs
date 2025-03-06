using TreesApi.BusinessLogic.Services.Tree.Parameters;
using TreesApi.DataAccess.Entities;

namespace TreesApi.BusinessLogic.Validators;

public interface ITreeUpdateValidator
{
    void ValidateCreateNode(TreeNodeEntity? parent, CreateNodeParameters parameters);
    void ValidateRenameNode(TreeNodeEntity? node, RenameNodeParameters parameters);
    void ValidateDeleteNode(TreeNodeEntity? node, DeleteNodeParameters parameters);
}
