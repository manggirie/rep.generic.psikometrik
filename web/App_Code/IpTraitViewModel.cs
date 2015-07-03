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
      public IpTraitViewModel(Bespoke.epsikologi_sesiujian.Domain.SesiUjian sesi)
      {


           OneA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 1").Count(a => a.Nilai == 1);
           OneB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 1").Count(a => a.Nilai == 0);

           TwoA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 2").Count(a => a.Nilai == 1);
           TwoB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 2").Count(a => a.Nilai == 0);

           ThreeA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 3").Count(a => a.Nilai == 1);
           ThreeB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 3").Count(a => a.Nilai == 0);

           FourA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 4").Count(a => a.Nilai == 1);
           FourB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 4").Count(a => a.Nilai == 0);

           FiveA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 5").Count(a => a.Nilai == 1);
           FiveB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 5").Count(a => a.Nilai == 0);

           SixA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 6").Count(a => a.Nilai == 1);
           SixB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 6").Count(a => a.Nilai == 0);

           SevenA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 7").Count(a => a.Nilai == 1);
           SevenB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 7").Count(a => a.Nilai == 0);

           EightA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 8").Count(a => a.Nilai == 1);
           EightB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 8").Count(a => a.Nilai == 0);

           NineA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 9").Count(a => a.Nilai == 1);
           NineB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 9").Count(a => a.Nilai == 0);

           TenA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 10").Count(a => a.Nilai == 1);
           TenB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 10").Count(a => a.Nilai == 0);

           ElevenA = sesi.JawapanCollection.Where(s => s.Trait == "Skor 11").Count(a => a.Nilai == 1);
           ElevenB = sesi.JawapanCollection.Where(s => s.Trait == "Skor 11").Count(a => a.Nilai == 0);
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
      public string EI { get{ return E > I ? "E" : "I";} }

      public int S { get{return ThreeA + FourA + FiveA;} }
      public int N { get{return ThreeB + FourB + FiveB;} }
      public string SN { get{ return S > N ? "S" : "N";} }

      public int T { get{return SixA + SevenA + EightA;} }
      public int F { get{return SixB + SevenB + EightB;} }
      public string TF { get{ return T > F ? "T" : "F";} }

      public int J { get{return NineA + TenA + ElevenA;} }
      public int P { get{return NineB + TenB + ElevenB;} }
      public string JP { get{ return J > P ? "J" : "P";} }

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