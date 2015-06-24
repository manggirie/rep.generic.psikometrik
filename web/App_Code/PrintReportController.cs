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
    	public async Task<ActionResult> PrintSesiUjianTrait(string id )
    	{
    		var context = new SphDataContext();
    		var sesi = await context.LoadOneAsync<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>(x => x.Id == id);
    		
    		if(null == sesi)
    			return HttpNotFound("Cannot find SesiUjian " + id);

    		return View("Trait-" + sesi.NamaUjian, sesi);
    	}


    	[Route("indikator/{id}")]
    	public async Task<ActionResult> PrintSesiUjianIndikator(string id )
    	{
    		var context = new SphDataContext();
    		var sesi = await context.LoadOneAsync<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>(x => x.Id == id);
    		
    		if(null == sesi)
    			return HttpNotFound("Cannot find SesiUjian " + id);

    		return View("Indikator-" + sesi.NamaUjian, sesi);
    	}
    }
}