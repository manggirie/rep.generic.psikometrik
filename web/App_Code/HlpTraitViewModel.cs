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
    public class HlpTraitViewModel
    {

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
        }

        public override string ToString()
        {
            var setting = new JsonSerializerSettings();
            setting.Converters.Add(new StringEnumConverter());
            setting.Formatting = Formatting.Indented;
            return JsonConvert.SerializeObject(this, setting);
        }

        [JsonIgnore]
        public Pengguna Pengguna => m_pengguna;

        [JsonIgnore]
        public SesiUjian Sesi => m_sesi;

        
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


    }
}
