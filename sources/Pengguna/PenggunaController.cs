using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_pengguna.Domain
{
    public partial class PenggunaController : System.Web.Mvc.Controller
    {
        //exec:Search
        public async Task<System.Web.Mvc.ActionResult> Search()
        {

            var json = Bespoke.Sph.Web.Helpers.ControllerHelpers.GetRequestBody(this);
            var request = new System.Net.Http.StringContent(json);
            var url = "epsikologi/pengguna/_search";

            using (var client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.ElasticSearchHost);
                var response = await client.PostAsync(url, request);
                var content = response.Content as System.Net.Http.StreamContent;
                if (null == content) throw new Exception("Cannot execute query on es " + request);
                this.Response.ContentType = "application/json; charset=utf-8";
                return Content(await content.ReadAsStringAsync());
            }

        }

        //exec:Save
        public async Task<System.Web.Mvc.ActionResult> Save([RequestBody]Pengguna item)
        {

            if (null == item) throw new ArgumentNullException("item");
            var context = new Bespoke.Sph.Domain.SphDataContext();
            if (item.IsNewItem) item.Id = Guid.NewGuid().ToString();

            using (var session = context.OpenSession())
            {
                session.Attach(item);
                await session.SubmitChanges("save");
            }
            this.Response.ContentType = "application/json; charset=utf-8";
            return Json(new { success = true, status = "OK", id = item.Id, href = "pengguna/" + item.Id });
        }
        //exec:DateNow
        [HttpPost]
        [Authorize]
        public async Task<System.Web.Mvc.ActionResult> DateNow([RequestBody]Pengguna item)
        {
            var context = new Bespoke.Sph.Domain.SphDataContext();
            if (null == item) throw new ArgumentNullException("item");
            var ed = await context.LoadOneAsync<EntityDefinition>(d => d.Name == "Pengguna");
            var brokenRules = new ObjectCollection<ValidationResult>();
            if (brokenRules.Count > 0) return Json(new { success = false, rules = brokenRules.ToArray() });

            var operation = ed.EntityOperationCollection.Single(o => o.WebId == "9a62bd85-4691-458e-f05b-4b1076f38fa2");
            var rc = new RuleContext(item);
            var setter1 = operation.SetterActionChildCollection.Single(a => a.WebId == "17a78198-334a-4423-e06a-a111bf12cbd2");
            item.TarikhDaftar = (System.DateTime)setter1.Field.GetValue(rc);

            if (item.IsNewItem) item.Id = Guid.NewGuid().ToString();

            using (var session = context.OpenSession())
            {
                session.Attach(item);
                await session.SubmitChanges("DateNow");
            }
            return Json(new { success = true, message = "", status = "OK", id = item.Id });
        }
        //exec:TarikhKemaskiniNow
        [HttpPost]
        [Authorize]
        public async Task<System.Web.Mvc.ActionResult> TarikhKemaskiniNow([RequestBody]Pengguna item)
        {
            var context = new Bespoke.Sph.Domain.SphDataContext();
            if (null == item) throw new ArgumentNullException("item");
            var ed = await context.LoadOneAsync<EntityDefinition>(d => d.Name == "Pengguna");
            var brokenRules = new ObjectCollection<ValidationResult>();
            if (brokenRules.Count > 0) return Json(new { success = false, rules = brokenRules.ToArray() });

            var operation = ed.EntityOperationCollection.Single(o => o.WebId == "a97bdf39-8490-4f87-bac8-e6925c7685c6");
            var rc = new RuleContext(item);
            var setter1 = operation.SetterActionChildCollection.Single(a => a.WebId == "11c4e05a-4c4c-4e98-bb84-e0b84f4013d8");
            item.TarikhKemaskini = (System.DateTime)setter1.Field.GetValue(rc);

            if (item.IsNewItem) item.Id = Guid.NewGuid().ToString();

            using (var session = context.OpenSession())
            {
                session.Attach(item);
                await session.SubmitChanges("TarikhKemaskiniNow");
            }
            return Json(new { success = true, message = "", status = "OK", id = item.Id });
        }
        //exec:validate
        [HttpPost]
        public async Task<System.Web.Mvc.ActionResult> Validate(string id, [RequestBody]Pengguna item)
        {
            var context = new Bespoke.Sph.Domain.SphDataContext();
            if (null == item) throw new ArgumentNullException("item");
            var ed = await context.LoadOneAsync<EntityDefinition>(d => d.Name == "Pengguna");
            var brokenRules = new ObjectCollection<ValidationResult>();
            var rules = id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var r in rules)
            {
                var appliedRules = ed.BusinessRuleCollection.Where(b => b.Name == r);
                ValidationResult result = item.ValidateBusinessRule(appliedRules);

                if (!result.Success)
                {
                    brokenRules.Add(result);
                }
            }
            if (brokenRules.Count > 0) return Json(new { success = false, rules = brokenRules.ToArray() });


            return Json(new { success = true, status = "OK", id = item.Id });
        }
        //exec:Remove
        [HttpDelete]
        public async Task<System.Web.Mvc.ActionResult> Remove(string id)
        {

            var repos = ObjectBuilder.GetObject<IRepository<Pengguna>>();
            var item = await repos.LoadOneAsync(id);
            if (null == item)
                return new HttpNotFoundResult();

            var context = new Bespoke.Sph.Domain.SphDataContext();
            using (var session = context.OpenSession())
            {
                session.Delete(item);
                await session.SubmitChanges("delete");
            }
            this.Response.ContentType = "application/json; charset=utf-8";
            return Json(new { success = true, status = "OK", id = item.Id });
        }
        //exec:Schemas
        public async Task<System.Web.Mvc.ActionResult> Schemas()
        {
            var context = new SphDataContext();
            var ed = await context.LoadOneAsync<EntityDefinition>(t => t.Name == "Pengguna");
            var script = await ed.GenerateCustomXsdJavascriptClassAsync();
            this.Response.ContentType = "application/javascript";
            return Content(script);
        }
    }
}
