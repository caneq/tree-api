using AutoMapper;
using TreesApi.BusinessLogic.Models;
using TreesApi.BusinessLogic.Services.Tree.Parameters;
using TreesApi.BusinessLogic.Validators;
using TreesApi.DataAccess.Entities;
using TreesApi.DataAccess.UnitsOfWork;

namespace TreesApi.BusinessLogic.Services.Tree;

public class TreeService : ITreeService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITreeUpdateValidator _treeUpdateValidator;

    public TreeService(IMapper mapper, IUnitOfWork unitOfWork, ITreeUpdateValidator treeUpdateValidator)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _treeUpdateValidator = treeUpdateValidator;
    }

    public async Task<TreeNodeModel?> GetOrCreateTreeAsync(GetOrCreateTreeParameters parameters)
    {
        var result = await _unitOfWork.TreeNodeEntityRepository.GetTreeWithAllChildAsync(parameters.Name);
        result ??= await CreateTreeAsync(parameters.Name);
        return _mapper.Map<TreeNodeModel>(result);
    }

    public async Task CreateNodeAsync(CreateNodeParameters parameters)
    {
        var parent = await _unitOfWork.TreeNodeEntityRepository.GetByIdAsync(parameters.ParentNodeId, nameof(TreeNodeEntity.Root), nameof(TreeNodeEntity.Children));
        
        _treeUpdateValidator.ValidateCreateNode(parent, parameters);

        var newTreeNode = new TreeNodeEntity
        {
            Name = parameters.NodeName,
            ParentId = parameters.ParentNodeId,
            RootId = GetRootNode(parent!).Id
        };

        await _unitOfWork.TreeNodeEntityRepository.InsertAsync(newTreeNode);
        await _unitOfWork.SaveAsync();
    }

    public async Task RenameNodeAsync(RenameNodeParameters parameters)
    {
        var node = await _unitOfWork.TreeNodeEntityRepository.GetNodeWithSiblingsAndRootAsync(parameters.NodeId);

        _treeUpdateValidator.ValidateRenameNode(node, parameters);

        if (node!.Name == parameters.NewNodeName)
        {
            return;
        }
        
        node.Name = parameters.NewNodeName;
        _unitOfWork.TreeNodeEntityRepository.Update(node);
        await _unitOfWork.SaveAsync();
    }
    
    public async Task DeleteNodeAsync(DeleteNodeParameters parameters)
    {
        var node = await _unitOfWork.TreeNodeEntityRepository.GetByIdAsync(parameters.NodeId, nameof(TreeNodeEntity.Root), nameof(TreeNodeEntity.Children));
        
        _treeUpdateValidator.ValidateDeleteNode(node, parameters);

        _unitOfWork.TreeNodeEntityRepository.Delete(node!);
        await _unitOfWork.SaveAsync();
    }

    private async Task<TreeNodeEntity> CreateTreeAsync(string name)
    {
        var newTree = new TreeNodeEntity
        {
            Name = name
        };
 
        await _unitOfWork.TreeNodeEntityRepository.InsertAsync(newTree);
        await _unitOfWork.SaveAsync();
        return newTree;
    }

    private static TreeNodeEntity GetRootNode(TreeNodeEntity node)
    {
        return node.Root ?? node;
    }
}
