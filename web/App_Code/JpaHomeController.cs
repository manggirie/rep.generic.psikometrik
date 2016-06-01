using System;
using System.Web.Mvc;
using Bespoke.Sph.Domain;
using System.Threading.Tasks;
using System.Linq;

namespace web.sph.App_Code
{
      public class JpaHomeViewModel
     {
         public Designation Designation { get; set; }
         public UserProfile Profile { get; set; }
         public string StartModule { get; set; }
         public int TotalMessageCount { get; set; }

          public ObjectCollection<Message> Messages { get; } = new ObjectCollection<Message>();
     }


    [Authorize]
    [RoutePrefix("epsikologi")]
    public class JpaHomeController : Controller
    {
        [Route("")]
        public async Task<ActionResult> Index()
        {
            var context = new SphDataContext();
            var profile = await context.LoadOneAsync<UserProfile>(ua => ua.UserName == User.Identity.Name);
            if (null == profile)
                return View( "Default", new JpaHomeViewModel { Designation = new Designation { IsHelpVisible = false } });


            var designation = (await context.LoadOneAsync<Designation>(d => d.Name == profile.Designation)) ?? new Designation { IsHelpVisible = true, HelpUri = "/docs/" };
            designation.HelpUri = string.IsNullOrWhiteSpace(designation.HelpUri) ? "/docs/" : designation.HelpUri;

            var query = context.CreateQueryable<Message>();
            var messagesLo = await context.LoadAsync(query.Where(x => x.UserName == User.Identity.Name && x.IsRead == false),1,5,true);


            var vm = new JpaHomeViewModel
            {
                Profile = profile,
                Designation = designation,
                StartModule = "#" + profile.StartModule,
                TotalMessageCount = messagesLo.TotalRows ?? 0
            };
            vm.Messages.AddRange(messagesLo.ItemCollection);
            if(this.User.IsInRole("Developers"))
                return Redirect("/sph#dev.home");

            return View("Default", vm);
        }

        [AllowAnonymous]
        [Route("penafian")]
        public ActionResult Penafian()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("manual-pengguna")]
        public ActionResult ManualPengguna()
        {
            return View();
        }
    }
}
