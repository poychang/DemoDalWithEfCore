using DataAccessLayer.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataAccessLayer
{
    /// <summary>SQLite 資料庫管理者</summary>
    public class SqliteManager : DbManager
    {
        /// <summary>建構式</summary>
        /// <param name="optionsAccessor"></param>
        public SqliteManager(IOptions<DbManagerOptions> optionsAccessor) : base(optionsAccessor) { }

        /// <summary>使用 SQLite 資料庫</summary>
        protected override void UseDbContext()
        {
            var contextOptions = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlite(OptionsAccessor.Value.ConnectionString)
                .Options;
            Context = new MyDbContext(contextOptions);
        }

        /// <summary>解構式</summary>
        ~SqliteManager()
        {
            Dispose(false);
        }
    }
}
