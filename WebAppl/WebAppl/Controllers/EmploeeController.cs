using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppl.DbRepo;

namespace WebAppl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmploeeController : ControllerBase
    {
        private readonly WebApiContext dbContext;
        public EmploeeController(WebApiContext dbContext) {
            this.dbContext = dbContext;
        }

        // GET: api/Emploee
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return dbContext.Employees.ToList();
        }

        // GET: api/Emploee/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Emploee
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Emploee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Emploee/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
