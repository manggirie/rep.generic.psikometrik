using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_skorukbp.Domain;
using Bespoke.epsikologi_soalan.Domain;
using Bespoke.epsikologi_ujian.Domain;
using Bespoke.epsikologi_ukbprecommendation.Domain;
using Bespoke.Sph.Domain;
// ReSharper disable InconsistentNaming

namespace web.sph.App_Code
{
    public class UkbpTraitViewModel : LaporanViewModel
    {
        private readonly SesiUjian m_sesiA;
        private readonly SesiUjian m_sesiB;
        private readonly SkorUkbp[] m_scores;
        private readonly UkbpRecommendation[] m_recommendations;
        public const string KEBOLEHPERCAYAAN = "Kebolehpercayaan";
        public const string NASIONALISME = "Nasionalisme";
        public const string KETENANGAN = "Ketenangan";
        public const string KECERIAAN = "Keceriaan";
        public const string LUAHAN_PERASAAN = "Luahan Perasaan";
        public const string BERSIMPATI = "Bersimpati";
        public const string BERFIKIRAN_RASIONAL = "Berfikiran Rasional";

        public const string BERSIKAP_ASERTIF = "Bersikap Asertif";
        public const string BERTOLERANSI = "Bertoleransi";
        public const string PENYIMPANGAN_TINGKAH_LAKU = "Penyimpangan Tingkah Laku";
        public const string AKTIF_SOSIAL = "Aktif Sosial";

        public UkbpTraitViewModel(SesiUjian sesiA, SesiUjian sesiB, SkorUkbp[] scores, UkbpRecommendation[] recommendations)
        {
            m_sesiA = sesiA;
            m_sesiB = sesiB;
            m_scores = scores;
            m_recommendations = recommendations;
            this.Sesi = sesiA;


            this.AddDarkToLight(1, 6, 9, 12, 15, 1, KEBOLEHPERCAYAAN);
            this.AddLightToDark(1, 8, 13, 17, 20, 1, NASIONALISME);
            this.AddDarkToLight(5, 30, 55, 90, 100, 5, KETENANGAN);
            this.AddDarkToLight(5, 25, 50, 85, 100, 5, KECERIAAN);
            this.AddLightToDark(5, 25, 55, 80, 100, 5, LUAHAN_PERASAAN);
            this.AddLightToDark(5, 25, 55, 80, 100, 5, BERSIMPATI);
            this.AddDarkToLight(5, 40, 65, 85, 100, 5, BERFIKIRAN_RASIONAL);

            this.AddLightToDark(5, 20, 40, 70, 100, 5, AKTIF_SOSIAL);
            this.AddLightToDark(5, 20, 40, 60, 100, 5, BERSIKAP_ASERTIF);
            this.AddDarkToLight(5, 30, 55, 80, 100, 5, BERTOLERANSI);
            this.AddLightToDark(5, 50, 60, 70, 100, 5, PENYIMPANGAN_TINGKAH_LAKU);


        }


        public UkbpRecommendation RecommendationKBY => m_recommendations.Single(x => x.Tret == KEBOLEHPERCAYAAN && x.Skor == this.KBY.Skor);
        public UkbpRecommendation RecommendationASF => m_recommendations.Single(x => x.Tret == BERSIKAP_ASERTIF && x.Skor == this.ASF.Skor);
        public UkbpRecommendation RecommendationNAS => m_recommendations.Single(x => x.Tret == NASIONALISME && x.Skor == this.NAS.Skor);
        public UkbpRecommendation RecommendationAKS => m_recommendations.Single(x => x.Tret == AKTIF_SOSIAL && x.Skor == this.AKS.Skor);
        public UkbpRecommendation RecommendationPTL => m_recommendations.Single(x => x.Tret == PENYIMPANGAN_TINGKAH_LAKU && x.Skor == this.PTL.Skor);
        public UkbpRecommendation RecommendationRAS => m_recommendations.Single(x => x.Tret == BERFIKIRAN_RASIONAL && x.Skor == this.RAS.Skor);
        public UkbpRecommendation RecommendationSIM => m_recommendations.Single(x => x.Tret == BERSIMPATI && x.Skor == this.SIM.Skor);
        public UkbpRecommendation RecommendationKCN => m_recommendations.Single(x => x.Tret == KECERIAAN && x.Skor == this.KCN.Skor);
        public UkbpRecommendation RecommendationKTG => m_recommendations.Single(x => x.Tret == KETENANGAN && x.Skor == this.KTG.Skor);
        public UkbpRecommendation RecommendationTOL => m_recommendations.Single(x => x.Tret == BERTOLERANSI && x.Skor == this.TOL.Skor);
        public UkbpRecommendation RecommendationLPN => m_recommendations.Single(x => x.Tret == LUAHAN_PERASAAN && x.Skor == this.LPN.Skor);


        public SkorUkbp NAS
        {
            get
            {
                var val = m_sesiA.JawapanCollection.Where(x => x.Trait == "NAS").Sum(x => x.Nilai);
                if (val == 0)
                    val = 1;
                return m_scores.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "NE");
            }
        }

        public SkorUkbp PTL
        {
            get
            {

                var ptl1 = m_sesiA.JawapanCollection.Where(x => x.Trait == "PTL1").Sum(x => x.Nilai);
                var ptl2 = m_sesiA.JawapanCollection.Where(x => x.Trait == "PTL2").Sum(x => x.Nilai);
                var ptl3 = m_sesiA.JawapanCollection.Where(x => x.Trait == "PTL3").Sum(x => x.Nilai);
                var val = ptl1 + ptl2 + ptl3;
                Console.WriteLine(val);
                return m_scores.OrderBy(x => Guid.NewGuid()).First(x => x.Tret == "PTL");
                // return m_scores.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "PTL");
            }
        }

        public SkorUkbp LPN
        {
            get
            {
                var val = m_sesiA.JawapanCollection.Where(x => x.Trait == "LPN").Sum(x => x.Nilai);
                return m_scores.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "LP" && x.Jantina == this.Pengguna.Jantina);
            }
        }
        public SkorUkbp KCN
        {
            get
            {
                var val = m_sesiA.JawapanCollection.Where(x => x.Trait == "KCN").Sum(x => x.Nilai);
                return m_scores.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "KC" && x.Jantina == this.Pengguna.Jantina);
            }
        }
        public SkorUkbp TOL
        {
            get
            {
                var val = m_sesiA.JawapanCollection.Where(x => x.Trait == "TOL").Sum(x => x.Nilai);
                return m_scores.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "TL" && x.Jantina == this.Pengguna.Jantina);
            }
        }

        public SkorUkbp RAS
        {
            get
            {
                var val = m_sesiA.JawapanCollection.Where(x => x.Trait == "RAS").Sum(x => x.Nilai);
                return m_scores.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "BR" && x.Jantina == this.Pengguna.Jantina);
            }
        }


        public SkorUkbp AKS
        {
            get
            {
                var val = m_sesiA.JawapanCollection.Where(x => x.Trait == "AKS").Sum(x => x.Nilai);
                return m_scores.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "AS" && x.Jantina == this.Pengguna.Jantina);
            }
        }

        public SkorUkbp KBY
        {
            get
            {
                var val = m_sesiA.JawapanCollection.Where(x => x.Trait == "KBY").Sum(x => x.Nilai);
                if (val == 0)
                    val = 1;
                return m_scores.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "KBY" && x.Jantina == "NA");
            }
        }

        public SkorUkbp KTG
        {
            get
            {
                var ktg = m_sesiA.JawapanCollection.Where(x => x.Trait == "KTG").Sum(x => x.Nilai);
                return m_scores.Single(x => ktg.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "KT" && x.Jantina == this.Pengguna.Jantina);
            }
        }

        public SkorUkbp SIM
        {
            get
            {
                var val = m_sesiA.JawapanCollection.Where(x => x.Trait == "SIM").Sum(x => x.Nilai);
                return m_scores.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "BS" && x.Jantina == this.Pengguna.Jantina);
            }
        }

        public SkorUkbp ASF
        {
            get
            {
                var val = m_sesiA.JawapanCollection.Where(x => x.Trait == "ASF").Sum(x => x.Nilai);
                return m_scores.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "AF" && x.Jantina == this.Pengguna.Jantina);
            }
        }
        class CategoryScore
        {
            public CategoryScore(string cat, int val)
            {
                this.Name = cat;
                this.Value = val;
            }
            public string Name { get; private set; }
            public int Value { get; private set; }
        }

        public class Kod1Type
        {
            public SesiUjian Sesi { get; }
            public Kod1Type(SesiUjian sesi)
            {
                this.Sesi = sesi;
            }
            public string KodKerjaya1
            {
                get
                {
                    var results = new List<CategoryScore>
              {
                  new CategoryScore("P", P),
                  new CategoryScore("E", E),
                  new CategoryScore("K", K),
                  new CategoryScore("M", M),
                  new CategoryScore("I", I),
                  new CategoryScore("T", T)
              };

                    var temp = results.OrderByDescending(s => s.Value).Select(s => s.Name).Take(3);
                    return string.Join("", temp);
                }
            }

            public string KodKerjaya2
            {
                get
                {
                    char[] array = this.KodKerjaya1.ToCharArray();
                    char temp = array[1];
                    array[1] = array[2];
                    array[2] = temp;
                    return new string(array);
                }
            }

            public string KodKerjaya => this.KodKerjaya1 + "/" + this.KodKerjaya2;


            public int P
            {
                get
                {
                    return
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "A").Sum(a => a.Nilai);
                }
            }
            public int E
            {
                get
                {
                    return
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "B").Sum(a => a.Nilai);
                }
            }


            public int K
            {
                get
                {
                    return
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "C").Sum(a => a.Nilai);
                }
            }

            public int M
            {
                get
                {
                    return
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "D").Sum(a => a.Nilai);
                }
            }

            public int I
            {
                get
                {
                    return
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "E").Sum(a => a.Nilai);
                }
            }

            public int T
            {
                get
                {
                    return
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "F").Sum(a => a.Nilai);
                }
            }

        }

        public class Kod2Type
        {
            public SesiUjian Sesi { get; }
            public Kod2Type(SesiUjian sesi)
            {
                this.Sesi = sesi;
            }
            public string KodKerjaya1
            {
                get
                {
                    var results = new List<CategoryScore>
              {
                  new CategoryScore("P", P),
                  new CategoryScore("E", E),
                  new CategoryScore("K", K),
                  new CategoryScore("M", M),
                  new CategoryScore("I", I),
                  new CategoryScore("T", T)
              };

                    var temp = results.OrderByDescending(s => s.Value).Select(s => s.Name).Take(3);
                    return string.Join("", temp);
                }
            }

            public string KodKerjaya2
            {
                get
                {
                    char[] array = this.KodKerjaya1.ToCharArray();
                    char temp = array[1];
                    array[1] = array[2];
                    array[2] = temp;
                    return new string(array);
                }
            }

            public string KodKerjaya => this.KodKerjaya1 + "/" + this.KodKerjaya2;


            public int P
            {
                get
                {
                    return
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "A1").Sum(a => a.Nilai) +
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "A2").Sum(a => a.Nilai) +
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "A3").Sum(a => a.Nilai);
                }
            }
            public int E
            {
                get
                {
                    return
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "B1").Sum(a => a.Nilai) +
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "B2").Sum(a => a.Nilai) +
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "B3").Sum(a => a.Nilai);
                }
            }


            public int K
            {
                get
                {
                    return
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "C1").Sum(a => a.Nilai) +
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "C2").Sum(a => a.Nilai) +
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "C3").Sum(a => a.Nilai);
                }
            }

            public int M
            {
                get
                {
                    return
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "D1").Sum(a => a.Nilai) +
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "D2").Sum(a => a.Nilai) +
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "D3").Sum(a => a.Nilai);
                }
            }

            public int I
            {
                get
                {
                    return
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "E1").Sum(a => a.Nilai) +
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "E2").Sum(a => a.Nilai) +
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "E3").Sum(a => a.Nilai);
                }
            }

            public int T
            {
                get
                {
                    return
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "F1").Sum(a => a.Nilai) +
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "F2").Sum(a => a.Nilai) +
                      this.Sesi.JawapanCollection.Where(a => a.Trait == "F3").Sum(a => a.Nilai);
                }
            }

        }

        public Kod1Type KodMinat1 => new Kod1Type(m_sesiB);
        public Kod2Type KodMinat2 => new Kod2Type(this.m_sesiB);


        public static async Task<string> GenerateLaporanTable(ProgramReportModel model)
        {
            var context = new SphDataContext();
            var no = $"{model.Program}/{model.Bil}/{model.Siri}/{model.Tahun}";
            var ujian = await context.LoadOneAsync<Ujian>(x => x.Id == "UKBP-A");

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
                var t1 = t;
                if (t1 == "KBY" || t1 == "NAS" || t1 == "PTL1" || t1 == "PTL2" || t1 == "PTL3")
                {
                    continue;
                }
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

                var responden = await context.LoadOneAsync<Pengguna>(x => x.MyKad == s.MyKad);

                foreach (var t in traits)
                {
                    var t1 = t;
                    var t2 = t;
                    switch (t1)
                    {
                        case "AKS": t2 = "AS"; break;
                        case "ASF": t2 = "AF"; break;
                        case "KBY": t2 = "KBY"; break;
                        case "KCN": t2 = "KC"; break;
                        case "KTG": t2 = "KT"; break;
                        case "LPN": t2 = "LP"; break;
                        case "NAS": t2 = "NE"; break;
                        case "RAS": t2 = "BR"; break;
                        case "SIM": t2 = "BS"; break;
                        case "TOL": t2 = "TL"; break;
                    }
                    var score = s.JawapanCollection.Where(a => a.Trait == t1).Sum(a => a.Nilai);
                    html.AppendLine("           <td>" + score + "</td>");
                    if (t1 == "KBY" || t1 == "NAS" || t1 == "PTL1" || t1 == "PTL2" || t1 == "PTL3")
                    {
                        continue;
                    }

                    var lookup = await context.LoadOneAsync<SkorUkbp>(x => score.IsBetween(x.NilaiMin, x.NilaiMax, true, true) && x.Tret == t2 && x.Jantina == responden.Jantina);
                    html.AppendLine(null == lookup
                        ? $"           <td>{score} : {t1} : {responden.Jantina}</td>"
                        : $"           <td>{lookup.Percentile}</td>");

                }

                var button = $@"
                    <td>
                        <a class=""trait-report btn btn-info"" target=""_blank"" href=""cetak-laporan/indikator/ukbp/{s.Id}""> <i class=""fa fa-print""></i> Indikator</a>
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