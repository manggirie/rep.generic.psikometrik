using System.Threading.Tasks;
using System.Web.Mvc;
using Bespoke.epsikologi_hlprecomendation.Domain;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_skorhlp.Domain;
using Bespoke.Sph.Domain;

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
