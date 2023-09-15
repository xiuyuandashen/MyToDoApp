using AutoMapper;
using MyToDo.Api;
using MyToDo.Shared.Dtos;
using MyToDoApp.Api.Model;

namespace MyToDoApp.Api.Service
{
    public class LoginService : ILoginService
    {
        public LoginService(IUnitOfWork work,IMapper mapper)
        {
            Work = work;
            Mapper = mapper;
        }

        public IUnitOfWork Work { get; }
        public IMapper Mapper { get; }

        public async Task<ApiResponse> LoginAsync(string Account, string Password)
        {
            try
            {
                var model = await Work.GetRepository<User>().GetFirstOrDefaultAsync(predicate:
                    x => (x.Account.Equals(Account)) &&
                    (x.PassWord.Equals(Password)));

                if (model == null)
                    return new ApiResponse("账号或密码错误,请重试！");
                
                return new ApiResponse(true, new UserDto()
                {
                    Account = model.Account,
                    UserName = model.UserName,
                    Id = model.Id
                });


            }
            catch (Exception ex) {
                return new ApiResponse(false, "登录失败！");
            }
        }

        public async Task<ApiResponse> Resgiter(UserDto user)
        {
            try
            {
                User model = Mapper.Map<User>(user);
                var repository = Work.GetRepository<User>();
                User userModel = await repository.GetFirstOrDefaultAsync(predicate:x=>x.Account.Equals(model.Account));

                if (userModel != null)
                {
                    return new ApiResponse($"当前账号:{model.Account}已存在,请重新注册！");
                }

                await repository.InsertAsync(model);

                if(await Work.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true,model);
                }

                return new ApiResponse("注册失败,请稍后重试！");
            }
            catch(Exception ex)
            {
                return new ApiResponse("注册账号失败！");
            }
        }
    }
}
