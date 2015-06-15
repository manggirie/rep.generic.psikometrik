using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_sesiujian.Domain
{
    public partial class SesiUjianController : System.Web.Mvc.Controller
    {
        //exec:Search
        public async Task<System.Web.Mvc.ActionResult> Search()
        {

            var json = Bespoke.Sph.Web.Helpers.ControllerHelpers.GetRequestBody(this);
            var request = new System.Net.Http.StringContent(json);
            var url = "epsikologi/sesiujian/_search";

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
        public async Task<System.Web.Mvc.ActionResult> Save([RequestBody]SesiUjian item)
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
            return Json(new { success = true, status = "OK", id = item.Id, href = "sesiujian/" + item.Id });
        }
        //exec:SubmitSesiUjian
        [HttpPost]
        [Authorize(Roles = "Responden")]
        public async Task<System.Web.Mvc.ActionResult> SubmitSesiUjian([RequestBody]SesiUjian item)
        {
            var context = new Bespoke.Sph.Domain.SphDataContext();
            if (null == item) throw new ArgumentNullException("item");
            var ed = await context.LoadOneAsync<EntityDefinition>(d => d.Name == "SesiUjian");
            var brokenRules = new ObjectCollection<ValidationResult>();
            if (brokenRules.Count > 0) return Json(new { success = false, rules = brokenRules.ToArray() });

            var operation = ed.EntityOperationCollection.Single(o => o.WebId == "2c06f3df-8555-4533-ddd4-3baf1751675a");
            var rc = new RuleContext(item);
            var setter1 = operation.SetterActionChildCollection.Single(a => a.WebId == "0b7e6570-9623-4c38-fb14-cb414771f783");
            item.TarikhUjian = (System.DateTime)setter1.Field.GetValue(rc);
            var setter2 = operation.SetterActionChildCollection.Single(a => a.WebId == "ee17774f-370e-4835-c9d4-43a43fe96bd6");
            item.MasaTamat = (System.DateTime)setter2.Field.GetValue(rc);
            var setter3 = operation.SetterActionChildCollection.Single(a => a.WebId == "e9d49346-b54f-4fc9-e290-db6fa42b0524");
            item.Status = (System.String)setter3.Field.GetValue(rc);

            if (item.IsNewItem) item.Id = Guid.NewGuid().ToString();

            using (var session = context.OpenSession())
            {
                session.Attach(item);
                await session.SubmitChanges("SubmitSesiUjian");
            }
            return Json(new { success = true, message = "", status = "OK", id = item.Id });
        }
        //exec:validate
        [HttpPost]
        public async Task<System.Web.Mvc.ActionResult> Validate(string id, [RequestBody]SesiUjian item)
        {
            var context = new Bespoke.Sph.Domain.SphDataContext();
            if (null == item) throw new ArgumentNullException("item");
            var ed = await context.LoadOneAsync<EntityDefinition>(d => d.Name == "SesiUjian");
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

            var repos = ObjectBuilder.GetObject<IRepository<SesiUjian>>();
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
            var ed = await context.LoadOneAsync<EntityDefinition>(t => t.Name == "SesiUjian");
            var script = await ed.GenerateCustomXsdJavascriptClassAsync();
            this.Response.ContentType = "application/javascript";
            return Content(script);
        }
    }
}
