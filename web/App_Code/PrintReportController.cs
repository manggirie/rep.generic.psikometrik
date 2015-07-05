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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;


namespace web.sph.App_Code
{
    [RoutePrefix("cetak-laporan")]
    public class PrintReportController : Controller
    {
    	public PrintReportController()
    	{
    		ConfigHelper.RegisterDependencies();
    	}

    	[Route("trait/ip/{id}")]
    	public async Task<ActionResult> PrintSesiUjianTraitIp(string id )
    	{
    		var context = new SphDataContext();
    		var sesi = await context.LoadOneAsync<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>(x => x.Id == id);

    		if(null == sesi)
    			return HttpNotFound("Cannot find SesiUjian " + id);

            var vm = new IpTraitViewModel(sesi);
    		return View("Trait-Ip-" + vm.Result, vm);
    	}

    	[Route("trait/ibk/{id}")]
    	public async Task<ActionResult> PrintIbkTrait(string id )
    	{
    		var context = new SphDataContext();
    		var sesi = await context.LoadOneAsync<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>(x => x.Id == id);

    		if(null == sesi)
    			return HttpNotFound("Cannot find SesiUjian " + id);

            var vm = new IbkTraitViewModel(sesi);
    		return View("Trait-Ibk-" + vm.Result, vm);
    	}


    	[Route("indikator/ip/{id}")]
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
