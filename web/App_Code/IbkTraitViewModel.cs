using System;
using System.Web.Mvc;
using System.Text;
using Bespoke.Sph.Domain;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;


namespace web.sph.App_Code
{
    public class IbkTraitViewModel
    {
      class CategoryScore
      {
        public CategoryScore(string cat, int val)
        {
          this.Name = cat;
          this.Value = val;
        }
        public string Name  { get; set; }
        public int Value { get; set; }
      }

      [JsonIgnore]
      public Bespoke.epsikologi_sesiujian.Domain.SesiUjian Sesi { get; set; }
      public IbkTraitViewModel(Bespoke.epsikologi_sesiujian.Domain.SesiUjian sesi)
      {
           Sesi = sesi;
      }

      [JsonIgnore]
      public Bespoke.epsikologi_pengguna.Domain.Pengguna Pengguna {get;set;}

      [JsonIgnore]
      public Bespoke.epsikologi_ujian.Domain.Ujian Ujian {get;set;}
      [JsonIgnore]
      public Bespoke.epsikologi_permohonan.Domain.Permohonan Permohonan {get;set;}
      [JsonIgnore]
      public Bespoke.epsikologi_ibkkodkerjaya.Domain.IbkKodKerjaya IbkKodKerjaya {get;set;}
      [JsonIgnore]
      public Bespoke.epsikologi_ibkrecommendation.Domain.IbkRecommendation IbkRecommendation {get;set;}

      public string KodKerjaya1
      {
          get
          {
            var results = new List<CategoryScore>();
            results.Add(new CategoryScore("P", P));
            results.Add(new CategoryScore("E", E));
            results.Add(new CategoryScore("K", K));
            results.Add(new CategoryScore("M", M));
            results.Add(new CategoryScore("I", I));
            results.Add(new CategoryScore("T", T));

            var temp = results.OrderByDescending(s => s.Value).Select(s => s.Name).Take(3);
            return string.Join("", temp);
          }
      }

      public string KodKerjaya2
      {
          get
          {
            var kod1 = this.KodKerjaya1;
            char[] array = this.KodKerjaya1.ToCharArray();
            char temp = array[1];
            array[1] = array[2];
            array[2] = temp;
            return new string(array);
          }
      }

      public string KodKerjaya
      {
        get
        {
          return this.KodKerjaya1 + "/" + this.KodKerjaya2;
        }
      }


      public int P
      {
        get
        {
          return
            this.Sesi.JawapanCollection.Where(a => a.Trait == "A1").Sum(a => a.Nilai)+
            this.Sesi.JawapanCollection.Where(a => a.Trait == "A2").Sum(a => a.Nilai)+
            this.Sesi.JawapanCollection.Where(a => a.Trait == "A3").Sum(a => a.Nilai);
        }
      }
      public int E
      {
        get
        {
          return
            this.Sesi.JawapanCollection.Where(a => a.Trait == "B1").Sum(a => a.Nilai)+
            this.Sesi.JawapanCollection.Where(a => a.Trait == "B2").Sum(a => a.Nilai)+
            this.Sesi.JawapanCollection.Where(a => a.Trait == "B3").Sum(a => a.Nilai);
        }
      }


      public int K
      {
        get
        {
          return
            this.Sesi.JawapanCollection.Where(a => a.Trait == "C1").Sum(a => a.Nilai)+
            this.Sesi.JawapanCollection.Where(a => a.Trait == "C2").Sum(a => a.Nilai)+
            this.Sesi.JawapanCollection.Where(a => a.Trait == "C3").Sum(a => a.Nilai);
        }
      }

      public int M
      {
        get
        {
          return
            this.Sesi.JawapanCollection.Where(a => a.Trait == "D1").Sum(a => a.Nilai)+
            this.Sesi.JawapanCollection.Where(a => a.Trait == "D2").Sum(a => a.Nilai)+
            this.Sesi.JawapanCollection.Where(a => a.Trait == "D3").Sum(a => a.Nilai);
        }
      }

      public int I
      {
        get
        {
          return
            this.Sesi.JawapanCollection.Where(a => a.Trait == "E1").Sum(a => a.Nilai)+
            this.Sesi.JawapanCollection.Where(a => a.Trait == "E2").Sum(a => a.Nilai)+
            this.Sesi.JawapanCollection.Where(a => a.Trait == "E3").Sum(a => a.Nilai);
        }
      }

      public int T
      {
        get
        {
          return
            this.Sesi.JawapanCollection.Where(a => a.Trait == "F1").Sum(a => a.Nilai)+
            this.Sesi.JawapanCollection.Where(a => a.Trait == "F2").Sum(a => a.Nilai)+
            this.Sesi.JawapanCollection.Where(a => a.Trait == "F3").Sum(a => a.Nilai);
        }
      }

      public override string ToString()
      {
          var setting = new JsonSerializerSettings();
         setting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
         setting.Formatting = Formatting.Indented;
         return JsonConvert.SerializeObject(this, setting);
      }
    }
}
