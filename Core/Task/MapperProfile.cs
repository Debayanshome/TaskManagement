using AutoMapper;
using TaskManagement.Core.Common;
using TaskManagement.Core.Task.Shared;
using TaskManagement.Repository.Models;
using TaskManagement.Shared.Repository;

namespace TaskManagement.Core.Task
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<TaskDetail, TaskResponseModel>(MemberList.None)
                    .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee))
                    .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.Documents));
            CreateMap<Document, DocumentResponseModel>(MemberList.None);
            CreateMap<TaskManagement.Repository.Models.Employee, EmployeeResponseModel>(MemberList.None);
            CreateMap<PaginatedList<Repository.Models.TaskDetail>, PaginatedResponseModel<TaskResponseModel>>(MemberList.None)
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src));
        }
    }
}
