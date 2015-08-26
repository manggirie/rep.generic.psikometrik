using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bespoke.epsikologi_ipurecommendation.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_skoripu.Domain;
using Bespoke.epsikologi_soalan.Domain;
using Bespoke.epsikologi_ujian.Domain;
using Bespoke.Sph.Domain;
using Newtonsoft.Json;

namespace web.sph.App_Code
{
    public class IpuTraitViewModel : LaporanViewModel
    {

        public IpuTraitViewModel(SesiUjian sesi)
        {
            Sesi = sesi;
        }


        [JsonIgnore]
        public IpuRecommendation RecommendationA { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationB { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationC { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationD { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationE { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationF { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationG { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationH { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationI { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationJ { get; set; }

        public int? SkorA { get; set; }
        public int? SkorB { get; set; }
        public int? SkorC { get; set; }
        public int? SkorD { get; set; }
        public int? SkorE { get; set; }
        public int? SkorF { get; set; }
        public int? SkorG { get; set; }
        public int? SkorH { get; set; }
        public int? SkorI { get; set; }
        public int? SkorJ { get; set; }

        public int A
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "A").Sum(a => a.Nilai);
            }
        }

        public int B
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "B").Sum(a => a.Nilai);
            }
        }

        public int C
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "C").Sum(a => a.Nilai);
            }
        }
        public int D
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "D").Sum(a => a.Nilai);
            }
        }
        public int E
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "E").Sum(a => a.Nilai);
            }
        }
        public int F
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "F").Sum(a => a.Nilai);
            }
        }
        public int G
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "G").Sum(a => a.Nilai);
            }
        }
        public int H
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "H").Sum(a => a.Nilai);
            }
        }
        public int I
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "I").Sum(a => a.Nilai);
            }
        }
        public int J
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "J").Sum(a => a.Nilai);
            }
        }


        public static async Task<string> GenerateLaporanTable(ProgramReportModel model)
        {
            var context = new SphDataContext();
            var no = $"{model.Program}/{model.Bil}/{model.Siri}/{model.Tahun}";
            var ujian = await context.LoadOneAsync<Ujian>(x => x.Id == "IPU");

            var query = context.CreateQueryable<SesiUjian>()
                .Where(s => s.NamaProgram == no)
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
                html.AppendLine("           <th>%</th>");
            }

            html.AppendLine("           <th>Cetakan Individu</th>");
            html.AppendLine("       </tr>");
            html.AppendLine("   </thead>");
            html.AppendLine("   <tbody>");
            foreach (var s in sesi)
            {
                html.AppendLine("   <tr>");
                html.AppendLine("   <td>" + s.NamaPengguna + "</td>");
                html.AppendFormat("   <td>{0:dd/MM/yyyy HH:mm}</td>", s.TarikhUjian);
                foreach (var t in traits)
                {
                    var t1 = t;
                    var score = s.JawapanCollection.Where(a => a.Trait == t1).Sum(a => a.Nilai);
                    html.AppendLine("           <td>" + score + "</td>");
                    if (t1 == "J")
                    {
                        html.AppendLine($"           <td>{score}</td>");
                    }
                    else
                    {
                        var lookup = await context.LoadOneAsync<SkorIPU>(x => score.IsBetween(x.NilaiMin, x.NilaiMax, true, true));
                        html.AppendLine($"           <td>{lookup.Percentile}</td>");
                    }
                }

                var button = $@"
                    <td>
                        <a class=""trait-report btn btn-info"" target=""_blank"" href=""cetak-laporan/trait/ipu/{s.Id}""> <i class=""fa fa-print""></i> Tret</a>
                        <a class=""indikator-report btn btn-info"" target=""_blank"" href=""cetak-laporan/indikator/ipu/{s.Id}""> <i class=""fa fa-table""></i> Indikator</a>
                        <a class=""excel-report btn btn-info"" download href=""cetak-laporan/xls/ipu/{s.Id}""> <i class=""fa fa-file-excel-o""></i> Simpan Markah</a>
                    </td>";
                html.AppendLine(button);


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
                html.AppendLine("           <td colspan=\"2\">" + score + "</td>");
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
                    html.AppendLine("           <td colspan=\"2\">" + avg + "</td>");
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

            html.AppendLine("<div><em>Nota: % = Percentile</em></di>");

            return html.ToString();
        }
    }
}
