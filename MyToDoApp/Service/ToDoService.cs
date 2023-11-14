using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDoApp.Service
{
    public class ToDoService : BaseService<ToDoDto>, IToDoService
    {
        public ToDoService(HttpRestClient client) : base(client, "ToDo")
        {
            this.client = client;
        }

        private readonly HttpRestClient client;

        public async Task<ApiResponse<PagedList<ToDoDto>>> GetAllFilterAsync(ToDoParameter param)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/ToDo/GetAll?pageIndex={param.PageIndex}" +
                $"&pageSize={param.PageSize}" +
                $"&search={param.Search}" +
                $"&Status={param.Status}";
            return await client.ExecuteAsync<PagedList<ToDoDto>>(request);
        }
    }
}
