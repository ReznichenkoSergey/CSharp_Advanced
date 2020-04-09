using System;
using System.Collections.Generic;
using System.Linq;

using IteaLinqToSql.Models.Entities;
using IteaLinqToSql.Models.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IteaLinqToSql.Controllers
{
    [Route("api/history")]
    [ApiController]
    public class JoinController : ControllerBase
    {
        readonly IService<LoginHistory> service;

        public JoinController(IService<LoginHistory> service)
        {
            this.service = service;
        }

        [HttpGet]
        public List<LoginHistory> GetAll()
        {
            return service
                .GetQuery()
                .ToList();
        }


        [HttpGet("join")]
        public List<LoginHistory> GetJoin()
        {
            return service
                .GetQuery()
                .Include(x => x.User)
                .ToList();
        }


        /// <summary>
        /// Просто тест скрипта группировки
        /// </summary>
        /// <returns></returns>
        [HttpGet("group")]
        public List<LoginHistory> GetGroup()
        {
            return service
                .GetQuery()
                .GroupBy(p => p.UserId)
                .Select((x) => new LoginHistory
                {
                    UserId = x.Key,
                    Id = x.Count()
                })
                .ToList();
        }


        [HttpGet("maxdate")]
        public List<LoginHistory> GetMaxDate()
        {
            return service
                .GetQuery()
                .Where(x => x.LoginTime == (service.GetAll().Max(y => y.LoginTime)))
                .ToList();
        }

        [HttpGet("mindate")]
        public List<LoginHistory> GetMinDate()
        {
            return service
                .GetQuery()
                .Where(x => x.LoginTime == (service.GetAll().Min(y => y.LoginTime)))
                .ToList();
        }

        /// <summary>
        /// Сортировка
        /// </summary>
        /// <returns></returns>
        [HttpGet("order_{desc}_{val}")]
        public List<LoginHistory> GetOrderedDesc(string desc = "desc", string val = "date")
        {
            if (desc.Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                switch (val.ToLower())
                {
                    case "device":
                        return service
                            .GetQuery()
                            .OrderByDescending(x => x.UserDevice)
                            .ToList();
                    case "ip":
                        return service
                            .GetQuery()
                            .OrderByDescending(x => x.IPAddress)
                            .ToList();
                    case "date":
                        return service
                            .GetQuery()
                            .OrderByDescending(x => x.LoginTime)
                            .ToList();
                    default:
                        return service
                            .GetQuery()
                            .OrderByDescending(x => x.Id)
                            .ToList();
                }
            }
            else
            {
                switch (val.ToLower())
                {
                    case "device":
                        return service
                            .GetQuery()
                            .OrderBy(x => x.UserDevice)
                            .ToList();
                    case "ip":
                        return service
                            .GetQuery()
                            .OrderBy(x => x.IPAddress)
                            .ToList();
                    case "date":
                        return service
                            .GetQuery()
                            .OrderBy(x => x.LoginTime)
                            .ToList();
                    default:
                        return service
                            .GetQuery()
                            .OrderBy(x => x.Id)
                            .ToList();
                }
            }
        }

        [HttpGet("{id}")]
        public LoginHistory Get(int id)
        {
            return service.FindById(id);
        }

        /// <summary>
        /// Поиск истории по индексу пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("userid_{id}")]
        public List<LoginHistory> GetByUserId(int id)
        {
            return service
                .GetAll()
                .Where(x => x.UserId.Equals(id))
                .ToList();
        }

        [HttpPost("save")]
        public List<LoginHistory> Post([FromBody] LoginHistory value)
        {
            return service
                .GetAll()
                .Where(x => x.Id == value.Id)
                .ToList();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            LoginHistory loginHistory1 = service.FindById(id);
            if (loginHistory1 != null)
            {
                service.Delete(loginHistory1);
            }
        }

    }
}
