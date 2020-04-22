using System.Collections.Generic;
using System.IO;
using System.Linq;

using IteaLinqToSql.Models.Entities;
using IteaLinqToSql.Models.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace IteaLinqToSql.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class GameSaveController : ControllerBase
    {
        readonly IService<GameSave> service;

        public GameSaveController(IService<GameSave> service)
        {
            this.service = service;
        }

        [HttpGet]
        public List<GameSave> Get()
        {
            return service
                .GetQuery()
                .ToList();
        }

        [HttpGet("{id}")]
        public GameSave Get(int id)
        {
            return service.FindById(id);
        }

        [HttpPost("save")]
        public List<GameSave> Post([FromBody] GameSave value)
        {
            return service
                .GetAll()
                .Where(x => x.Id == value.Id)
                .ToList();
        }

        [HttpPut()]
        public int Put(GameSave value)
        {
            service.Create(value);
            return value.Id;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            GameSave gameSave = service.FindById(id);
            if (gameSave != null)
            {
                service.Delete(gameSave);
            }
        }
    }
}
