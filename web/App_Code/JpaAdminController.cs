using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace web.sph.App_Code
{
    [RoutePrefix("jpa-admin")]
    //[Authorize(Roles = "developers")]
    public class JpaAdminController : Controller
    {
        //[Authorize]
        [HttpDelete]
        [Route("all-users")]
        public ActionResult RemoveAllUsers()
        {
            int page = 1;
            int count;

            var users = Membership.GetAllUsers(page, 40, out count);
            var deleted = new List<string>();

            foreach (MembershipUser u in users)
            {
                if (Roles.IsUserInRole(u.UserName, "developers")) continue;
                if (Roles.IsUserInRole(u.UserName, "administrators")) continue;

                Membership.DeleteUser(u.UserName, true);
                deleted.Add(u.UserName);
            }

            return Json(new { success = deleted.Count > 0, deleted });


        }


    }
}