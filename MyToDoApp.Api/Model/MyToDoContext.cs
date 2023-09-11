using Microsoft.EntityFrameworkCore;

namespace MyToDoApp.Api.Model
{
    public class MyToDoContext : DbContext
    {
        public MyToDoContext(DbContextOptions<MyToDoContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<User>();
            //base.OnModelCreating(builder);
        }

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<ToDo> ToDo { get; set; }

        public virtual DbSet<Memo> Memo { get; set; }

        public override int SaveChanges()
        {
            // 自动赋值创建时间和更新时间
            SetSystemField();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // 自动赋值创建时间和更新时间
            SetSystemField();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 系统字段赋值
        /// </summary>
        private void SetSystemField()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity)
                {
                    var entity = (BaseEntity)item.Entity;
                    //添加操作
                    if (item.State == EntityState.Added)
                    {
                        //if (entity.Id == Guid.Empty)
                        //{
                        //    entity.Id = Guid.NewGuid();
                        //}
                        entity.CreateDate = DateTime.Now;
                        entity.UpdateDate = DateTime.Now;
                    }
                    //修改操作
                    else if (item.State == EntityState.Modified)
                    {
                        entity.UpdateDate = DateTime.Now;
                    }
                }

            }
        }
    }
}
