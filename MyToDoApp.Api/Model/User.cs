using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyToDoApp.Api.Model
{
    public class User : BaseEntity
    {
        [StringLength(20)]
        [Comment("账号")]
        public string Account { get; set; }


        [StringLength(128)]
        [Comment("用户名")]
        public string UserName { get; set; }

        [StringLength(128)]
        [Comment("密码")]
        public string PassWord { get; set; }
    }
}
