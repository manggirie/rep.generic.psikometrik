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
    public class IpTraitViewModel
    {
      private Bespoke.epsikologi_pengguna.Domain.Pengguna m_pengguna;
      public IpTraitViewModel(Bespoke.epsikologi_sesiujian.Domain.SesiUjian sesi, Bespoke.epsikologi_pengguna.Domain.Pengguna pengguna)
      {
          m_pengguna = pengguna;
           OneA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 1").Count(a => a.JawapanPilihan.StartsWith("a)"));
           OneB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 1").Count(a => a.JawapanPilihan.StartsWith("b)"));

           TwoA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 2").Count(a => a.JawapanPilihan.StartsWith("a)"));
           TwoB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 2").Count(a => a.JawapanPilihan.StartsWith("b)"));

           ThreeA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 3").Count(a => a.JawapanPilihan.StartsWith("a)"));
           ThreeB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 3").Count(a => a.JawapanPilihan.StartsWith("b)"));

           FourA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 4").Count(a => a.JawapanPilihan.StartsWith("a)"));
           FourB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 4").Count(a => a.JawapanPilihan.StartsWith("b)"));

           FiveA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 5").Count(a => a.JawapanPilihan.StartsWith("a)"));
           FiveB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 5").Count(a => a.JawapanPilihan.StartsWith("b)"));

           SixA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 6").Count(a => a.JawapanPilihan.StartsWith("a)"));
           SixB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 6").Count(a => a.JawapanPilihan.StartsWith("b)"));

           SevenA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 7").Count(a => a.JawapanPilihan.StartsWith("a)"));
           SevenB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 7").Count(a => a.JawapanPilihan.StartsWith("b)"));

           EightA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 8").Count(a => a.JawapanPilihan.StartsWith("a)"));
           EightB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 8").Count(a => a.JawapanPilihan.StartsWith("b)"));

           NineA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 9").Count(a => a.JawapanPilihan.StartsWith("a)"));
           NineB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 9").Count(a => a.JawapanPilihan.StartsWith("b)"));

           TenA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 10").Count(a => a.JawapanPilihan.StartsWith("a)"));
           TenB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 10").Count(a => a.JawapanPilihan.StartsWith("b)"));

           ElevenA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 11").Count(a => a.JawapanPilihan.StartsWith("a)"));
           ElevenB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 11").Count(a => a.JawapanPilihan.StartsWith("b)"));
           Sesi = sesi;


      }
      public int OneA {get;private set;}
      public int OneB {get;private set;}

      public int TwoA {get;private set;}
      public int TwoB {get;private set;}

      public int ThreeA {get;private set;}
      public int ThreeB {get;private set;}

      public int FourA {get;private set;}
      public int FourB {get;private set;}

      public int FiveA {get;private set;}
      public int FiveB {get;private set;}

      public int SixA {get;private set;}
      public int SixB {get;private set;}

      public int SevenA {get;private set;}
      public int SevenB {get;private set;}

      public int EightA {get;private set;}
      public int EightB {get;private set;}

      public int NineA {get;private set;}
      public int NineB {get;private set;}

      public int TenA {get;private set;}
      public int TenB {get;private set;}

      public int ElevenA {get;private set;}
      public int ElevenB {get;private set;}

      public int E { get{return OneA + TwoA;} }
      public int I { get{return OneB + TwoB;} }

      // kalau sama & lelaki -> E
      public string EI
      {
        get
        {
          if(E == I && m_pengguna.Jantina == "Lelaki" ) return "E";
          if(E == I && m_pengguna.Jantina == "Perempuan" ) return "I";
          return E > I ? "E" : "I";
        }
      }

      public int S { get{return ThreeA + FourA + FiveA;} }
      public int N { get{return ThreeB + FourB + FiveB;} }

      // kalau sama & lelaki -> S
      public string SN
      {
        get
        {

          if(E == I && m_pengguna.Jantina == "Lelaki" ) return "S";
          if(E == I && m_pengguna.Jantina == "Perempuan" ) return "N";
          return S > N ? "S" : "N";
        }
      }

      public int T { get{return SixA + SevenA + EightA;} }
      public int F { get{return SixB + SevenB + EightB;} }
      // kalau sama & lelaki -> T
      public string TF
      {
        get
        {
          if(E == I && m_pengguna.Jantina == "Lelaki" ) return "T";
          if(E == I && m_pengguna.Jantina == "Perempuan" ) return "F";
          return T > F ? "T" : "F";
        }
      }

      public int J { get{return NineA + TenA + ElevenA;} }
      public int P { get{return NineB + TenB + ElevenB;} }

      // kalau sama & lelaki -> J
      public string JP
      {
        get
        {

          if(E == I && m_pengguna.Jantina == "Lelaki" ) return "J";
          if(E == I && m_pengguna.Jantina == "Perempuan" ) return "P";
          return J > P ? "J" : "P";
        }
      }

      public string Result  { get{ return EI + SN + TF + JP; } }
      [JsonIgnore]
      public Bespoke.epsikologi_sesiujian.Domain.SesiUjian Sesi { get; set; }
      public override string ToString()
      {
          var setting = new JsonSerializerSettings();
         setting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
         setting.Formatting = Formatting.Indented;
         return JsonConvert.SerializeObject(this, setting);
      }
    }
}
