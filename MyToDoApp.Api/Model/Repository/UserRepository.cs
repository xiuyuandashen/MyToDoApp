using Microsoft.EntityFrameworkCore;
using MyToDo.Api;

namespace MyToDoApp.Api.Model.Repository
{
    public class UserRepository : Repository<User>, IRepository<User>
    {
        public UserRepository(MyToDoContext dbContext) : base(dbContext)
        {
            
        }
    }
}
