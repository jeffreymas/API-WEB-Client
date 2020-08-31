using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class DepartmentsController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:50946/api/")
        };

        // GET: Departments
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadDepart()
        {
            IEnumerable<Department> departments = null;
            var resTask = client.GetAsync("departments/");
            resTask.Wait();
            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Department>>();
                readTask.Wait();
                departments = readTask.Result;
            }

            return Json(departments, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertOrUpdate(Department departments, int id)
        {
            try
            {
                var json = JsonConvert.SerializeObject(departments);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                if (departments.Id == 0)
                {
                    var result = client.PostAsync("departments", byteContent).Result;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else if (departments.Id != 0)
                {
                    var result = client.PutAsync("departments/" + id, byteContent).Result;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                return Json(404);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetById(int id)
        {
            Department departments = null;
            var resTask = client.GetAsync("departments/" + id);
            resTask.Wait();
            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var getJson = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                departments = JsonConvert.DeserializeObject<Department>(getJson);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }

            return Json(departments, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            var result = client.DeleteAsync("departments/" + id).Result;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}