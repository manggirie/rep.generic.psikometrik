using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Bespoke.Sph.Domain;
using Bespoke.Sph.Web.Areas.Sph.Controllers;
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
            }
            catch (Exception ex)
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



        [AllowAnonymous]
        [HttpPost]
        [Route("forgot-password")]
        public async Task<ActionResult> ForgotPassword(string email)
        {
            var username = Membership.GetUserNameByEmail(email);
            if (string.IsNullOrWhiteSpace(username))
            {
                return Json(new { sucess = false, status = "Cannot find any user with email  " + email });
            }
            var setting = new Setting { UserName = email, Key = "ForgotPassword", Value = DateTime.Now.ToString("s"), Id = Strings.GenerateId() };
            var context = new SphDataContext();
            using (var session = context.OpenSession())
            {
                session.Attach(setting);
                await session.SubmitChanges("ForgotPassword");
            }
            using (var smtp = new SmtpClient())
            {
                var mail = new MailMessage(ConfigurationManager.FromEmailAddress, email)
                {
                    Subject = ConfigurationManager.ApplicationFullName + " Forgot password ",
                    Body = $@"Salam Sejahtera,

Anda memohon untuk membuat pertukaran kata laluan melalui alamat ini, klik di pautan dibawah untuk meneruskan pertukaran kata laluan anda
{ConfigurationManager.BaseUrl}/jpa-account/reset-password/{setting.Id}


Emel ini di jana oleh komputer.",
                    IsBodyHtml = false
                };
                await smtp.SendMailAsync(mail);
            }
            return Json(new { sucess = true, status = "ok" });
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("reset-password/{id}")]
        public async Task<ActionResult> ResetPassword(string id)
        {
            var context = new SphDataContext();
            var setting = await context.LoadOneAsync<Setting>(x => x.Id == id);
            var model = new ResetPaswordModel { IsValid = true, Id = id };

            if (null == setting)
            {
                model.IsValid = false;
                model.Mesage = "The link is invalid";
                return View(model);
            }

            model.Email = setting.UserName;
            if ((DateTime.Now - setting.CreatedDate).TotalMinutes > 10)
            {
                model.IsValid = false;
                model.Mesage = "The link has expired";
                return View(model);

            }
            var user = Membership.FindUsersByEmail(setting.UserName);
            if (user.Count == 0)
            {
                model.IsValid = false;
                model.Mesage = "Cannot find any user with email  " + model.Email;
            }
            model.Email = setting.UserName;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPaswordModel model)
        {
            var context = new SphDataContext();
            var key = await context.LoadOneAsync<Setting>(x => x.Id == model.Id);
            if (null == key)
                return HttpNotFound("Cannot find any password reset key " + model.Id);

            var username = Membership.GetUserNameByEmail(model.Email);
            if (model.Password != model.ConfirmPassword)
                return Json(new { success = false, status = "PASSWORD_DOESNOT_MATCH", message = "Kata laluan anda tidak sama" });
            if (string.IsNullOrWhiteSpace(username))
                return HttpNotFound("Cannot find any user registered with " + model.Email);

            var user = Membership.GetUser(username);
            if (null == user) throw new Exception("Cannot find any user with email " + model.Email);


            var ok = AdminController.CheckPasswordComplexity(Membership.Provider, model.Password);
            if (!ok)
                return Json(new { success = false, status = "PASSWORD_COMPLEXITY", message = "Kata laluan anda tidak mengikut kesesuaian yang ditetapkan" });

            var temp = user.ResetPassword();
            user.ChangePassword(temp, model.Password);

            var profile = await context.LoadOneAsync<UserProfile>(u => u.UserName == username)
                ?? new UserProfile
                {
                    UserName = username,
                    Email = model.Email,
                    HasChangedDefaultPassword = true,
                    Id = username,
                    StartModule = ""
                };
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
    public class ResetPaswordModel
    {
        public string ConfirmPassword { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsValid { get; set; }
        public string Mesage { get; set; }
        public string Id { get; set; }
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
