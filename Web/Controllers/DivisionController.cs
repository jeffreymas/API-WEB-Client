using API.Models;
using API.ViewModels;
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
    public class DivisionController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:50946/api/")
        };
        // GET: Divisions
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadDiv()
        {
            IEnumerable<DivisionVM> divisionVMs = null;
            var resTask = client.GetAsync("division");
            resTask.Wait();
            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<DivisionVM>>();
                readTask.Wait();
                divisionVMs = readTask.Result;
            }
            else
            {
                divisionVMs = Enumerable.Empty<DivisionVM>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }

            return Json(divisionVMs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetById(int id)
        {
            DivisionVM divisionVMs = null;
            var resTask = client.GetAsync("division/" + id);
            resTask.Wait();
            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var getJson = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                divisionVMs = JsonConvert.DeserializeObject<DivisionVM>(getJson);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }

            return Json(divisionVMs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertOrUpdate(Division division, int id)
        {
            try
            {
                var json = JsonConvert.SerializeObject(division);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                if (id == 0)
                {
                    var result = client.PostAsync("division", byteContent).Result;
                    return Json(result);
                }
                else if (id != 0)
                {
                    var result = client.PutAsync("division/" + id, byteContent).Result;
                    return Json(result);
                }

                return Json(404);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult Delete(int id)
        {
            var result = client.DeleteAsync("division/" + id).Result;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
