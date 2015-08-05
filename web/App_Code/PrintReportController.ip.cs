using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aspose.Pdf;
using Bespoke.epsikologi_iprecommendation.Domain;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_permohonan.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_ujian.Domain;
using Bespoke.Sph.Domain;
using Document = Aspose.Pdf.Document;

namespace web.sph.App_Code
{
    public partial class PrintReportController
    {

        [Route("trait-html/ip/{id}")]
        public async Task<ActionResult> PrintSesiUjianTraitIp(string id)
        {
            var vm = await GetIpTraitViewModelAsync(id);
            if (null == vm)
                return HttpNotFound("Cannot find SesiUjian " + id);
            return View("Trait-Ip", vm);
        }


        [Route("trait/ip/{id}")]
        public async Task<ActionResult> PrintPdfTraitIp(string id)
        {
            var license = new License();
            license.SetLicense(@"C:\project\rep.generic.psikometrik\lib\Aspose.Pdf.lic");
            license.Embedded = true;

            var vm = await GetIpTraitViewModelAsync(id);
            var html = RenderViewToString(this, "Trait-Ip", vm);

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(html)))
            {
                var options = new HtmlLoadOptions(ConfigurationManager.BaseUrl);
                var pdf = new Document(stream, options);
                options.PageInfo.IsLandscape = false;

                var os = new MemoryStream();
                pdf.Save(os, SaveFormat.Pdf);
                os.Position = 0;
                return File(os, MimeMapping.GetMimeMapping(".pdf"), "Laporan Indeks Personaliti (IP).pdf");


            }

        }

        private async Task<IpTraitViewModel> GetIpTraitViewModelAsync(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Pengguna>(x => x.MyKad == sesi.MyKad);
            var ujian = await context.LoadOneAsync<Ujian>(x => x.Id == sesi.NamaUjian);
            var permohonan = await context.LoadOneAsync<Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);

            if (null == sesi || null == user)
                return null;

            var vm = new IpTraitViewModel(sesi, user)
            {
                Permohonan = permohonan,
                Ujian = ujian
            };

            vm.Recommendation = await context.LoadOneAsync<IpRecommendation>(x => x.Skor == vm.Result);
            if (null == vm.Recommendation)
                throw new InvalidOperationException("Cannot find IpRecommendation for " + vm.Result);

            return vm;
        }


        [Route("indikator/ip/{id}")]
        public async Task<ActionResult> PrintSesiUjianIndikator(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);

            if (null == sesi)
                return HttpNotFound("Cannot find SesiUjian " + id);

            return View("Indikator-" + sesi.NamaUjian, sesi);
        }

        public static string RenderPartialViewToString(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.ToString();
            }
        }

        public static string RenderViewToString(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindView(controller.ControllerContext, viewName, "~/Views/PrintReport/_MasterPage.cshtml");
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.ToString();
            }
        }

    }
}
