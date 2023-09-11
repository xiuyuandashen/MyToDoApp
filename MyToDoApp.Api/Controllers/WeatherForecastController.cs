using Microsoft.AspNetCore.Mvc;
using MyToDo.Api;
using MyToDoApp.Api.Model;
using MyToDoApp.Api.Service;

namespace MyToDoApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnitOfWork uow)
        {
            _logger = logger;
            Uow = uow;
        }

        public IUnitOfWork Uow { get; }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetUsers")]
        public async Task<ApiResponse> GetUsers()
        {
            IRepository<User> repository = Uow.GetRepository<User>();
            IList<User> users = await repository.GetAllAsync(orderBy: f => f.OrderByDescending(x => x.UpdateDate));
            _logger.LogInformation("查询所有用户");
            return new ApiResponse(true, users);
            ;
        }

        [HttpPost("InsertUser")]

        public async Task<ApiResponse> AddUser([FromBody] User user)
        {
            IRepository<User> repository = Uow.GetRepository<User>();
            _ = await repository.InsertAsync(user);
            if (await Uow.SaveChangesAsync() > 0)
                return new ApiResponse("添加成功");
            

            return new ApiResponse("添加失败", false);
        }
    }
}