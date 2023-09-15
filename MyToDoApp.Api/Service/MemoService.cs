using AutoMapper;
using MyToDo.Api;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using MyToDoApp.Api.Model;
using MyToDoApp.Api.Model.Repository;

namespace MyToDoApp.Api.Service
{
    public class MemoService : IMemoService
    {


        public MemoService(IUnitOfWork uow,ILogger<MemoRepository> logger,IMapper mapper)
        {
            Uow = uow;
            Logger = logger;
            Mapper = mapper;
        }

        public IUnitOfWork Uow { get; }
        public ILogger<MemoRepository> Logger { get; }
        public IMapper Mapper { get; }

        public async Task<ApiResponse> AddAsync(MemoDto model)
        {
            try
            {
                // AutoMapper 转换
                var Memo = Mapper.Map<Memo>(model);
                IRepository<Memo> repository = Uow.GetRepository<Memo>();
                await repository.InsertAsync(Memo);
                if (await Uow.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, Memo);
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
                IRepository<Memo> repository = Uow.GetRepository<Memo>();
                Memo Memo = await repository.GetFirstOrDefaultAsync(predicate: f => f.Id.Equals(id));
                repository.Delete(Memo);
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

        public async Task<ApiResponse> GetAllAsync(QueryParameter parameter)
        {
            try
            {
                var repository = Uow.GetRepository<Memo>();
                var Memos = await repository.GetPagedListAsync(predicate:
                   x => string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Title.Contains(parameter.Search),
                   pageIndex: parameter.PageIndex,
                   pageSize: parameter.PageSize,
                   orderBy: source => source.OrderByDescending(t => t.CreateDate));
                return new ApiResponse(true, Memos);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = Uow.GetRepository<Memo>();
                var Memo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return new ApiResponse(true, Memo);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(MemoDto model)
        {
            try
            {
                var repository = Uow.GetRepository<Memo>();
                var Memo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));
                Memo.Title = model.Title;
                Memo.Content = model.Content;  
                repository.Update(Memo);
                if (await Uow.SaveChangesAsync() > 0)
                    return new ApiResponse(true, Memo);
                return new ApiResponse("更新数据异常！");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        
    }
}
