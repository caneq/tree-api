using AutoMapper;
using TreesApi.BusinessLogic.Models;
using TreesApi.DataAccess.Entities;
using TreesApi.DataAccess.Models;

namespace TreesApi.BusinessLogic.MappingProfiles;

public class BusinessLogicMappingProfile : Profile
{
    public BusinessLogicMappingProfile()
    {
        CreateMap<TreeNodeEntity, TreeNodeModel>();
        CreateMap<JournalEntryEntity, JournalEntryModel>();
        CreateMap<JournalEntryEntity, JournalEntryPageableModel>();
        CreateMap<PageableResult<JournalEntryEntity>, PageableResult<JournalEntryPageableModel>>();
    }
}
