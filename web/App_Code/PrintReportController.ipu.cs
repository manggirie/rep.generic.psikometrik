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
using Bespoke.epsikologi_ipurecommendation.Domain;
using Bespoke.epsikologi_skoripu.Domain;
using Bespoke.epsikologi_ipupercentilenorms.Domain;


namespace web.sph.App_Code
{
    public partial class PrintReportController
    {

      	[Route("trait/ipu/{id}")]
      	public async Task<ActionResult> PrintSesiUjianTraitIpu(string id )
      	{
      		var context = new SphDataContext();
      		var sesi = await  context.LoadOneAsync<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>(x => x.Id == id);
          var user = await context.LoadOneAsync<Bespoke.epsikologi_pengguna.Domain.Pengguna>(x => x.MyKad == sesi.MyKad);

          var ujianTask = context.LoadOneAsync<Bespoke.epsikologi_ujian.Domain.Ujian>(x => x.Id == sesi.NamaUjian);
          var permohonanTask = context.LoadOneAsync<Bespoke.epsikologi_permohonan.Domain.Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);
          await Task.WhenAll(ujianTask, permohonanTask);


        	if(null == sesi)
      			return HttpNotFound("Cannot find SesiUjian " + id);
        	if(null == sesi)
        		return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

          var vm = new IpuTraitViewModel(sesi);
          vm.Permohonan = await permohonanTask;
          vm.Ujian = await ujianTask;
          vm.Pengguna = user;

          /* */
          var apTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.A.ToString());
          var bpTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.B.ToString());
          var cpTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.C.ToString());
          var dpTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.D.ToString());
          var epTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.E.ToString());
          var fpTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.F.ToString());
          var gpTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.G.ToString());
          var hpTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.H.ToString());
          var ipTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.I.ToString());
          await Task.WhenAll(apTask, bpTask, cpTask, epTask, fpTask, gpTask, hpTask, ipTask);

          var ap = await apTask;
          var bp = await bpTask;
          var cp = await cpTask;
          var dp = await dpTask;
          var ep = await epTask;
          var fp = await fpTask;
          var gp = await gpTask;
          var hp = await hpTask;
          var ip = await ipTask;

          var a = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "A" && x.NilaiMin <= int.Parse(ap.A) && int.Parse(ap.A) <= x.NilaiMax);
          var b = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "B" && x.NilaiMin <= int.Parse(bp.B) && int.Parse(bp.B) <= x.NilaiMax);
          var c = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "C" && x.NilaiMin <= int.Parse(bp.C) && int.Parse(cp.C) <= x.NilaiMax);
          var d = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "D" && x.NilaiMin <= int.Parse(dp.D) && int.Parse(dp.D) <= x.NilaiMax);
          var e = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "E" && x.NilaiMin <= int.Parse(ep.E) && int.Parse(ep.E) <= x.NilaiMax);
          var f = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "F" && x.NilaiMin <= int.Parse(fp.F) && int.Parse(fp.F) <= x.NilaiMax);
          var g = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "G" && x.NilaiMin <= int.Parse(gp.G) && int.Parse(gp.G) <= x.NilaiMax);
          var h = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "H" && x.NilaiMin <= int.Parse(hp.H) && int.Parse(hp.H) <= x.NilaiMax);
          var i = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "I" && x.NilaiMin <= int.Parse(ip.I) && int.Parse(ip.I) <= x.NilaiMax);

          vm.RecommendationA = await a;
          vm.RecommendationB = await b;
          vm.RecommendationC = await c;
          vm.RecommendationD = await d;
          vm.RecommendationE = await e;
          vm.RecommendationF = await f;
          vm.RecommendationG = await g;
          vm.RecommendationH = await h;
          vm.RecommendationI = await i;
          vm.RecommendationJ = await context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "J" && x.NilaiMin <= vm.J && vm.J <= x.NilaiMax);

      		return View("Trait-Ipu", vm);

      	}


    	[Route("indikator/ipu/{id}")]
    	public async Task<ActionResult> PrintSesiUjianIndikatorIpu(string id )
    	{
    		var context = new SphDataContext();
    		var sesi = await  context.LoadOneAsync<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>(x => x.Id == id);
        var user = await context.LoadOneAsync<Bespoke.epsikologi_pengguna.Domain.Pengguna>(x => x.MyKad == sesi.MyKad);

        var ujianTask = context.LoadOneAsync<Bespoke.epsikologi_ujian.Domain.Ujian>(x => x.Id == sesi.NamaUjian);
        var permohonanTask = context.LoadOneAsync<Bespoke.epsikologi_permohonan.Domain.Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);
        await Task.WhenAll(ujianTask, permohonanTask);


      	if(null == sesi)
    			return HttpNotFound("Cannot find SesiUjian " + id);
      	if(null == sesi)
      		return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

        var vm = new IpuTraitViewModel(sesi);
        vm.Permohonan = await permohonanTask;
        vm.Ujian = await ujianTask;
        vm.Pengguna = user;

        /* */
        var apTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <=  vm.A && vm.A <= x.NilaiMax);
        var bpTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <=  vm.B && vm.B <= x.NilaiMax);
        var cpTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <=  vm.C && vm.D <= x.NilaiMax);
        var dpTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <=  vm.D && vm.D <= x.NilaiMax);
        var epTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <=  vm.E && vm.E <= x.NilaiMax);
        var fpTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <=  vm.F && vm.F <= x.NilaiMax);
        var gpTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <=  vm.G && vm.G <= x.NilaiMax);
        var hpTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <=  vm.H && vm.H <= x.NilaiMax);
        var ipTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <=  vm.I && vm.I <= x.NilaiMax);
        await Task.WhenAll(apTask, bpTask, cpTask, epTask, fpTask, gpTask, hpTask, ipTask);

        var ap = await apTask;
        var bp = await bpTask;
        var cp = await cpTask;
        var dp = await dpTask;
        var ep = await epTask;
        var fp = await fpTask;
        var gp = await gpTask;
        var hp = await hpTask;
        var ip = await ipTask;

        vm.SkorA = ap.Percentile;
        vm.SkorB = bp.Percentile;
        vm.SkorC = cp.Percentile;
        vm.SkorD = dp.Percentile;
        vm.SkorE = ep.Percentile;
        vm.SkorF = fp.Percentile;
        vm.SkorG = gp.Percentile;
        vm.SkorH = hp.Percentile;
        vm.SkorI = ip.Percentile;
        vm.SkorJ = vm.J;

    		return View("Indikator-Ipu-" + user.Jantina, vm);

    	}


    }
}
