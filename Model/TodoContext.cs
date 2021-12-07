using Microsoft.EntityFrameworkCore;
namespace TodoApi.Model
{
    //DbContext 是 EF Core 跟資料庫溝通的主要類別，透過繼承 DbContext 可以定義跟資料庫溝通的行為。

    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
        {
        }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}