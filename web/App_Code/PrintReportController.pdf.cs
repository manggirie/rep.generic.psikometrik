using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Aspose.Pdf;
using Bespoke.Sph.Domain;
using Document = Aspose.Pdf.Document;
using PageSize = Aspose.Pdf.Generator.PageSize;

namespace web.sph.App_Code
{
    public partial class PrintReportController
    {

        public ActionResult Pdf(LaporanViewModel vm, string view, Func<string, string> htmlProcessing = null, bool isLandscape = false, int tryCount = 0)
        {
            var license = new License();
            license.SetLicense(ConfigurationManager.BaseDirectory + @"\lib\Aspose.Pdf.lic");
            license.Embedded = true;

            var html = RenderViewToString(this, view, vm);
            if (htmlProcessing != null) html = htmlProcessing(html);

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(html)))
            {
                try
                {
                    var page = new PageInfo
                    {
                        Height = isLandscape ? PageSize.A4Width : PageSize.A4Height,
                        Width = isLandscape ? PageSize.A4Height : PageSize.A4Width,
                        IsLandscape = isLandscape,
                        Margin = new MarginInfo(8, 10, 8, 10),

                    };
                    var options = new HtmlLoadOptions(ConfigurationManager.BaseUrl) { PageInfo = page };
                    var pdf = new Document(stream, options) { PageInfo = page };


                    var os = new MemoryStream();
                    pdf.Save(os, SaveFormat.Pdf);
                    os.Position = 0;
                    return File(os, MimeMapping.GetMimeMapping(".pdf"), $"{vm.Ujian.NamaUjian}-{vm.Sesi.MyKad}.pdf");
                }
                catch (NotSupportedException e) when (e.Message.Contains("woff") && tryCount < 3)
                {
                    return Pdf(vm, view, htmlProcessing, isLandscape, tryCount + 1);
                }
                catch (NotSupportedException e) when (e.Message.Contains("woff") && tryCount >= 3)
                {
                    return Content(html);
                }


            }

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
