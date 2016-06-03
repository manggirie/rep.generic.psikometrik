using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace web.sph.App_Code
{
    [Authorize(Roles = "administrators")]
    [RoutePrefix("sandbox")]
    public class SandboxController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return Content("This is the sandbox.");
        }

        [HttpPost]
        [Route("email")]
        public async Task<ActionResult> Email(SandboxEmailModel model)
        {
            using (var smtp = new SmtpClient())
            {
                var mail = new MailMessage(model.sender, model.recipient)
                {
                    Subject = model.subject,
                    Body = model.message,
                    IsBodyHtml = false
                };
                try
                {
                    await smtp.SendMailAsync(mail);
                    return Json(new { success = true, status = "E-mail was sent!" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, status = ex.Message });
                }
            }
        }

        [Route("slow")]
        public async Task<ActionResult> Slow()
        {
            await Task.Delay(5000);
            return Content("OK");
        }
    }

    public class SandboxEmailModel
    {
        public string recipient { get; set; }
        public string sender { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
    }
}