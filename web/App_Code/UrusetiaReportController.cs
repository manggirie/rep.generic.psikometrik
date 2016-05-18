using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_soalan.Domain;
using Bespoke.epsikologi_ujian.Domain;
using Bespoke.Sph.Domain;

namespace web.sph.App_Code
{
    [Authorize(Roles = "JanaLaporan")]
    [RoutePrefix("urusetia-report")]
    public class UrusetiaReportController : Controller
    {
        public UrusetiaReportController()
        {
            ConfigHelper.RegisterDependencies();
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Program(ProgramReportModel model)
        {
            if (model.Ujian == "IPU")
            {
                var html2 = await IpuTraitViewModel.GenerateLaporanTable(model);
                return Content(html2, "text/html", Encoding.UTF8);
            }
            if (model.Ujian.Contains("UKBP"))
            {
                var html2 = await UkbpTraitViewModel.GenerateLaporanTable(model);
                return Content(html2, "text/html", Encoding.UTF8);
            }

            var context = new SphDataContext();
            var no = $"{model.Program}/{model.Bil}/{model.Siri}/{model.Tahun}";
            var ujian = await context.LoadOneAsync<Ujian>(x => x.UjianNo == model.Ujian || x.NamaUjian == model.Ujian);

            var query = context.CreateQueryable<SesiUjian>()
                .Where(s => s.NamaProgram == model.Program)
                .Where(s => s.NamaUjian == model.Ujian)
                .Where(s => s.Status == "Diambil");
            var sesiLo = await context.LoadAsync(query, 1, 200, true);
            var sesi = sesiLo.ItemCollection;

            while (sesiLo.HasNextPage)
            {
                sesiLo = await context.LoadAsync(query, sesiLo.CurrentPage + 1, 200, true);
                sesi.AddRange(sesiLo.ItemCollection);
            }

            var soalanQuery = context.CreateQueryable<Soalan>()
                                .Where(s => s.NamaUjian == ujian.UjianNo || s.NamaUjian == ujian.NamaUjian);
            var soalanLo = await context.LoadAsync(soalanQuery, 1, 200, true);
            var soalans = soalanLo.ItemCollection;
            while (soalanLo.HasNextPage)
            {
                soalanLo = await context.LoadAsync(soalanQuery, soalanLo.CurrentPage + 1, 200, true);
                soalans.AddRange(soalanLo.ItemCollection);
            }

            var traits = soalans.Select(s => s.Trait).Distinct().OrderBy(s => s).ToArray();

            var html = new StringBuilder();

            html.AppendLine("<table class=\"table table-striped table-bordered\">");
            html.AppendLine("   <thead>");
            html.AppendLine("       <tr>");
            html.AppendLine("           <th>Nama</th>");
            html.AppendLine("           <th>Tarikh</th>");
            foreach (var t in traits)
            {
                html.AppendLine("           <th>" + t + "</th>");
            }

            html.AppendLine("           <th>Cetakan Individu</th>");
            html.AppendLine("       </tr>");
            html.AppendLine("   </thead>");
            html.AppendLine("   <tbody>");
            foreach (var s in sesi)
            {
                html.AppendLine("   <tr>");
                html.AppendLine("   <td>" + s.NamaPengguna + "</td>");
                html.AppendFormat("   <td>{0:dd/MM/yyyy}</td>", s.TarikhUjian);
                foreach (var t in traits)
                {
                    var t1 = t;
                    var score = s.JawapanCollection.Where(a => a.Trait == t1).Sum(a => a.Nilai);
                    html.AppendLine("           <td>" + score + "</td>");
                }
                var ip = s.NamaUjian.Contains("IP") && !s.NamaUjian.Contains("IPU");
                var ibk = s.NamaUjian.Contains("IBK");
                var iso = s.NamaUjian.Contains("ISO");
                var hlp = s.NamaUjian.Contains("HLP");
                var ppkp = s.NamaUjian.Contains("PPKP");
                var ukbp = s.NamaUjian.Contains("UKBP");
                var indikator = ibk || ip || hlp || iso ? "" :
                    $@"<a class=""indikator-report btn btn-info"" target=""_blank"" href=""cetak-laporan/indikator/{
                        s.NamaUjian}/{s.Id}""> <i class=""fa fa-print""></i> Indikator</a>";
                if (ppkp)
                {

                    indikator = $@"<a class=""laporan-profile-report btn btn-info"" target=""_blank"" 
                    href=""cetak-laporan/ppkp/profile/{s.Id}""> <i class=""fa fa-print""></i> Profil</a>

<a class=""laporan-umum-report btn btn-info"" target=""_blank"" 
                    href=""cetak-laporan/ppkp/umum/{s.Id}""> <i class=""fa fa-print""></i> Umum</a>

<a class=""laporan-khusus-report btn btn-info"" target=""_blank"" 
                    href=""cetak-laporan/ppkp/khusus/{s.Id}""> <i class=""fa fa-print""></i> Khusus</a>"
                    ;

                    html.AppendLine($@"
                    <td>
                         {indikator}
                    </td>");
                }
                else if (ukbp)
                {
                    indikator = $@"<a class=""btn btn-info"" target=""_blank"" 
                                href=""cetak-laporan/indikator/ukbp/{s.Id}""> <i class=""fa fa-print""></i>Indikator</a>";

                    html.AppendLine($@"
                    <td>
                         {indikator}
                    </td>");

                }
                else
                {

                    html.AppendFormat(@"
                    <td>
                        <a class=""trait-report btn btn-info"" target=""_blank"" href=""cetak-laporan/trait/{2}/{0}""> <i class=""fa fa-print""></i> Tret</a>
                        {1}
                    </td>", s.Id, indikator, s.NamaUjian);
                }


                html.AppendLine("   </tr>");
            }


            // sum
            html.AppendLine("   <tr>");
            html.AppendLine("   <td>Jumlah Markah</td>");
            html.AppendLine("   <td></td>");
            foreach (var t in traits)
            {
                var t1 = t;
                var score = sesi.SelectMany(x => x.JawapanCollection).Where(a => a.Trait == t1).Sum(a => a.Nilai);
                html.AppendLine("           <td>" + score + "</td>");
            }
            html.AppendLine("   <td></td>");
            html.AppendLine("   </tr>");

            // average
            html.AppendLine("   <tr>");
            html.AppendLine("   <td>Purata Markah</td>");
            html.AppendLine("   <td></td>");
            foreach (var t in traits)
            {
                var t1 = t;
                if (sesi.Count > 0)
                {

                    var avg = sesi.SelectMany(x => x.JawapanCollection).Where(a => a.Trait == t1).Sum(a => a.Nilai) / sesi.Count;
                    html.AppendLine("           <td>" + avg + "</td>");
                }
                else
                {
                    html.AppendLine("           <td> NA</td>");
                }
            }
            html.AppendLine("   <td></td>");
            html.AppendLine("   </tr>");

            html.AppendLine("</tbody>");
            html.AppendLine("</table>");

            return Content(html.ToString(), "text/html", Encoding.UTF8);
        }


    }

}
