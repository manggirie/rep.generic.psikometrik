using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_ujian.Domain
{
    public partial class UjianController : System.Web.Mvc.Controller
    {
        //exec:Search
        public async Task<System.Web.Mvc.ActionResult> Search()
        {

            var json = Bespoke.Sph.Web.Helpers.ControllerHelpers.GetRequestBody(this);
            var request = new System.Net.Http.StringContent(json);
            var url = "epsikologi/ujian/_search";

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
        public async Task<System.Web.Mvc.ActionResult> Save([RequestBody]Ujian item)
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
            return Json(new { success = true, status = "OK", id = item.Id, href = "ujian/" + item.Id });
        }
        //exec:validate
        [HttpPost]
        public async Task<System.Web.Mvc.ActionResult> Validate(string id, [RequestBody]Ujian item)
        {
            var context = new Bespoke.Sph.Domain.SphDataContext();
            if (null == item) throw new ArgumentNullException("item");
            var ed = await context.LoadOneAsync<EntityDefinition>(d => d.Name == "Ujian");
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

            var repos = ObjectBuilder.GetObject<IRepository<Ujian>>();
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
            var ed = await context.LoadOneAsync<EntityDefinition>(t => t.Name == "Ujian");
            var script = await ed.GenerateCustomXsdJavascriptClassAsync();
            this.Response.ContentType = "application/javascript";
            return Content(script);
        }
    }
}
