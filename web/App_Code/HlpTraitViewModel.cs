using System;
using System.Linq;
using Bespoke.epsikologi_hlprecomendation.Domain;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_skorhlp.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
// ReSharper disable InconsistentNaming

namespace web.sph.App_Code
{
    public class HlpResult
    {
        public string Skor { get; set; }
        public int? Percentile { get; set; }
        public string Tret { get; set; }
        public string Recommendation { get; set; }
        public int Point { get; set; }
    }

    public class HlpTraitViewModel : LaporanViewModel
    {
        public const string KEBOLEHPERCAYAAN = "Kebolehpercayaan";
        public const string NASIONALISME = "Nasionalisme";
        public const string KETENANGAN = "Ketenangan";
        public const string KECERIAAN = "Keceriaan";
        public const string LUAHAN_PERASAAN = "Luahan Perasaan";
        public const string BERSIMPATI = "Bersimpati";
        public const string BERFIKIRAN_RASIONAL = "Berfikiran Rasional";

        public const string BERSIKAP_ASERTIF = "Bersikap Asertif";
        public const string BERTOLERANSI = "Bertoleransi";
        public const string DAYA_TAHAN = "Daya Tahan";
        public const string AKTIF_SOSIAL = "Aktif Sosial";

        private readonly SesiUjian m_sesi;
        private readonly Pengguna m_pengguna;
        private readonly SkorHlp[] m_scoreTables;
        private readonly HlpRecomendation[] m_recommendations;

        public HlpTraitViewModel(SesiUjian sesi,
          Pengguna pengguna, SkorHlp[] scoreTables,
          HlpRecomendation[] recommendations)
        {
            m_sesi = sesi;
            m_pengguna = pengguna;
            m_scoreTables = scoreTables;
            m_recommendations = recommendations;
            this.Pengguna = pengguna;
            this.Sesi = sesi;


            if (pengguna.Jantina == "Lelaki")
            {
                this.AddDarkToLight(1, 6, 9, 12, 15, 1, KEBOLEHPERCAYAAN);

                this.AddDarkToLight(5, 40, 65, 85, 100, 5, BERFIKIRAN_RASIONAL);

                this.AddDarkToLight(5, 35, 55, 90, 100, 5, KETENANGAN);
                this.AddDarkToLight(5, 25, 50, 85, 100, 5, KECERIAAN);
                this.AddLightToDark(5, 25, 55, 80, 100, 5, LUAHAN_PERASAAN);
                this.AddLightToDark(5, 25, 55, 80, 100, 5, BERSIMPATI);

                this.AddLightToDark(5, 20, 40, 70, 100, 5, AKTIF_SOSIAL);
                this.AddLightToDark(5, 20, 40, 60, 100, 5, BERSIKAP_ASERTIF);
                this.AddDarkToLight(5, 30, 55, 80, 100, 5, BERTOLERANSI);
                this.AddLightToDark(5, 55, 70, 85, 100, 5, DAYA_TAHAN);
            }
            if (pengguna.Jantina == "Perempuan")
            {
                this.AddDarkToLight(1, 6, 9, 12, 15, 1, KEBOLEHPERCAYAAN);

                this.AddDarkToLight(5, 40, 65, 85, 100, 5, BERFIKIRAN_RASIONAL);
                this.AddDarkToLight(5, 30, 55, 90, 100, 5, KETENANGAN);
                this.AddDarkToLight(5, 25, 50, 85, 100, 5, KECERIAAN);
                this.AddLightToDark(5, 25, 55, 80, 100, 5, LUAHAN_PERASAAN);
                this.AddLightToDark(5, 25, 55, 80, 100, 5, BERSIMPATI);

                this.AddLightToDark(5, 20, 40, 70, 100, 5, AKTIF_SOSIAL);
                this.AddLightToDark(5, 20, 40, 60, 100, 5, BERSIKAP_ASERTIF);
                this.AddDarkToLight(5, 30, 55, 80, 100, 5, BERTOLERANSI);
                this.AddLightToDark(5, 50, 60, 70, 100, 5, DAYA_TAHAN);
            }
        }



        public override string ToString()
        {
            var setting = new JsonSerializerSettings();
            setting.Converters.Add(new StringEnumConverter());
            setting.Formatting = Formatting.Indented;
            return JsonConvert.SerializeObject(this, setting);
        }



        public HlpResult FR => ComputeResult("FR");
        public HlpResult KT => ComputeResult("KT");
        public HlpResult KC => ComputeResult("KC");
        public HlpResult LP => ComputeResult("LP");
        public HlpResult DT => ComputeResult("DT");
        public HlpResult SM => ComputeResult("SM");
        public HlpResult TL => ComputeResult("TL");
        public HlpResult AF => ComputeResult("AF");
        public HlpResult AS => ComputeResult("AS");


        public HlpResult KB => ComputeResultNoJantina("KB");


        private HlpResult ComputeResult(string tret)
        {
            var point = m_sesi.JawapanCollection.Where(a => a.Trait == tret).Sum(a => a.Nilai);
            var list = m_scoreTables
                          .Where(x => x.Jantina == m_pengguna.Jantina)
                          .Where(x => x.Tret == tret && point >= x.NilaiMin && point <= x.NilaiMax)
                          .ToList();
            if (list.Count != 1)
                throw new Exception($"Overlap score tables [{list.Count}] {string.Join(";", list.Select(x => x.Id).ToArray())} -> Tret :{tret},  Point : {point},  Jantina :{m_pengguna.Jantina}");

            var percent = m_scoreTables
                            .Where(x => x.Jantina == m_pengguna.Jantina)
                            .Single(x => x.Tret == tret && point >= x.NilaiMin && point <= x.NilaiMax)
                            .Percentile;
            var score = m_scoreTables.Where(x => x.Tret == tret)
                          .Where(x => point >= x.NilaiMin && point <= x.NilaiMax)
                          .Where(x => x.Jantina == m_pengguna.Jantina)
                          .Select(x => x.Skor).Single();
            var result = new HlpResult
            {
                Tret = tret,
                Skor = score,
                Percentile = percent,
                Point = point
            };
            result.Recommendation = m_recommendations.Where(x => x.Tret == tret && x.Skor == result.Skor)
                    .Select(x => x.Text)
                    .SingleOrDefault();
            return result;
        }


        private HlpResult ComputeResultNoJantina(string tret)
        {
            var point = m_sesi.JawapanCollection.Where(a => a.Trait == tret).Sum(a => a.Nilai);

            var temps = m_scoreTables

                              .Where(x => x.Tret == tret && point >= x.NilaiMin && point <= x.NilaiMax)
                              .ToList();
            if (temps.Count < 1)
            {
                throw new Exception($"TRET:{tret} and point: {point} have {temps.Count} entries");

            }

            if (temps.Count > 1)
            {
                var itemsx = string.Join(",", temps.Select(x => x.Id));
                throw new Exception($"TRET:{tret} and point: {point} have {temps.Count} entries : {itemsx}");
            }




            var percent = m_scoreTables
                            .Single(x => x.Tret == tret && point >= x.NilaiMin && point <= x.NilaiMax)
                            .Percentile;
            var score = m_scoreTables.Where(x => x.Tret == tret)
                          .Where(x => point >= x.NilaiMin && point <= x.NilaiMax)
                          .Select(x => x.Skor).Single();
            var result = new HlpResult
            {
                Tret = tret,
                Skor = score,
                Percentile = percent,
                Point = point
            };
            result.Recommendation = m_recommendations.Where(x => x.Tret == tret && x.Skor == result.Skor)
                    .Select(x => x.Text)
                    .SingleOrDefault();
            return result;
        }



        public HlpRecomendation RecommendationKBY => m_recommendations.Single(x => x.Tret == "KB" && x.Skor == this.KBY.Skor);
        public HlpRecomendation RecommendationASF => m_recommendations.Single(x => x.Tret == "AF" && x.Skor == this.ASF.Skor);
        public HlpRecomendation RecommendationAKS => m_recommendations.Single(x => x.Tret == "AS" && x.Skor == this.AKS.Skor);
        public HlpRecomendation RecommendationDT => m_recommendations.Single(x => x.Tret == "DT" && x.Skor == this.DT.Skor);
        public HlpRecomendation RecommendationRAS => m_recommendations.Single(x => x.Tret == "FR" && x.Skor == this.RAS.Skor);
        public HlpRecomendation RecommendationSIM => m_recommendations.Single(x => x.Tret == "SM" && x.Skor == this.SIM.Skor);
        public HlpRecomendation RecommendationKCN => m_recommendations.Single(x => x.Tret == "KC" && x.Skor == this.KCN.Skor);
        public HlpRecomendation RecommendationKTG => m_recommendations.Single(x => x.Tret == "KT" && x.Skor == this.KTG.Skor);
        public HlpRecomendation RecommendationTOL => m_recommendations.Single(x => x.Tret == "TL" && x.Skor == this.TOL.Skor);
        public HlpRecomendation RecommendationLPN => m_recommendations.Single(x => x.Tret == "LP" && x.Skor == this.LPN.Skor);





        public SkorHlp DTY
        {
            get
            {
                var val = this.Sesi.JawapanCollection.Where(x => x.Trait == "DT").Sum(x => x.Nilai);
                return m_scoreTables.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "DT" && x.Jantina == this.Pengguna.Jantina);
            }
        }

        public SkorHlp LPN
        {
            get
            {
                var val = this.Sesi.JawapanCollection.Where(x => x.Trait == "LP").Sum(x => x.Nilai);
                return m_scoreTables.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "LP" && x.Jantina == this.Pengguna.Jantina);
            }
        }
        public SkorHlp KCN
        {
            get
            {
                var val = this.Sesi.JawapanCollection.Where(x => x.Trait == "KC").Sum(x => x.Nilai);
                return m_scoreTables.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "KC" && x.Jantina == this.Pengguna.Jantina);
            }
        }
        public SkorHlp TOL
        {
            get
            {
                var val = this.Sesi.JawapanCollection.Where(x => x.Trait == "TL").Sum(x => x.Nilai);
                return m_scoreTables.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "TL" && x.Jantina == this.Pengguna.Jantina);
            }
        }

        public SkorHlp RAS
        {
            get
            {
                var val = this.Sesi.JawapanCollection.Where(x => x.Trait == "FR").Sum(x => x.Nilai);
                return m_scoreTables.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "FR" && x.Jantina == this.Pengguna.Jantina);
            }
        }


        public SkorHlp AKS
        {
            get
            {
                var val = this.Sesi.JawapanCollection.Where(x => x.Trait == "AS").Sum(x => x.Nilai);
                return m_scoreTables.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "AS" && x.Jantina == this.Pengguna.Jantina);
            }
        }

        public SkorHlp KBY
        {
            get
            {
                var val = this.Sesi.JawapanCollection.Where(x => x.Trait == "KB").Sum(x => x.Nilai);
                if (val == 0)
                    val = 1;
                return m_scoreTables.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "KB");
            }
        }

        public SkorHlp KTG
        {
            get
            {
                var ktg = this.Sesi.JawapanCollection.Where(x => x.Trait == "KT").Sum(x => x.Nilai);
                return m_scoreTables.Single(x => x.NilaiMin <= ktg && ktg <= x.NilaiMax && x.Tret == "KT" && x.Jantina == this.Pengguna.Jantina);
            }
        }

        public SkorHlp SIM
        {
            get
            {
                var val = this.Sesi.JawapanCollection.Where(x => x.Trait == "SM").Sum(x => x.Nilai);
                return m_scoreTables.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "SM" && x.Jantina == this.Pengguna.Jantina);
            }
        }

        public SkorHlp ASF
        {
            get
            {
                var val = this.Sesi.JawapanCollection.Where(x => x.Trait == "AF").Sum(x => x.Nilai);
                return m_scoreTables.Single(x => val.IsBetween(x.NilaiMin, x.NilaiMax) && x.Tret == "AF" && x.Jantina == this.Pengguna.Jantina);
            }
        }

    }
}
