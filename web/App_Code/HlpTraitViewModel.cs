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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;


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

      private Bespoke.epsikologi_sesiujian.Domain.SesiUjian m_sesi;
      private Bespoke.epsikologi_pengguna.Domain.Pengguna m_pengguna;
      private Bespoke.epsikologi_skorhlp.Domain.SkorHlp[] m_scoreTables;
      private Bespoke.epsikologi_hlprecomendation.Domain.HlpRecomendation[] m_recommendations;

      public HlpTraitViewModel(Bespoke.epsikologi_sesiujian.Domain.SesiUjian sesi,
        Bespoke.epsikologi_pengguna.Domain.Pengguna pengguna, Bespoke.epsikologi_skorhlp.Domain.SkorHlp[] scoreTables,
        Bespoke.epsikologi_hlprecomendation.Domain.HlpRecomendation[] recommendations)
      {
        // Skor Rendah
        // Skor Sederhana Rendah
        // Skor Sedarhana Tinggi
        // Skor Tinggi
          m_sesi = sesi;
          m_pengguna = pengguna;
          m_scoreTables = scoreTables;
          m_recommendations = recommendations;
      }

      public override string ToString()
      {
          var setting = new JsonSerializerSettings();
           setting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
           setting.Formatting = Formatting.Indented;
           return JsonConvert.SerializeObject(this, setting);
      }

      [JsonIgnore]
      public Bespoke.epsikologi_pengguna.Domain.Pengguna Pengguna { get{ return m_pengguna;} }
      [JsonIgnore]
      public Bespoke.epsikologi_sesiujian.Domain.SesiUjian Sesi { get{ return m_sesi;} }

      public int KB { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "KB").Sum(a => a.Nilai); } }
      public int KT { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "KT").Sum(a => a.Nilai); } }
      public int KC { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "KC").Sum(a => a.Nilai); } }
      public int LP { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "LP").Sum(a => a.Nilai); } }
      public int AS { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "AS").Sum(a => a.Nilai); } }
      public int AF { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "AF").Sum(a => a.Nilai); } }
      public int TL { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "TL").Sum(a => a.Nilai); } }
      public int SM { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "SM").Sum(a => a.Nilai); } }
      public int DT { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "DT").Sum(a => a.Nilai); } }

      public string KBScore
      {
        get
        {
          var score = m_scoreTables.Single(x => x.Tret == "KB" && KB >= x.NilaiMin && KB <= x.NilaiMax);
          return score.Skor;
        }
      }

      public string KBRecommendation
      {
        get
        {
            var rec = m_recommendations.Where(x => x.Tret == "KB" && x.Skor == KBScore)
                    .Select(x => x.Text)
                    .SingleOrDefault();
            if(null == rec)
              throw new Exception("Cannot find recomendation for KB with score " + KBScore);

            return rec;

        }
      }

      public HlpResult FR
      {
        get
        {
          const string TRET = "FR";
          var point = m_sesi.JawapanCollection.Where(a => a.Trait == TRET).Sum(a => a.Nilai);
          var percent =  m_scoreTables
                          .Where(x => x.Jantina == m_pengguna.Jantina)
                          .Single(x => x.Tret == TRET && point >= x.NilaiMin && point <= x.NilaiMax)
                          .Percentile;
          var score = m_scoreTables.Where(x => x.Tret == TRET)
                        .Where(x => point >= x.NilaiMin && point <= x.NilaiMax)
                        .Where(x => x.Jantina == m_pengguna.Jantina)
                        .Select(x => x.Skor).Single();
          var result = new HlpResult
                    {
                        Tret = TRET,
                        Skor = score ,
                        Percentile = percent,
                        Point = point
                    };
          result.Recommendation = m_recommendations.Where(x => x.Tret == TRET && x.Skor == result.Skor)
                  .Select(x => x.Text)
                  .SingleOrDefault();
          return result;
        }
      }

    }
}
