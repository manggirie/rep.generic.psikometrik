using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_tblprsideincome.Domain
{
    public partial class tblprsideincomeController : System.Web.Mvc.Controller
    {
        //exec:Search
        public async Task<System.Web.Mvc.ActionResult> Search()
        {

            var json = Bespoke.Sph.Web.Helpers.ControllerHelpers.GetRequestBody(this);
            var request = new System.Net.Http.StringContent(json);
            var url = "epsikologi/tblprsideincome/_search";

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
        public async Task<System.Web.Mvc.ActionResult> Save([RequestBody]tblprsideincome item)
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
            return Json(new { success = true, status = "OK", id = item.Id, href = "tblprsideincome/" + item.Id });
        }
        //exec:Kemaskini
        [HttpPost]
        public async Task<System.Web.Mvc.ActionResult> Kemaskini([RequestBody]tblprsideincome item)
        {
            var context = new Bespoke.Sph.Domain.SphDataContext();
            if (null == item) throw new ArgumentNullException("item");
            var ed = await context.LoadOneAsync<EntityDefinition>(d => d.Name == "tblprsideincome");
            var brokenRules = new ObjectCollection<ValidationResult>();
            if (brokenRules.Count > 0) return Json(new { success = false, rules = brokenRules.ToArray() });

            var operation = ed.EntityOperationCollection.Single(o => o.WebId == "96b3f279-1e89-430b-e423-f790f1bdbc7e");
            var rc = new RuleContext(item);

            if (item.IsNewItem) item.Id = Guid.NewGuid().ToString();

            using (var session = context.OpenSession())
            {
                session.Attach(item);
                await session.SubmitChanges("Kemaskini");
            }
            return Json(new { success = true, message = "Maklumat Berjaya DikemasKini", status = "OK", id = item.Id });
        }
        //exec:Simpan
        [HttpPost]
        public async Task<System.Web.Mvc.ActionResult> Simpan([RequestBody]tblprsideincome item)
        {
            var context = new Bespoke.Sph.Domain.SphDataContext();
            if (null == item) throw new ArgumentNullException("item");
            var ed = await context.LoadOneAsync<EntityDefinition>(d => d.Name == "tblprsideincome");
            var brokenRules = new ObjectCollection<ValidationResult>();
            if (brokenRules.Count > 0) return Json(new { success = false, rules = brokenRules.ToArray() });

            var operation = ed.EntityOperationCollection.Single(o => o.WebId == "ae31a320-1414-4b41-9ade-eb3b17ef4ba2");
            var rc = new RuleContext(item);

            if (item.IsNewItem) item.Id = Guid.NewGuid().ToString();

            using (var session = context.OpenSession())
            {
                session.Attach(item);
                await session.SubmitChanges("Simpan");
            }
            return Json(new { success = true, message = "Maklumat Berjaya Disimpan", status = "OK", id = item.Id });
        }
        //exec:validate
        [HttpPost]
        public async Task<System.Web.Mvc.ActionResult> Validate(string id, [RequestBody]tblprsideincome item)
        {
            var context = new Bespoke.Sph.Domain.SphDataContext();
            if (null == item) throw new ArgumentNullException("item");
            var ed = await context.LoadOneAsync<EntityDefinition>(d => d.Name == "tblprsideincome");
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

            var repos = ObjectBuilder.GetObject<IRepository<tblprsideincome>>();
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
            var ed = await context.LoadOneAsync<EntityDefinition>(t => t.Name == "tblprsideincome");
            var script = await ed.GenerateCustomXsdJavascriptClassAsync();
            this.Response.ContentType = "application/javascript";
            return Content(script);
        }
    }
}
