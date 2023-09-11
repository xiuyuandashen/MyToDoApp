using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MyToDo.Api;
using MyToDoApp.Api.Model;
using MyToDoApp.Api.Model.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region ���� log4Net ��Ҫ��װlog4net��Microsoft.Extensions.Logging.Log4Net.AspNetCore
builder.Services.AddLogging(cfg =>
{
    cfg.AddLog4Net();
    //Ĭ�ϵ������ļ�·�����ڸ�Ŀ¼�����ļ���Ϊlog4net.config
    //����ļ�·���������б仯����Ҫ����������·��������
    //��������Ŀ��Ŀ¼�´���һ����Ϊcfg���ļ��У���log4net.config�ļ��������У�������Ϊlog.config
    //����Ҫʹ������Ĵ�������������
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
#region ע��EF Core 
builder.Services.AddDbContext<MyToDoContext>(options =>
{
    options.UseSqlServer(configuration["ConnectionStrings:SqlServer"]);
});
// ע��UnitOfWork ������Ԫ
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
