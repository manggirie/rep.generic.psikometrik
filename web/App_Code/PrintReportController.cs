using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace web.sph.App_Code
{
    [Authorize(Roles = "JanaLaporan")]
    [RoutePrefix("cetak-laporan")]
    public partial class PrintReportController : Controller
    {
        public PrintReportController()
        {
            ConfigHelper.RegisterDependencies();
        }

        [Route("xls/{id}")]
        public FilePathResult GetExcelFile(string id)
        {
            var path = Path.Combine(Path.GetTempPath(), id);
            return File(path, MimeMapping.GetMimeMapping(id), $"Senarai Responden {DateTime.Now:yyyy MMMM dd-HHmm}.xlsx");
        }

    }
}
