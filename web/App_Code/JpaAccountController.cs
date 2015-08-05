using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Bespoke.Sph.Domain;
using Newtonsoft.Json;

namespace web.sph.App_Code
{
    [RoutePrefix("jpa-account")]
    public class JpaAccountController : Controller
    {
      	[Authorize]
      	[HttpPost]
      	[Route("profile")]
        public async Task<ActionResult> UpdateUser(UserProfile profile)
        {
            var context = new SphDataContext();
            var userprofile = await context.LoadOneAsync<UserProfile>(p => p.UserName == User.Identity.Name)
                ?? new UserProfile();
            userprofile.UserName = User.Identity.Name;
            userprofile.Email = profile.Email;
            userprofile.Telephone = profile.Telephone;
            userprofile.FullName = profile.FullName;
            userprofile.StartModule = profile.StartModule;
            userprofile.Language = profile.Language;

            if (userprofile.IsNewItem) userprofile.Id = userprofile.UserName.ToIdFormat();

            using (var session = context.OpenSession())
            {
                session.Attach(userprofile);
                await session.SubmitChanges();
            }
            this.Response.ContentType = "application/json; charset=utf-8";
            return Content(JsonConvert.SerializeObject(userprofile));


        }


        [AllowAnonymous]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(JpaLoginModel model, string returnUrl)
        {
            var logger = ObjectBuilder.GetObject<ILogger>();
            if (ModelState.IsValid)
            {
                var directory = ObjectBuilder.GetObject<IDirectoryService>();
                if (await directory.AuthenticateAsync(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    var context = new SphDataContext();
                    var profile = await context.LoadOneAsync<UserProfile>(u => u.UserName == model.UserName);
                    await logger.LogAsync(new LogEntry { Log = EventLog.Security });
                    if (null != profile)
                    {
                        if (!profile.HasChangedDefaultPassword)
                            return RedirectToAction("ChangePassword");
                        if (returnUrl == "/" ||
                            returnUrl.Equals("/epsikologi", StringComparison.InvariantCultureIgnoreCase) ||
                            returnUrl.Equals("/epsikologi#", StringComparison.InvariantCultureIgnoreCase) ||
                            returnUrl.Equals("/epsikologi/", StringComparison.InvariantCultureIgnoreCase) ||
                            returnUrl.Equals("/epsikologi/#", StringComparison.InvariantCultureIgnoreCase) ||
                            string.IsNullOrWhiteSpace(returnUrl))
                            return Redirect("/epsikologi#" + profile.StartModule);
                    }
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return Redirect("epsikologi#");
                }
                var user = await directory.GetUserAsync(model.UserName);
                await logger.LogAsync(new LogEntry { Log = EventLog.Security, Message = "Login Failed" });
                if (null != user && user.IsLockedOut)
                    ModelState.AddModelError("", "Your acount has beeen locked, Please contact your administrator.");
                else
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            return View(model);
        }


       [AllowAnonymous]
       [Route("change-password")]
       public ActionResult ChangePassword()
       {
           return View();
       }

      [HttpPost]
      [AllowAnonymous]
      [Route("change-password")]
      public async Task<ActionResult> ChangePassword(ChangePaswordModel model)
      {
          var userName = User.Identity.Name;

          if (!Membership.ValidateUser(userName, model.OldPassword))
          {
              return Json(new { success = false, status = "PASSWORD_INCORRECT", message = "Your old password is incorrect", user = userName });
          }
          if (model.Password != model.ConfirmPassword)
              return Json(new { success = false, status = "PASSWORD_DOESNOT_MATCH", message = "Your password is not the same" });


          var user = Membership.GetUser(userName);
          if (null == user) throw new Exception("Cannot find user");

          try
          {
            var valid = user.ChangePassword(model.OldPassword, model.Password);
            if (!valid)
              return Json(new { success = false, status = "ERROR_CHANGING_PASSWORD", message = "There's an error changing your password" });
          }catch(Exception ex)
          {
              return Json(new { success = false, status = "EXCEPTION_CHANGING_PASSWORD", message = ex.Message });
          }

          var context = new SphDataContext();
          var profile = await context.LoadOneAsync<UserProfile>(u => u.UserName == User.Identity.Name);
          profile.HasChangedDefaultPassword = true;

          using (var session = context.OpenSession())
          {
              session.Attach(profile);
              await session.SubmitChanges("Change password");
          }

          if (Request.ContentType.Contains("application/json"))
          {
              this.Response.ContentType = "application/json; charset=utf-8";
              return Content(JsonConvert.SerializeObject(new { success = true, status = "OK" }));
          }

          return Redirect("/");
      }

    }

    public class ChangePaswordModel
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Message { get; set; }
    }

    public class JpaLoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
