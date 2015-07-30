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
    [Authorize(Roles="JanaLaporan")]
    [RoutePrefix("cetak-laporan")]
    public class PrintReportController : Controller
    {
    	public PrintReportController()
    	{
    		ConfigHelper.RegisterDependencies();
    	}


      	[Route("indikator/hlp/{id}")]
      	public async Task<ActionResult> HlpIndikator(string id )
      	{
      		var context = new SphDataContext();
      		var sesi = await context.LoadOneAsync<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>(x => x.Id == id);
          var user = await context.LoadOneAsync<Bespoke.epsikologi_pengguna.Domain.Pengguna>(x => x.MyKad == sesi.MyKad);

          var query = context.CreateQueryable<Bespoke.epsikologi_skorhlp.Domain.SkorHlp>();
          var lo = await context.LoadAsync(query, size:1000);
          var scoreTables = lo.ItemCollection;


          var rq = context.CreateQueryable<Bespoke.epsikologi_hlprecomendation.Domain.HlpRecomendation>();
          var rlo = await context.LoadAsync(rq, size:200);
          var recommendations = rlo.ItemCollection;


      		if(null == sesi)
      			return HttpNotFound("Cannot find SesiUjian " + id);
        	if(null == sesi)
        		return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

          var vm = new HlpTraitViewModel(sesi, user, scoreTables.ToArray(), recommendations.ToArray());
      		return View("Indikator-Hlp-" + user.Jantina, vm);
      	}

    	[Route("trait/hlp/{id}")]
    	public async Task<ActionResult> PrintTraitForHlp(string id )
    	{
    		var context = new SphDataContext();
    		var sesi = await context.LoadOneAsync<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>(x => x.Id == id);
        var user = await context.LoadOneAsync<Bespoke.epsikologi_pengguna.Domain.Pengguna>(x => x.MyKad == sesi.MyKad);

        var query = context.CreateQueryable<Bespoke.epsikologi_skorhlp.Domain.SkorHlp>();
        var lo = await context.LoadAsync(query, size:1000);
        var scoreTables = lo.ItemCollection;


        var rq = context.CreateQueryable<Bespoke.epsikologi_hlprecomendation.Domain.HlpRecomendation>();
        var rlo = await context.LoadAsync(rq, size:200);
        var recommendations = rlo.ItemCollection;


    		if(null == sesi)
    			return HttpNotFound("Cannot find SesiUjian " + id);
      	if(null == sesi)
      		return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

        var vm = new HlpTraitViewModel(sesi, user, scoreTables.ToArray(), recommendations.ToArray());
    		return View("Trait-Hlp-" + user.Jantina, vm);
    	}


    	[Route("trait/ip/{id}")]
    	public async Task<ActionResult> PrintSesiUjianTraitIp(string id )
    	{
    		var context = new SphDataContext();
    		var sesi = await context.LoadOneAsync<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>(x => x.Id == id);
        var user = await context.LoadOneAsync<Bespoke.epsikologi_pengguna.Domain.Pengguna>(x => x.MyKad == sesi.MyKad);
    		if(null == sesi)
    			return HttpNotFound("Cannot find SesiUjian " + id);
      	if(null == sesi)
      		return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

        var vm = new IpTraitViewModel(sesi, user);
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
