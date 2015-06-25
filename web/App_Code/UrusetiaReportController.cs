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
            var context = new SphDataContext();
            var no = string.Format("{0}/{1}/{2}/{3}", model.Program, model.Bil, model.Siri, model.Tahun);
            var permohonan = await context.LoadOneAsync<Bespoke.epsikologi_permohonan.Domain.Permohonan>(x => x.PermohonanNo == no);
            var ujian = await context.LoadOneAsync<Bespoke.epsikologi_ujian.Domain.Ujian>(x => x.UjianNo == model.Ujian || x.NamaUjian == model.Ujian);

            var query = context.CreateQueryable<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>()
                .Where(s => s.NamaProgram == no)
                .Where(s =>s.NamaUjian == model.Ujian)
                .Where(s => s.Status == "Diambil");
            var sesiLo = await context.LoadAsync(query, 1, 200, true);
            var sesi = sesiLo.ItemCollection;

            while(sesiLo.HasNextPage)
            {
                sesiLo = await context.LoadAsync(query, sesiLo.CurrentPage + 1, 200, true);
                sesi.AddRange(sesiLo.ItemCollection);
            }

            var soalanQuery = context.CreateQueryable<Bespoke.epsikologi_soalan.Domain.Soalan>()
                                .Where (s => s.NamaUjian == ujian.UjianNo || s.NamaUjian == ujian.NamaUjian);
            var soalanLo = await context.LoadAsync(soalanQuery, 1, 200, true);
            var soalans = soalanLo.ItemCollection;
            while(soalanLo.HasNextPage)
            {
                soalanLo = await context.LoadAsync(soalanQuery, soalanLo.CurrentPage + 1, 200, true);
                soalans.AddRange(soalanLo.ItemCollection);
            }

            var traits = soalans.Select(s => s.Trait).Distinct().OrderBy(s =>s).ToArray();

            var html = new StringBuilder();
            html.AppendLine("<table class=\"table table-striped table-bordered\">");
            html.AppendLine("   <thead>");
            html.AppendLine("       <tr>");
            html.AppendLine("           <th>Nama</th>");
            html.AppendLine("           <th>Tarikh</th>");
            foreach(var t in traits)
            {
                html.AppendLine("           <th>" + t + "</th>");
            }

            html.AppendLine("           <th>Cetakan Individu</th>");
            html.AppendLine("       </tr>");
            html.AppendLine("   </thead>");
            html.AppendLine("   <tbody>");
            foreach(var s in sesi)
            {
                html.AppendLine("   <tr>");
                html.AppendLine("   <td>" + s.NamaPengguna + "</td>");
                html.AppendFormat("   <td>{0:dd/MM/yyyy HH:mm}</td>", s.TarikhUjian); 
                foreach(var t in traits)
                {
                    var t1 = t;
                    var score = s.JawapanCollection.Where(a => a.Trait == t1).Sum(a => a.Nilai);
                    html.AppendLine("           <td>" + score + "</td>");
                }
                var indikator = s.NamaUjian.Contains("IBK") ? "" : @"<a class=""btn btn-info"" target=""_blank"" href=""cetak-laporan/indikator/{0}""> <i class=""fa fa-print""></i> Indikator</a>";

                html.AppendFormat(@"   
                    <td>
                        <a class=""btn btn-info"" target=""_blank"" href=""cetak-laporan/trait/{0}""> <i class=""fa fa-print""></i> Tret</a>
                        {1}
                    </td>", s.Id, indikator); 
                html.AppendLine("   </tr>");
            }


            // sum
            html.AppendLine("   <tr>");
            html.AppendLine("   <td>Jumlah Markah</td>");
            html.AppendLine("   <td></td>"); 
            foreach(var t in traits)
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
            foreach(var t in traits)
            {
                var t1 = t;
                if(sesi.Count > 0)
                {

                    var avg = sesi.SelectMany(x => x.JawapanCollection).Where(a => a.Trait == t1).Sum(a => a.Nilai)/sesi.Count;
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