using System.Threading.Tasks;
using System.Web.Mvc;
using Bespoke.Sph.Domain;

namespace web.sph.App_Code
{
    [Authorize]
    public class HrmisController : Controller
    {
        public HrmisController()
        {
            ConfigHelper.RegisterDependencies();
        }
        

        public async Task<ActionResult> GetUserDetailsByIcNo(string icno)
        {

            var context = ObjectBuilder.GetObject("hrmis");
            var data = await context.GetUserDetailsByIcNo(icno);
            return Json(new { success = true, data, status = "OK" });
        }

    }

}
