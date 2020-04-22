using System.Collections.Generic;
using System.Linq;

using IteaLinqToSql.Models.Entities;
using IteaLinqToSql.Models.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IteaLinqToSql.Controllers
{
    [Route("api/myclass")]
    [ApiController]
    public class MyClassController : ControllerBase
    {
        List<MyClass> ts = new List<MyClass>
            {
                new MyClass{ Id = 1, Cipher ="Cipher1", Age= 20, Comment = "Comment1"},
                new MyClass{ Id = 2,Cipher ="Cipher2", Age= 20, Comment = "Comment2"},
                new MyClass{ Id = 3,Cipher ="Cipher3", Age= 20, Comment = "Comment3"}
            };

        readonly IService<MyClass> service;

        public MyClassController(IService<MyClass> service)
        {
            this.service = service;
        }

        [HttpGet]
        public List<MyClass> Get()
        {
            return ts;
        }

        [HttpGet("{id}")]
        public MyClass Get(int id)
        {
            return ts.Where(x=>x.Id == id).SingleOrDefault();
        }

        [HttpPost("save")]
        public List<MyClass> Post([FromBody] MyClass value)
        {
            return ts;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
