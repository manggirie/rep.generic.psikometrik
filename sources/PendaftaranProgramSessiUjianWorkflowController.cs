using System.Web.Mvc;
using System.Net.Http;
using System;
using Bespoke.Sph.Domain;
using System.Threading.Tasks;

namespace Bespoke.Sph.Workflows_PendaftaranProgramSessiUjian_0
{
    [RoutePrefix("wf/pendaftaran-program-sessi-ujian/v0")]
    public partial class PendaftaranProgramSessiUjianWorkflowController : Controller
    {



        //exec:Search
        [HttpPost]
        [Route("search")]
        public async Task<ActionResult> Search()
        {

            var json = Bespoke.Sph.Web.Helpers.ControllerHelpers.GetRequestBody(this);
            var request = new StringContent(json);
            var url = string.Format("{0}/{1}/workflow_pendaftaran-program-sessi-ujian_0/_search", ConfigurationManager.ElasticSearchHost, ConfigurationManager.ElasticSearchIndex);

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, request);
                var content = response.Content as StreamContent;

                if (null == content) throw new Exception("Cannot execute query on es " + request);
                return Content(await content.ReadAsStringAsync(), "application/json; charset=utf-8");
            }

        }


        [HttpGet]
        [Route("schemas")]
        public async Task<ActionResult> Schemas()
        {
            var store = ObjectBuilder.GetObject<IBinaryStore>();
            var doc = await store.GetContentAsync("wd.pendaftaran-program-sessi-ujian.0");
            WorkflowDefinition wd;
            using (var stream = new System.IO.MemoryStream(doc.Content))
            {
                wd = stream.DeserializeFromJson<WorkflowDefinition>();
            }


            var script = await wd.GenerateCustomXsdJavascriptClassAsync();
            this.Response.ContentType = "application/javascript";
            return Content(script);
        }

    }
}
