using Microsoft.EntityFrameworkCore;
using MyToDo.Api;

namespace MyToDoApp.Api.Model.Repository
{
    public class MemoRepository : Repository<Memo>, IRepository<Memo>
    {
        public MemoRepository(MyToDoContext dbContext) : base(dbContext)
        {
        }
    }
}
