using System;
using System.Web.Mvc;

namespace web.sph.App_Code
{
    [Authorize]
    [RoutePrefix("epsikologi")]
    public class JpaHomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            if(this.User.IsInRole("Developers"))
                return View("Developers");

            return View("Default");
        }
    }
}
