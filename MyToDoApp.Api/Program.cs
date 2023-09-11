using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MyToDo.Api;
using MyToDoApp.Api.Model;
using MyToDoApp.Api.Model.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region 配置 log4Net 需要安装log4net、Microsoft.Extensions.Logging.Log4Net.AspNetCore
builder.Services.AddLogging(cfg =>
{
    cfg.AddLog4Net();
    //默认的配置文件路径是在根目录，且文件名为log4net.config
    //如果文件路径或名称有变化，需要重新设置其路径或名称
    //比如在项目根目录下创建一个名为cfg的文件夹，将log4net.config文件移入其中，并改名为log.config
    //则需要使用下面的代码来进行配置
    //cfg.AddLog4Net(new Log4NetProviderOptions()
    //{
    //    Log4NetConfigFileName = "cfg/log.config",
    //    Watch = true
    //});
});
#endregion
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyToDoApp.Api", Version = "v1" });
});
var configuration = builder.Configuration;
#region 注册EF Core 
builder.Services.AddDbContext<MyToDoContext>(options =>
{
    options.UseSqlServer(configuration["ConnectionStrings:SqlServer"]);
});
// 注册UnitOfWork 工作单元
builder.Services.AddUnitOfWork<MyToDoContext>();
builder.Services.AddCustomRepository<ToDo, ToDoRepository>();
builder.Services.AddCustomRepository<User, UserRepository>();
builder.Services.AddCustomRepository<Memo, MemoRepository>();
#endregion
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
