using AutoMapper;
using MyToDo.Shared.Dtos;
using MyToDoApp.Api.Model;

namespace MyToDoApp.Api.Extensions
{
    public class AutoMapperProFile:MapperConfigurationExpression
    {

        public AutoMapperProFile()
        {
            // 增加实体间的转换
            CreateMap<ToDo, ToDoDto>().ReverseMap();
            CreateMap<Memo, MemoDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            
            /**
             *  使用 ForMember 对映射规则做进一步的加工
            CreateMap<PostModel, PostViewModel>()
            .ForMember(destination => destination.CommentCounts, source => source.MapFrom(i => i.Comments.Count()));
            **/


        }
    }
}
