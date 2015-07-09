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



      public HlpResult FR { get { return ComputeResult("FR"); } }
      public HlpResult KT { get { return ComputeResult("KT"); } }
      public HlpResult KC { get { return ComputeResult("KC"); } }
      public HlpResult LP { get { return ComputeResult("LP"); } }
      public HlpResult DT { get { return ComputeResult("DT"); } }
      public HlpResult SM { get { return ComputeResult("SM"); } }
      public HlpResult TL { get { return ComputeResult("TL"); } }
      public HlpResult AF { get { return ComputeResult("AF"); } }
      public HlpResult AS { get { return ComputeResult("AS"); } }



      public HlpResult KB
      {
        get
        {
          return ComputeResultNoJantina("KB");

        }
      }


      private HlpResult ComputeResult(string TRET)
      {
          var point = m_sesi.JawapanCollection.Where(a => a.Trait == TRET).Sum(a => a.Nilai);
          var list =  m_scoreTables
                        .Where(x => x.Jantina == m_pengguna.Jantina)
                        .Where(x => x.Tret == TRET && point >= x.NilaiMin && point <= x.NilaiMax)
                        .ToList();
          if(list.Count != 1)
            throw new Exception("Overlap score tables " + string.Join(";", list.Select(x => x.Id).ToArray()) + " -> Tret :" + TRET + " Point : " + point + " Jantina :" + m_pengguna.Jantina);

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


      private HlpResult ComputeResultNoJantina(string TRET)
      {
          var point = m_sesi.JawapanCollection.Where(a => a.Trait == TRET).Sum(a => a.Nilai);
          var percent =  m_scoreTables
                          .Single(x => x.Tret == TRET && point >= x.NilaiMin && point <= x.NilaiMax)
                          .Percentile;
          var score = m_scoreTables.Where(x => x.Tret == TRET)
                        .Where(x => point >= x.NilaiMin && point <= x.NilaiMax)
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
