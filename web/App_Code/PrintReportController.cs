using System;
using System.Web.Mvc;
using System.Text;
using Bespoke.Sph.Domain;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using System.Collections.Concurrent;
using System.IO;
using System.Diagnostics;

namespace web.sph.App_Code
{
    [RoutePrefix("cetak-laporan")]
    public class PrintReportController : Controller
    {
    	public PrintReportController()
    	{
    		ConfigHelper.RegisterDependencies();
    	}
    	[Route("trait/{id}")]
    	public async Task<ActionResult> PrintSesiUjianTrain(string id )
    	{
    		var context = new SphDataContext();
    		var sesi = await context.LoadOneAsync<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>(x => x.Id == id);
    		return Content(sesi.ToJsonString(true), "application/json", Encoding.UTF8);
    	}
    }
}