using System.Collections.Generic;
using System.Linq;

using IteaLinqToSql.Models.Abstract;
using IteaLinqToSql.Models.Database;
using IteaLinqToSql.Models.Entities;
using IteaLinqToSql.Models.Interfaces;

namespace IteaLinqToSql.Services
{
    public class LoginHistoryService : IService<LoginHistory>
    {
        public BaseRepository<LoginHistory> Repository { get; set; }

        public LoginHistoryService(IteaDbContext dbContext)
        {
            Repository = new BaseRepository<LoginHistory>(dbContext);
        }

        public void Create(LoginHistory item)
        {
            Repository.Create(item);
        }

        public void Delete(LoginHistory item)
        {
            Repository.Remove(item);
        }

        public List<LoginHistory> GetAll()
        {
            return Repository.GetAll().ToList();
        }

        public LoginHistory Update(int id, LoginHistory updatedItem)
        {
            Repository.Update(updatedItem);
            return updatedItem;
        }

        public LoginHistory FindById(int id)
        {
            return Repository.FindById(id);
        }

        public IQueryable<LoginHistory> GetQuery()
        {
            return Repository.GetAll();
        }
    }
}
