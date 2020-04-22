using System.Collections.Generic;
using System.Linq;

using IteaLinqToSql.Models.Abstract;
using IteaLinqToSql.Models.Database;
using IteaLinqToSql.Models.Entities;
using IteaLinqToSql.Models.Interfaces;

namespace IteaLinqToSql.Services
{
    public class GameSaveService : IService<GameSave>
    {
        public BaseRepository<GameSave> Repository { get; set; }

        public GameSaveService(IteaDbContext dbContext)
        {
            Repository = new BaseRepository<GameSave>(dbContext);
        }

        public void Create(GameSave item)
        {
            Repository.Create(item);
        }

        public void Delete(GameSave item)
        {
            Repository.Remove(item);
        }

        public List<GameSave> GetAll()
        {
            return Repository.GetAll().ToList();
        }

        public GameSave Update(int id, GameSave updatedItem)
        {
            Repository.Update(updatedItem);
            return updatedItem;
        }

        public GameSave FindById(int id)
        {
            return Repository.FindById(id);
        }

        public IQueryable<GameSave> GetQuery()
        {
            return Repository.GetAll();
        }
    }
}
