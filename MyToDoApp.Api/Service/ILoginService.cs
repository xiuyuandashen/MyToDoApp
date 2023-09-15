using MyToDo.Shared.Dtos;

namespace MyToDoApp.Api.Service
{
    public interface ILoginService 
    {
        Task<ApiResponse> LoginAsync(string Account, string Password);

        Task<ApiResponse> Resgiter(UserDto user);
    }
}
