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
    public interface IToDoService:IBaseService<ToDoDto>
    {

        public Task<ApiResponse<PagedList<ToDoDto>>> GetAllFilterAsync(ToDoParameter param);
    }
}
