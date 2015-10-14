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



    }
}
