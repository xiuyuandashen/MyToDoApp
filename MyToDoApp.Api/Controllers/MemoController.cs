using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using MyToDoApp.Api.Service;

namespace MyToDoApp.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemoController : ControllerBase
    {

        public MemoController(ILogger<MemoController> logger,IMemoService memoService)
        {
            Logger = logger;
            Service = memoService;
        }

        public ILogger<MemoController> Logger { get; }
        public IMemoService Service { get; }

        [HttpGet]
        public async Task<ApiResponse> Get(int id) => await Service.GetSingleAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameter param) => await Service.GetAllAsync(param);

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] MemoDto model) => await Service.AddAsync(model);

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] MemoDto model) => await Service.UpdateAsync(model);

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await Service.DeleteAsync(id);
    }
}
