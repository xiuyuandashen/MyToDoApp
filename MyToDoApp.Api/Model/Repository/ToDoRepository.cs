using Microsoft.EntityFrameworkCore;
using MyToDo.Api;

namespace MyToDoApp.Api.Model.Repository
{
    public class ToDoRepository : Repository<ToDo>, IRepository<ToDo>
    {
        public ToDoRepository(MyToDoContext dbContext) : base(dbContext)
        {
        }
    }
}
