using AutoMapper;
using MyToDo.Api;
using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using MyToDoApp.Api.Model;
using System.Data.Common;
using System.Reflection.Metadata;

namespace MyToDoApp.Api.Service
{
    public class ToDoService : IToDoService
    {

        public IUnitOfWork Uow { get; }
        public ILogger<ToDoService> Logger { get; }
        public IMapper Mapper { get; }

        public ToDoService(IUnitOfWork uow, ILogger<ToDoService> logger,IMapper mapper)
        {
            Uow = uow;
            Logger = logger;
            Mapper = mapper;
        }


        public async Task<ApiResponse> AddAsync(ToDoDto model)
        {
            try
            {
                // AutoMapper 转换
                var Todo = Mapper.Map<ToDo>(model);
                IRepository<ToDo> repository = Uow.GetRepository<ToDo>();
                await repository.InsertAsync(Todo);
                if (await Uow.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, Todo);
                }
                return new ApiResponse("添加数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }


        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                IRepository<ToDo> repository = Uow.GetRepository<ToDo>();
                ToDo toDo = await repository.GetFirstOrDefaultAsync(predicate: f => f.Id.Equals(id));
                repository.Delete(toDo);
                if (await Uow.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, "");
                }
                return new ApiResponse("删除数据失败");

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllAsync(ToDoParameter query)
        {
            try
            {
                IRepository<ToDo> repository = Uow.GetRepository<ToDo>();
                var toDos = await repository.GetPagedListAsync(
                    predicate: f => (string.IsNullOrWhiteSpace(query.Search) ? true : f.Title.Contains(query.Search)) && (query.Status == null ? true : f.Status.Equals(query.Status)),
                    pageIndex: query.PageIndex,
                    pageSize: query.PageSize,
                    orderBy: source => source.OrderByDescending(t => t.CreateDate)
                );
                //var items = Mapper.Map<IList<ToDoDto>>(toDos.Items);
                
                return new ApiResponse(true, toDos);

            }
            catch(Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
            
        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = Uow.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return new ApiResponse(true, todo);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(ToDoDto model)
        {
            try
            {
                var repository = Uow.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));
                todo.Title = model.Title;
                todo.Content = model.Content;
                todo.Status = model.Status;
                repository.Update(todo);
                if (await Uow.SaveChangesAsync() > 0)
                    return new ApiResponse(true, todo);
                return new ApiResponse("更新数据异常！");
            }
            catch(Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public Task<ApiResponse> GetAllAsync(QueryParameter query)
        {
            throw new NotImplementedException();
        }
    }
}
