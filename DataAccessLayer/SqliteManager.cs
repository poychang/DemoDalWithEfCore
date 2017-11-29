using System;
using System.Collections;
using DataAccessLayer.Repository;
using DataAccessLayer.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataAccessLayer
{
    /// <summary>SQLite 資料庫管理者</summary>
    public class SqliteManager : IDbManager
    {
        /// <summary>資料庫 Context</summary>
        private readonly DbContext _context;

        /// <summary>Repository 池</summary>
        private Hashtable _repositories;

        /// <summary>是否已清除</summary>
        private bool _disposed;

        /// <summary>建構式</summary>
        /// <param name="optionsAccessor"></param>
        public SqliteManager(IOptions<DbManagerOptions> optionsAccessor)
        {
            Console.WriteLine("Connect to Database");
            var contextOptions = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlite(optionsAccessor.Value.ConnectionString)
                .Options;
            _context = new MyDbContext(contextOptions);
        }

        /// <summary>解構式</summary>
        ~SqliteManager()
        {
            Dispose(false);
        }

        /// <summary>清除此類別資源</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>清除此類別資源</summary>
        /// <param name="disposing">是否在清理中</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        /// <summary>儲存所有異動</summary>
        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>檢查資料庫是否存在</summary>
        /// <returns>是否存在</returns>
        public bool IsDatabasebExist()
        {
            return _context.Database.EnsureCreated();
        }

        /// <summary>取得某一個 Entity Repository。如果沒有取過會初始化一個，如果有就取得之前的</summary>
        /// <typeparam name="TEntity">此 DbContext 裡面的 Entity Type</typeparam>
        /// <returns>Entity Repository</returns>
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            // 檢查是否已經初始化過類別為 TEntity 的 Entity Repository
            var type = typeof(TEntity).Name;
            if (_repositories.ContainsKey(type)) return (IRepository<TEntity>)_repositories[type];

            // 將初始化的 Entity Repository 實體存放進 Repository 池
            var repositoryType = typeof(EFGenericRepository<>);
            var repositoryInstance =
                Activator.CreateInstance(repositoryType
                    .MakeGenericType(typeof(TEntity)), _context);
            _repositories.Add(type, repositoryInstance);

            return (IRepository<TEntity>)_repositories[type];
        }
    }
}
