using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api;

namespace MyToDoApp.Api.Controllers
{
    [Route("api/[controller]/[acgtion]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {

        public ToDoController(IUnitOfWork uow)
        {
            Uow = uow;
        }

        public IUnitOfWork Uow { get; }
    }
}
