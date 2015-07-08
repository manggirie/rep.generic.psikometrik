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
    public class HlpTraitViewModel
    {
      public const string SkorSederhanaRendah = "Skor Sederhana Rendah";
      public const string SkorRendah = "Skor Rendah";
      public const string SkorSederhanaTinggi = "Skor Sederhana Tinggi";
      public const string SkorTinggi = "Skor Tinggi";

      private Bespoke.epsikologi_sesiujian.Domain.SesiUjian m_sesi;
      private Bespoke.epsikologi_pengguna.Domain.Pengguna m_pengguna;
      private Bespoke.epsikologi_skorhlp.Domain.SkorHlp[] m_scoreTables;

      public HlpTraitViewModel(Bespoke.epsikologi_sesiujian.Domain.SesiUjian sesi, Bespoke.epsikologi_pengguna.Domain.Pengguna pengguna, Bespoke.epsikologi_skorhlp.Domain.SkorHlp[] scoreTables)
      {
        // Skor Rendah
        // Skor Sederhana Rendah
        // Skor Sedarhana Tinggi
        // Skor Tinggi
          m_sesi = sesi;
          m_pengguna = pengguna;
          m_scoreTables = scoreTables;
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
      public int FR { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "FR").Sum(a => a.Nilai); } }
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
            switch (KBScore)
            {
              case SkorRendah: return "Jawapan tidak konsisten dan tafsiran laporan tidak tepat.";
              case SkorSederhanaRendah : return "Jawapan kurang konsisten dan tafsiran laporan kurang tepat.";
              case SkorSederhanaTinggi : return "Jawapan agak konsisten dengan sedikit ketidaktepatan tafsiran laporan.";
              case SkorTinggi : return "Jawapan konsisten dan tafsiran laporan boleh diterima.";
              default: throw new InvalidOperationException("We did not exped for KB to be " + KB);
            }
        }
      }

      public string BerfikiranRasional
      {
        get
        {
          if(m_pengguna.Jantina == "Lelaki")
          {
            if(FR >= 18 && FR <= 40) return SkorRendah;
            if(FR >= 13 && FR <= 17) return SkorSederhanaRendah;
            if(FR >= 8 && FR <= 12) return SkorSederhanaTinggi;
            if(FR >= 0 && FR <= 7) return SkorTinggi;
            throw new Exception("Cannot calculate FR Lelaki  : " + FR);
          }

          if(FR >= 21 && FR <= 40) return SkorRendah;
          if(FR >= 15 && FR <= 20) return SkorSederhanaRendah;
          if(FR >= 10 && FR <= 14) return SkorSederhanaTinggi;
          if(FR >= 0 && FR <= 9) return SkorTinggi;
          throw new Exception("Cannot calculate FR Perempuan : " + FR);

        }
      }

      public string BerfikiranRasionalText
      {
        get
        {
            switch (BerfikiranRasional)
            {
              case SkorRendah: return "Sangat sukar berfikir secara rasional atau membuat tindakan yang tepat apabila mengalami konflik yang mengganggu fikiran atau perasaan.";
              case SkorSederhanaRendah : return "Sukar berfikir secara rasional atau membuat tindakan yang tepat apabila mengalami konflik yang mengganggu fikiran atau perasaan.";
              case SkorSederhanaTinggi : return "Bersikap dan berfikiran rasional apabila membuat keputusan walaupun mengalami tekanan perasaan.";
              case SkorTinggi : return "Bersikap dan berfikiran sangat rasional apabila membuat keputusan walaupun mengalami tekanan perasaan.";
              default: throw new InvalidOperationException("We did not exped for KB to be " + KB);
            }
        }
      }


    }
}
