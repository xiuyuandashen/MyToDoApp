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
    public class MemoService : BaseService<MemoDto>, IMemoService
    {
        private readonly HttpRestClient client;

        public MemoService(HttpRestClient client) : base(client, "Memo")
        {
            this.client = client;
        }

        public async Task<ApiResponse<PagedList<MemoDto>>> GetAllFilterAsync(QueryParameter param)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/Memo/GetAll?pageIndex={param.PageIndex}" +
                $"&pageSize={param.PageSize}" +
                $"&search={param.Search}";
            return await client.ExecuteAsync<PagedList<MemoDto>>(request);
        }
    }
}
