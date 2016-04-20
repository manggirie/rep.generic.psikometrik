using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Bespoke.Sph.Domain;

[Authorize(Roles = "administartors,developers")]
[RoutePrefix("jpa-management")]
public class JpaManagementController : Controller
{

    [HttpDelete]
    [Route("users/{user}")]
    public async Task<ActionResult> RemoveUser(string user)
    {
        var context = new SphDataContext();
        var profile = await context.LoadOneAsync<UserProfile>(x => x.UserName == user);
        if (null != profile)
        {
            using (var session = context.OpenSession())
            {
                session.Delete(profile);
                await session.SubmitChanges("Remove");
            }
        }
        var member = Membership.GetUser(user);
        if (null != member)
        {
            Membership.DeleteUser(user, true);
        }
        return Content("{success : true, status : \"OK\"}", "application/json");
    }

}