using API.Models;
using API.Repositories;
using API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace API.Controllers
{
    public class DivisionController : ApiController
    {
        DivisionRepository repo = new DivisionRepository();

        public async Task<IEnumerable<DivisionVM>> Get()
        {
            return await repo.GetAll();
        }

        public async Task<DivisionVM> Get(int id)
        {
            return await repo.GetID(id);
        }

        public IHttpActionResult Post(Division division)
        {
            repo.Create(division);
            return Ok("Division Added Succesfully!"); //Status 200 OK 
        }

        public IHttpActionResult Put(int id, Division division)
        {
            if ((division.department_id != null) && (division.Name != ""))
            {
                repo.Update(id, division);
                return Ok("Division Added Succesfully!"); //Status 200 OK
            }
            return BadRequest("Failed to Add Division");
        }

        public IHttpActionResult Delete(int id)
        {
            var del = repo.Delete(id);
            if (del > 0)
            {
                return Ok("Successfully Delete");
            }
            return BadRequest("Not Success");
        }
    }
}
