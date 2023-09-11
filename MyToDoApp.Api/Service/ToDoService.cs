using MyToDo.Shared.Parameters;
using MyToDoApp.Api.Model;

namespace MyToDoApp.Api.Service
{
    public class ToDoService : IToDoService
    {
        public Task<ApiResponse> AddAsync(ToDo model)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetAllAsync(QueryParameter query)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetSingleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UpdateAsync(ToDo model)
        {
            throw new NotImplementedException();
        }
    }
}
