using System.Collections.Generic;
using System.Linq;
using Bespoke.epsikologi_ibkkodkerjaya.Domain;
using Bespoke.epsikologi_ibkrecommendation.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace web.sph.App_Code
{
    public class IbkTraitViewModel :LaporanViewModel
    {
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
        
        public IbkTraitViewModel(SesiUjian sesi)
        {
            Sesi = sesi;
        }
        
        [JsonIgnore]
        public IbkKodKerjaya IbkKodKerjaya { get; set; }
		[JsonIgnore]
        public IbkRecommendation IbkRecommendation { get; set; }

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

        public override string ToString()
        {
            var setting = new JsonSerializerSettings();
            setting.Converters.Add(new StringEnumConverter());
            setting.Formatting = Formatting.Indented;
            return JsonConvert.SerializeObject(this, setting);
        }
    }
}
