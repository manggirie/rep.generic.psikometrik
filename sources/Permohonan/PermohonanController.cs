using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_permohonan.Domain
{
    public partial class PermohonanController : System.Web.Mvc.Controller
    {
        //exec:Search
        public async Task<System.Web.Mvc.ActionResult> Search()
        {

            var json = Bespoke.Sph.Web.Helpers.ControllerHelpers.GetRequestBody(this);
            var request = new System.Net.Http.StringContent(json);
            var url = "epsikologi/permohonan/_search";

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
        public async Task<System.Web.Mvc.ActionResult> Save([RequestBody]Permohonan item)
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
            return Json(new { success = true, status = "OK", id = item.Id, href = "permohonan/" + item.Id });
        }
        //exec:PermohonanDariPenyelaras
        [HttpPost]
        public async Task<System.Web.Mvc.ActionResult> PermohonanDariPenyelaras([RequestBody]Permohonan item)
        {
            var context = new Bespoke.Sph.Domain.SphDataContext();
            if (null == item) throw new ArgumentNullException("item");
            var ed = await context.LoadOneAsync<EntityDefinition>(d => d.Name == "Permohonan");
            var brokenRules = new ObjectCollection<ValidationResult>();
            if (brokenRules.Count > 0) return Json(new { success = false, rules = brokenRules.ToArray() });

            var operation = ed.EntityOperationCollection.Single(o => o.WebId == "bbf6a1ee-6bd9-4414-9d17-c6edf9959661");
            var rc = new RuleContext(item);
            var setter1 = operation.SetterActionChildCollection.Single(a => a.WebId == "761529c5-1594-4677-ed18-e2ccc4a70f03");
            item.StatusPermohonan = (System.String)setter1.Field.GetValue(rc);
            var setter2 = operation.SetterActionChildCollection.Single(a => a.WebId == "842b2a2e-d7d0-4a44-ccc3-7ee32204b772");
            item.PermohonanNo = (System.String)setter2.Field.GetValue(rc);

            if (item.IsNewItem) item.Id = Guid.NewGuid().ToString();

            using (var session = context.OpenSession())
            {
                session.Attach(item);
                await session.SubmitChanges("PermohonanDariPenyelaras");
            }
            return Json(new { success = true, message = "", status = "OK", id = item.Id });
        }
        //exec:UrusetiaProcessPermohonanDariPenyelaras
        [HttpPost]
        public async Task<System.Web.Mvc.ActionResult> UrusetiaProcessPermohonanDariPenyelaras([RequestBody]Permohonan item)
        {
            var context = new Bespoke.Sph.Domain.SphDataContext();
            if (null == item) throw new ArgumentNullException("item");
            var ed = await context.LoadOneAsync<EntityDefinition>(d => d.Name == "Permohonan");
            var brokenRules = new ObjectCollection<ValidationResult>();
            if (brokenRules.Count > 0) return Json(new { success = false, rules = brokenRules.ToArray() });

            var operation = ed.EntityOperationCollection.Single(o => o.WebId == "e1557128-3b90-4bf5-d4ba-c975479f0e73");
            var rc = new RuleContext(item);
            var setter1 = operation.SetterActionChildCollection.Single(a => a.WebId == "4a3ffe52-23b7-4528-cc5f-b81857b9db7c");
            item.PermohonanNo = (System.String)setter1.Field.GetValue(rc);

            if (item.IsNewItem) item.Id = Guid.NewGuid().ToString();

            using (var session = context.OpenSession())
            {
                session.Attach(item);
                await session.SubmitChanges("UrusetiaProcessPermohonanDariPenyelaras");
            }
            return Json(new { success = true, message = "", status = "OK", id = item.Id });
        }
        //exec:validate
        [HttpPost]
        public async Task<System.Web.Mvc.ActionResult> Validate(string id, [RequestBody]Permohonan item)
        {
            var context = new Bespoke.Sph.Domain.SphDataContext();
            if (null == item) throw new ArgumentNullException("item");
            var ed = await context.LoadOneAsync<EntityDefinition>(d => d.Name == "Permohonan");
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

            var repos = ObjectBuilder.GetObject<IRepository<Permohonan>>();
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
            var ed = await context.LoadOneAsync<EntityDefinition>(t => t.Name == "Permohonan");
            var script = await ed.GenerateCustomXsdJavascriptClassAsync();
            this.Response.ContentType = "application/javascript";
            return Content(script);
        }
    }
}
