using System.Collections.Generic;
using System.Linq;

using IteaLinqToSql.Models.Abstract;
using IteaLinqToSql.Models.Database;
using IteaLinqToSql.Models.Entities;
using IteaLinqToSql.Models.Interfaces;

namespace IteaLinqToSql.Services
{
    public class MyClassService : IService<MyClass>
    {
        public BaseRepository<MyClass> Repository { get; set; }

        public MyClassService(IteaDbContext dbContext)
        {
            Repository = new BaseRepository<MyClass>(dbContext);
        }

        public void Create(MyClass item)
        {
            Repository.Create(item);
        }

        public void Delete(MyClass item)
        {
            Repository.Remove(item);
        }

        public List<MyClass> GetAll()
        {
            return Repository.GetAll().ToList();
        }

        public MyClass Update(int id, MyClass updatedItem)
        {
            Repository.Update(updatedItem);
            return updatedItem;
        }

        public MyClass FindById(int id)
        {
            return Repository.FindById(id);
        }

        public IQueryable<MyClass> GetQuery()
        {
            return Repository.GetAll();
        }
    }
}
