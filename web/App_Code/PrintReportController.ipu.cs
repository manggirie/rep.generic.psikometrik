using System.Web.Mvc;
using Bespoke.Sph.Domain;
using System.Threading.Tasks;
using Bespoke.epsikologi_ipurecommendation.Domain;
using Bespoke.epsikologi_skoripu.Domain;
using Bespoke.epsikologi_ipupercentilenorms.Domain;


namespace web.sph.App_Code
{
    public partial class PrintReportController
    {

        [Route("trait/ipu/{id}")]
        public async Task<ActionResult> PrintSesiUjianTraitIpu(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Bespoke.epsikologi_pengguna.Domain.Pengguna>(x => x.MyKad == sesi.MyKad);

            var ujianTask = context.LoadOneAsync<Bespoke.epsikologi_ujian.Domain.Ujian>(x => x.Id == sesi.NamaUjian);
            var permohonanTask = context.LoadOneAsync<Bespoke.epsikologi_permohonan.Domain.Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);
            await Task.WhenAll(ujianTask, permohonanTask);


            if (null == sesi)
                return HttpNotFound("Cannot find SesiUjian " + id);
            if (null == user)
                return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

            var vm = new IpuTraitViewModel(sesi)
            {
                Permohonan = await permohonanTask,
                Ujian = await ujianTask,
                Pengguna = user
            };

            /* */
            var apTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.A);
            var bpTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.B);
            var cpTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.C);
            var dpTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.D);
            var epTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.E);
            var fpTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.F);
            var gpTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.G);
            var hpTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.H);
            var ipTask = context.LoadOneAsync<IpuPercentileNorms>(x => x.RawScore == vm.I);
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

            var a = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "A" && x.NilaiMin <= ap.A && ap.A <= x.NilaiMax);
            var b = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "B" && x.NilaiMin <= bp.B && bp.B <= x.NilaiMax);
            var c = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "C" && x.NilaiMin <= bp.C && cp.C <= x.NilaiMax);
            var d = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "D" && x.NilaiMin <= dp.D && dp.D <= x.NilaiMax);
            var e = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "E" && x.NilaiMin <= ep.E && ep.E <= x.NilaiMax);
            var f = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "F" && x.NilaiMin <= fp.F && fp.F <= x.NilaiMax);
            var g = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "G" && x.NilaiMin <= gp.G && gp.G <= x.NilaiMax);
            var h = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "H" && x.NilaiMin <= hp.H && hp.H <= x.NilaiMax);
            var i = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "I" && x.NilaiMin <= ip.I && ip.I <= x.NilaiMax);

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


            return Pdf("Trait-Ipu", vm, x => x
                .Replace($"id=\"A{vm.SkorA}\"", $"id=\"A{vm.SkorA}\" class=\"tg-6wvf\"")
                .Replace($"id=\"B{vm.SkorB}\"", $"id=\"B{vm.SkorB}\" class=\"tg-6wvf\"")
                .Replace($"id=\"C{vm.SkorC}\"", $"id=\"C{vm.SkorC}\" class=\"tg-6wvf\"")
                .Replace($"id=\"D{vm.SkorD}\"", $"id=\"D{vm.SkorD}\" class=\"tg-6wvf\"")
                .Replace($"id=\"E{vm.SkorE}\"", $"id=\"E{vm.SkorE}\" class=\"tg-6wvf\"")
                .Replace($"id=\"F{vm.SkorF}\"", $"id=\"F{vm.SkorF}\" class=\"tg-6wvf\"")
                .Replace($"id=\"G{vm.SkorG}\"", $"id=\"G{vm.SkorG}\" class=\"tg-6wvf\"")
                .Replace($"id=\"H{vm.SkorH}\"", $"id=\"H{vm.SkorH}\" class=\"tg-6wvf\"")
                .Replace($"id=\"I{vm.SkorI}\"", $"id=\"I{vm.SkorI}\" class=\"tg-6wvf\"")
                .Replace($"id=\"J{vm.SkorJ}\"", $"id=\"J{vm.SkorJ}\" class=\"tg-6wvf\""));

        }


        [Route("indikator/ipu/{id}")]
        public async Task<ActionResult> PrintSesiUjianIndikatorIpu(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Bespoke.epsikologi_pengguna.Domain.Pengguna>(x => x.MyKad == sesi.MyKad);

            var ujianTask = context.LoadOneAsync<Bespoke.epsikologi_ujian.Domain.Ujian>(x => x.Id == sesi.NamaUjian);
            var permohonanTask = context.LoadOneAsync<Bespoke.epsikologi_permohonan.Domain.Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);
            await Task.WhenAll(ujianTask, permohonanTask);


            if (null == sesi)
                return HttpNotFound("Cannot find SesiUjian " + id);
            if (null == user)
                return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

            var vm = new IpuTraitViewModel(sesi)
            {
                Permohonan = await permohonanTask,
                Ujian = await ujianTask,
                Pengguna = user
            };

            /* */
            var apTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <= vm.A && vm.A <= x.NilaiMax);
            var bpTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <= vm.B && vm.B <= x.NilaiMax);
            var cpTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <= vm.C && vm.D <= x.NilaiMax);
            var dpTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <= vm.D && vm.D <= x.NilaiMax);
            var epTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <= vm.E && vm.E <= x.NilaiMax);
            var fpTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <= vm.F && vm.F <= x.NilaiMax);
            var gpTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <= vm.G && vm.G <= x.NilaiMax);
            var hpTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <= vm.H && vm.H <= x.NilaiMax);
            var ipTask = context.LoadOneAsync<SkorIPU>(x => x.NilaiMin <= vm.I && vm.I <= x.NilaiMax);
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

            var viewName = "Indikator-Ipu-" + user.Jantina;
            return Pdf(viewName, vm, x => x
                .Replace($"id=\"A{vm.SkorA}\"", $"id=\"A{vm.SkorA}\" class=\"tg-6wvf\"")
                .Replace($"id=\"B{vm.SkorB}\"", $"id=\"B{vm.SkorB}\" class=\"tg-6wvf\"")
                .Replace($"id=\"C{vm.SkorC}\"", $"id=\"C{vm.SkorC}\" class=\"tg-6wvf\"")
                .Replace($"id=\"D{vm.SkorD}\"", $"id=\"D{vm.SkorD}\" class=\"tg-6wvf\"")
                .Replace($"id=\"E{vm.SkorE}\"", $"id=\"E{vm.SkorE}\" class=\"tg-6wvf\"")
                .Replace($"id=\"F{vm.SkorF}\"", $"id=\"F{vm.SkorF}\" class=\"tg-6wvf\"")
                .Replace($"id=\"G{vm.SkorG}\"", $"id=\"G{vm.SkorG}\" class=\"tg-6wvf\"")
                .Replace($"id=\"H{vm.SkorH}\"", $"id=\"H{vm.SkorH}\" class=\"tg-6wvf\"")
                .Replace($"id=\"I{vm.SkorI}\"", $"id=\"I{vm.SkorI}\" class=\"tg-6wvf\"")
                .Replace($"id=\"J{vm.SkorJ}\"", $"id=\"J{vm.SkorJ}\" class=\"tg-6wvf\""));
        }


    }
}
