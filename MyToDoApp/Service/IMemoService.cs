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
    public interface IMemoService:IBaseService<MemoDto>
    {

        public Task<ApiResponse<PagedList<MemoDto>>> GetAllFilterAsync(QueryParameter param);
    }
}
