using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.DataClassification;
using MyToDo.Api;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using MyToDoApp.Api.Model;
using MyToDoApp.Api.Service;

namespace MyToDoApp.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {

        public ToDoController(IToDoService toDoService)
        {
            Service = toDoService;
        }

        public IToDoService Service { get; }

        [HttpGet]
        public async Task<ApiResponse> Get(int id) => await Service.GetSingleAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] ToDoParameter param) => await Service.GetAllAsync(param);

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] ToDoDto model) => await Service.AddAsync(model);

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] ToDoDto model) => await Service.UpdateAsync(model);

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await Service.DeleteAsync(id);


        /// <summary>
        /// 获取FormData类型的请求数据
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> Test([FromForm] FormData formData)
        {

            string Name = formData.Name;
            var files = formData.Files;


            return new ApiResponse("上传成功！");

        }


        
    }

    public class FormData{

        public string Name { get; set; }

        /// <summary>
        /// 文件都是上传到这
        /// </summary>
        public List<IFormFile> ? Files { get; set; }

    }
}
