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

        /*
        var apTask = context.LoadOneAsync<SkorIPU>(x => x.Tret == "A" && vm.A >= x.NilaiMin && vm.A <= x.NilaiMax);
        await Task.WhenAll(apTask);

        var ap = await apTask;
        var a = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "A" && x.NilaiMin <= ap.Percentile && ap.Percentile <= x.NilaiMax);

        vm.RecommendationA = await a; */
        vm.RecommendationJ = await context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "J" && x.NilaiMin <= vm.J && vm.J <= x.NilaiMax);


    		return View("Trait-Ipu", vm);
    	}


    }
}
