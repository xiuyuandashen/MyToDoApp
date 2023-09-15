using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Shared.Dtos;
using MyToDoApp.Api.Service;

namespace MyToDoApp.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        public LoginController(ILoginService service)
        {
            Service = service;
        }

        public ILoginService Service { get; }

        [HttpPost]
        public async Task<ApiResponse> Login([FromBody] UserDto param) => await Service.LoginAsync(param.Account,param.PassWord);

        [HttpPost]
        public async Task<ApiResponse> Resgiter([FromBody] UserDto param) => await Service.Resgiter(param);

    }
}
