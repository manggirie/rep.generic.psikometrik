using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Bespoke.epsikologi_pendaftaranprogram.Domain;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_permohonan.Domain;
using Bespoke.Sph.Domain;
using OfficeOpenXml;

namespace web.sph.App_Code
{
    //[RoutePrefix("pelajar")]
    //[Authorize(Roles = "PengurusanPenggunaJabatan, developers")]
    public class HrmisController : Controller
    {
        public HrmisController()
        {
            ConfigHelper.RegisterDependencies();
        }

        [HttpGet]
        [Route("test")]
        public ActionResult Test()
        {
            return Content("test OK");
        }

        //[HttpPost]
        //[Route("get-user-detail")]
        public async Task<ActionResult> GetUserDetailsByIcNo(string icno)
        {

            var context = ObjectBuilder.GetObject("hrmis");
            var data = await context.GetUserDetailsByIcNo(icno);
            return Json(new { success = true, data, status = "OK" });
        }

    }

}
