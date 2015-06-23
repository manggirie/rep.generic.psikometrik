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
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Program(ProgramReportModel model)
        {
            await RegisterCustomEntityDependencies();
            var context = new SphDataContext();
            var no = string.Format("{0}/{1}/{2}/{3}", model.Program, model.Bil, model.Siri, model.Tahun);
            var permohonan = await context.LoadOneAsync<Bespoke.epsikologi_permohonan.Domain.Permohonan>(x => x.PermohonanNo == no);
            var ujian = await context.LoadOneAsync<Bespoke.epsikologi_ujian.Domain.Ujian>(x => x.UjianNo == model.Ujian || x.NamaUjian == model.Ujian);

            var query = context.CreateQueryable<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>()
                .Where(s => s.NamaProgram == no)
                .Where(s =>s.NamaUjian == model.Ujian);
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
            html.AppendLine("<table class=\"table table-striped\">");
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
                html.AppendLine("   <td>" + s.TarikhUjian + "</td>"); 
                foreach(var t in traits)
                {
                    var t1 = t;
                    var score = s.JawapanCollection.Where(a => a.Trait == t1).Sum(a => a.Nilai);
                    html.AppendLine("           <td>" + score + "</td>");
                }
                html.AppendFormat(@"   
                    <td>
                        <a class=""btn btn-default"" target=""_blank"" href=""print/laporan-sesi-ujian/{0}""> <i class=""fa fa-print""></i> Tret</a>
                        <a class=""btn btn-default"" target=""_blank"" href=""print/indikator/{0}""> <i class=""fa fa-print""></i> Indikator</a>
                    </td>", s.Id); 
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
                var avg = sesi.SelectMany(x => x.JawapanCollection).Where(a => a.Trait == t1).Sum(a => a.Nilai)/sesi.Count;
                html.AppendLine("           <td>" + avg + "</td>");
            }
            html.AppendLine("   <td></td>"); 
            html.AppendLine("   </tr>");

            html.AppendLine("</tbody>");
            html.AppendLine("</table>");

            return Content(html.ToString(), "text/html", Encoding.UTF8);
        }



        public async Task RegisterCustomEntityDependencies()
        {
            var sqlAssembly = Assembly.Load("sql.repository");
            var sqlRepositoryType = sqlAssembly.GetType("Bespoke.Sph.SqlRepository.SqlRepository`1");

            var context = new SphDataContext();
            var query = context.EntityDefinitions.Where(e => e.IsPublished == true);
            var lo = await context.LoadAsync(query, includeTotalRows: true);
            var entityDefinitions = new ObjectCollection<EntityDefinition>(lo.ItemCollection);
            while (lo.HasNextPage)
            {
                lo = await context.LoadAsync(query, includeTotalRows: true, page: lo.CurrentPage + 1);
                entityDefinitions.AddRange(lo.ItemCollection);
            }

            var bags = new ConcurrentDictionary<Type, object>();

            Parallel.ForEach(entityDefinitions, (ed, count) =>
            {
                var ed1 = ed;
                try
                {
                    var edAssembly = Assembly.Load(ConfigurationManager.ApplicationName + "." + ed1.Name);
                    var edTypeName = string.Format("Bespoke.{0}_{1}.Domain.{2}", ConfigurationManager.ApplicationName,ed1.Id, ed1.Name);
                    var edType = edAssembly.GetType(edTypeName);
                    if (null == edType)
                        Console.WriteLine("Cannot create type " + edTypeName);

                    var reposType = sqlRepositoryType.MakeGenericType(edType);
                    var repository = Activator.CreateInstance(reposType);

                    var ff = typeof(IRepository<>).MakeGenericType(edType);
                    bags.AddOrReplace(ff, repository);
                }
                catch (FileNotFoundException e)
                {
                    Debug.WriteLine(e);
                }
            });
            foreach (var type in bags.Keys)
            {
                ObjectBuilder.AddCacheList(type, bags[type]);
            }

        }

    }

    public class ProgramReportModel
    {
        public int Siri {get;set;}
        public int Tahun {get;set;}
        public int Bil {get;set;}
        public string Ujian {get;set;}
        public string Program {get; set;}
    }
}