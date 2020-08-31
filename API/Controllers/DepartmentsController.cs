using API.Models;
using API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    public class DepartmentsController : ApiController
    {
        DepartmentRepository repo = new DepartmentRepository();

        // GET: api/Departments
        public async Task<IEnumerable<Department>> Get()
        {
            return await repo.Get();
        }

        // GET: api/Departments/5
        public async Task<Department> Get(int id)
        {
            return await repo.Get(id);
        }

        // POST: api/Departments
        public IHttpActionResult Post(Department department)
        {
            repo.Creat(department);
            return Ok("Data has been inserted");
        }

        // PUT: api/Departments/5
        [HttpPut]
        [ActionName("Depertment/{id}")]
        public IHttpActionResult Post(int id, Department department)
        {
            repo.Update(id, department);
            return Ok("Data has been changed");
        }

        // DELETE: api/Departments/5
        public IHttpActionResult Delete(int id)
        {
            repo.Delete(id);
            return Ok("Data has been delete");
        }
    }
}
