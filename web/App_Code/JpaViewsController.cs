using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Bespoke.Sph.Domain;
using Bespoke.Sph.Web.Filters;
using Bespoke.Sph.Web.Helpers;
using Bespoke.epsikologi_pengguna.Domain;

namespace web.sph.App_Code
{
    [RoutePrefix("jpa-views")]
    public class JpaViewsController : Controller
    {
      public JpaViewsController()
      {
        ConfigHelper.RegisterDependencies();
      }
      [NoCache]
      [Authorize]
      [Route("count/pengguna-responden-dari-jabatan")]
      public async Task<ActionResult> CountForRespondenDariJabatan()
      {
          var context = new SphDataContext();
          var penyelaras = await context.LoadOneAsync<Pengguna>(x => x.MyKad == User.Identity.Name);

          var count = await context.GetCountAsync<Pengguna>(x => x.NamaJabatan == penyelaras.NamaJabatan);
          return Json(new {hits = new {total = count}}, JsonRequestBehavior.AllowGet);
      }




    }
}
