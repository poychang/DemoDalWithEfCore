using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Schema
{
    /// <summary>資料庫模型</summary>
    public class MyDbContext : DbContext
    {
        /// <summary>建構式</summary>
        /// <param name="options">設定 MyDbContext 的選項</param>
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        /// <summary>資料表</summary>
        public DbSet<MyDataSet> MyDataSet { get; set; }
    }
}
