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
    public partial class PrintReportController : Controller
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
        var ujian = await context.LoadOneAsync<Bespoke.epsikologi_ujian.Domain.Ujian>(x => x.Id == sesi.NamaUjian);
        var permohonan = await context.LoadOneAsync<Bespoke.epsikologi_permohonan.Domain.Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);

      	if(null == sesi)
    			return HttpNotFound("Cannot find SesiUjian " + id);
      	if(null == sesi)
      		return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

        var vm = new IpTraitViewModel(sesi, user);
        vm.Permohonan = permohonan;
        vm.Ujian = ujian;

        vm.Recommendation = await context.LoadOneAsync<Bespoke.epsikologi_iprecommendation.Domain.IpRecommendation>(x => x.Skor == vm.Result);
        if(null == vm.Recommendation)
          throw new InvalidOperationException("Cannot find IpRecommendation for " + vm.Result);

    		return View("Trait-Ip", vm);
    	}

    	[Route("trait/ibk/{id}")]
    	public async Task<ActionResult> PrintIbkTrait(string id )
    	{
    		var context = new SphDataContext();
    		var sesi = await context.LoadOneAsync<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>(x => x.Id == id);
        var user = await context.LoadOneAsync<Bespoke.epsikologi_pengguna.Domain.Pengguna>(x => x.MyKad == sesi.MyKad);
        var ujian = await context.LoadOneAsync<Bespoke.epsikologi_ujian.Domain.Ujian>(x => x.Id == sesi.NamaUjian);
        var permohonan = await context.LoadOneAsync<Bespoke.epsikologi_permohonan.Domain.Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);

    		if(null == sesi)
    			return HttpNotFound("Cannot find SesiUjian " + id);

        var vm = new IbkTraitViewModel(sesi)
        {
          Pengguna = user,
          Ujian = ujian,
          Permohonan = permohonan
        };


        var id1 = vm.KodKerjaya.Replace("/", "-");
        var id2 = id1.Substring(4,3) + "-" + id1.Substring(0,3) ;


      //  if(vm.KodKerjaya != "xxx")
      //    throw new Exception("id1 = " + id1 + " and id2 = " + id2);

        vm.IbkRecommendation = await context.LoadOneAsync<Bespoke.epsikologi_ibkrecommendation.Domain.IbkRecommendation>(
          x => x.Id == id1 || x.Id == id2 );
        vm.IbkKodKerjaya = await context.LoadOneAsync<Bespoke.epsikologi_ibkkodkerjaya.Domain.IbkKodKerjaya>(x => x.Id == vm.KodKerjaya.Substring(0,1));


        return View("Trait-Ibk", vm);
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
