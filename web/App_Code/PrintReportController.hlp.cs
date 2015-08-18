using System.Threading.Tasks;
using System.Web.Mvc;
using Bespoke.epsikologi_hlprecomendation.Domain;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_skorhlp.Domain;
using Bespoke.Sph.Domain;

namespace web.sph.App_Code
{
    public partial class PrintReportController 
    {

        [Route("trait/hlp/{id}")]
        public async Task<ActionResult> PrintTraitForHlp(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Pengguna>(x => x.MyKad == sesi.MyKad);
            var ujianTask = context.LoadOneAsync<Bespoke.epsikologi_ujian.Domain.Ujian>(x => x.Id == sesi.NamaUjian);
            var permohonanTask = context.LoadOneAsync<Bespoke.epsikologi_permohonan.Domain.Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);
            await Task.WhenAll(ujianTask, permohonanTask);

            var query = context.CreateQueryable<SkorHlp>();
            var lo = await context.LoadAsync(query, size: 1000);
            var scoreTables = lo.ItemCollection;


            var rq = context.CreateQueryable<HlpRecomendation>();
            var rlo = await context.LoadAsync(rq, size: 200);
            var recommendations = rlo.ItemCollection;


            if (null == sesi)
                return HttpNotFound("Cannot find SesiUjian " + id);
            if (null == user)
                return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

            var vm = new HlpTraitViewModel(sesi, user, scoreTables.ToArray(), recommendations.ToArray())
            {
                Permohonan = await permohonanTask,
                Ujian = await ujianTask
            };

            var viewName = "Trait-Hlp-" + user.Jantina;
            const string STYLE = "border:3px solid red";
            return Pdf(viewName, vm, "~/Views/PrintReport/_MasterPage.NoHeader.cshtml",
            x => x
                .Replace($"id=\"KB{vm.KB.Point}\"", $"id=\"KB{vm.KB.Point}\"      style=\"{STYLE}\"")
                .Replace($"id=\"FR{vm.FR.Percentile}\"", $"id=\"FR{vm.FR.Percentile}\" style=\"{STYLE}\"")
                .Replace($"id=\"KT{vm.KT.Percentile}\"", $"id=\"KT{vm.KT.Percentile}\" style=\"{STYLE}\"")
                .Replace($"id=\"KC{vm.KC.Percentile}\"", $"id=\"KC{vm.KC.Percentile}\" style=\"{STYLE}\"")
                .Replace($"id=\"LP{vm.LP.Percentile}\"", $"id=\"LP{vm.LP.Percentile}\" style=\"{STYLE}\"")
                .Replace($"id=\"SM{vm.SM.Percentile}\"", $"id=\"SM{vm.SM.Percentile}\" style=\"{STYLE}\"")
                .Replace($"id=\"AS{vm.AS.Percentile}\"", $"id=\"AS{vm.AS.Percentile}\" style=\"{STYLE}\"")
                .Replace($"id=\"AF{vm.AF.Percentile}\"", $"id=\"AF{vm.AF.Percentile}\" style=\"{STYLE}\"")
                .Replace($"id=\"TL{vm.TL.Percentile}\"", $"id=\"TL{vm.TL.Percentile}\" style=\"{STYLE}\"")
                .Replace($"id=\"DT{vm.DT.Percentile}\"", $"id=\"DT{vm.DT.Percentile}\" style=\"{STYLE}\""), true);
        }



    }
}
