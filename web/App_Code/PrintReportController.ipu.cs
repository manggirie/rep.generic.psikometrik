using System.IO;
using System.Linq;
using System.Web.Mvc;
using Bespoke.Sph.Domain;
using System.Threading.Tasks;
using System.Web;
using Bespoke.epsikologi_ipurecommendation.Domain;
using Bespoke.epsikologi_skoripu.Domain;
using Bespoke.epsikologi_ipupercentilenorms.Domain;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_permohonan.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_soalan.Domain;
using Bespoke.epsikologi_ujian.Domain;
using OfficeOpenXml;

namespace web.sph.App_Code
{
    public partial class PrintReportController
    {

        [Route("trait/ipu/{id}")]
        public async Task<ActionResult> PrintSesiUjianTraitIpu(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Pengguna>(x => x.MyKad == sesi.MyKad);

            var ujianTask = context.LoadOneAsync<Ujian>(x => x.Id == sesi.NamaUjian);
            var permohonanTask = context.LoadOneAsync<Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);
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
            var c = context.LoadOneAsync<IpuRecommendation>(x => x.Tret == "C" && x.NilaiMin <= cp.C && cp.C <= x.NilaiMax);
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


            return View("Trait-Ipu", vm);
        }


        [Route("indikator/ipu/{id}")]
        public async Task<ActionResult> PrintSesiUjianIndikatorIpu(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Pengguna>(x => x.MyKad == sesi.MyKad);

            var ujianTask = context.LoadOneAsync<Ujian>(x => x.Id == sesi.NamaUjian);
            var permohonanTask = context.LoadOneAsync<Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);
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
            var apTask = context.LoadOneAsync<SkorIPU>(x => vm.A.IsBetween(x.NilaiMin, x.NilaiMax, true, true) && x.Jantina == user.Jantina && x.Tret == "A");
            var bpTask = context.LoadOneAsync<SkorIPU>(x => vm.B.IsBetween(x.NilaiMin, x.NilaiMax, true, true) && x.Jantina == user.Jantina && x.Tret == "B");
            var cpTask = context.LoadOneAsync<SkorIPU>(x => vm.C.IsBetween(x.NilaiMin, x.NilaiMax, true, true) && x.Jantina == user.Jantina && x.Tret == "C");
            var dpTask = context.LoadOneAsync<SkorIPU>(x => vm.D.IsBetween(x.NilaiMin, x.NilaiMax, true, true) && x.Jantina == user.Jantina && x.Tret == "D");
            var epTask = context.LoadOneAsync<SkorIPU>(x => vm.E.IsBetween(x.NilaiMin, x.NilaiMax, true, true) && x.Jantina == user.Jantina && x.Tret == "E");
            var fpTask = context.LoadOneAsync<SkorIPU>(x => vm.F.IsBetween(x.NilaiMin, x.NilaiMax, true, true) && x.Jantina == user.Jantina && x.Tret == "F");
            var gpTask = context.LoadOneAsync<SkorIPU>(x => vm.G.IsBetween(x.NilaiMin, x.NilaiMax, true, true) && x.Jantina == user.Jantina && x.Tret == "G");
            var hpTask = context.LoadOneAsync<SkorIPU>(x => vm.H.IsBetween(x.NilaiMin, x.NilaiMax, true, true) && x.Jantina == user.Jantina && x.Tret == "H");
            var ipTask = context.LoadOneAsync<SkorIPU>(x => vm.I.IsBetween(x.NilaiMin, x.NilaiMax, true, true) && x.Jantina == user.Jantina && x.Tret == "I");
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
            return View(viewName, vm);
        }

        [Route("xls/ipu/{id}")]
        public async Task<ActionResult> ExportIpuToExcel(string id)
        {

            var temp = Path.GetTempFileName() + ".xlsx";
            System.IO.File.Copy(Server.MapPath("~/App_Data/template/laporan-ipu.xlsx"), temp, true);


            var file = new FileInfo(temp);
            var excel = new ExcelPackage(file);
            var ws = excel.Workbook.Worksheets["IPU"];

            var context = new SphDataContext();
            var program = await context.GetScalarAsync<SesiUjian, string>(x => x.Id == id, x => x.NamaProgram);

            var query = context.CreateQueryable<SesiUjian>()
                .Where(s => s.NamaProgram == program)
                .Where(s => s.NamaUjian == "IPU")
                .Where(s => s.Status == "Diambil");
            var sesiLo = await context.LoadAsync(query, 1, 200, true);
            var sesi = sesiLo.ItemCollection;

            while (sesiLo.HasNextPage)
            {
                sesiLo = await context.LoadAsync(query, sesiLo.CurrentPage + 1, 200, true);
                sesi.AddRange(sesiLo.ItemCollection);
            }

            var soalanQuery = context.CreateQueryable<Soalan>()
                                .Where(s => s.NamaUjian == "IPU");
            var soalanLo = await context.LoadAsync(soalanQuery, 1, 200, true);
            var soalans = soalanLo.ItemCollection;
            while (soalanLo.HasNextPage)
            {
                soalanLo = await context.LoadAsync(soalanQuery, soalanLo.CurrentPage + 1, 200, true);
                soalans.AddRange(soalanLo.ItemCollection);
            }

            var traits = soalans.Select(s => s.Trait).Distinct().OrderBy(s => s).ToArray();

            var row = 2;
            foreach (var s in sesi)
            {
                var user = await context.LoadOneAsync<Pengguna>(x => x.MyKad == s.MyKad);
                row++;
                ws.InsertRow(row, 1, row);
                ws.Cells[row, 1].Value = s.NamaPengguna;
                ws.Cells[row, 2].Value = $"{s.TarikhUjian:dd/MM/yyyy HH:mm}";
                var column = 1;
                foreach (var t in traits)
                {
                    column += 2;
                    var t1 = t;
                    var score = s.JawapanCollection.Where(a => a.Trait == t1).Sum(a => a.Nilai);
                    ws.Cells[row, column].Value = score;
                    if (t1 == "J")
                    {
                        ws.Cells[row, column + 1].Value = score;
                    }
                    else
                    {
                        /*var lookup = await context.LoadOneAsync<SkorIPU>(x => score.IsBetween(x.NilaiMin, x.NilaiMax, true, true));*/
                         var lookup = await context.LoadOneAsync<SkorIPU>(x => score.IsBetween(x.NilaiMin, x.NilaiMax, true, true) && x.Jantina == user.Jantina && x.Tret == t1);
                        ws.Cells[row, column + 1].Value = lookup.Percentile;
                    }
                }

            }


            excel.Save();
            excel.Dispose();

            return File(temp, MimeMapping.GetMimeMapping(".xlsx"), "Laporan Markah dan Percentile IPU.xlsx");
        }


    }
}
