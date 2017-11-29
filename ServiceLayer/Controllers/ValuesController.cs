using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Repository;
using DataAccessLayer.Schema;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Models;

namespace ServiceLayer.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IRepository<MyDataSet> _repo;

        public ValuesController(IDbManager dbManager)
        {
            _repo = dbManager.Repository<MyDataSet>();
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_repo.Read());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return new JsonResult(_repo.Read(p => p.Id == id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]MyDataModel value)
        {
            if (!ModelState.IsValid) return BadRequest();

            var entity = new MyDataSet() { Description = value.Description };
            _repo.Create(entity);
            _repo.SaveChanges();

            return Get(entity.Id);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]MyDataModel value)
        {
            if (!ModelState.IsValid) return BadRequest();

            var entity = _repo.Read(p => p.Id == id);
            entity.Description = value.Description;
            _repo.Update(entity);
            _repo.SaveChanges();

            return Get(entity.Id);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repo.Delete(_repo.Read(p => p.Id == id));
            _repo.SaveChanges();

            return Accepted();
        }
    }
}
