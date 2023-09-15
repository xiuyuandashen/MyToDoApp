using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using MyToDoApp.Api.Model;

namespace MyToDoApp.Api.Service
{
    public interface IToDoService : IBaseService<ToDoDto>
    {

        Task<ApiResponse> GetAllAsync(ToDoParameter query);
    }
}
