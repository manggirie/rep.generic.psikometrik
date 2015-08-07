using System.Linq;
using Bespoke.epsikologi_ipurecommendation.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Newtonsoft.Json;

namespace web.sph.App_Code
{
    public class IpuTraitViewModel : LaporanViewModel
    {

        public IpuTraitViewModel(SesiUjian sesi)
        {
            Sesi = sesi;
        }


        [JsonIgnore]
        public IpuRecommendation RecommendationA { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationB { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationC { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationD { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationE { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationF { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationG { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationH { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationI { get; set; }
        [JsonIgnore]
        public IpuRecommendation RecommendationJ { get; set; }

        public int? SkorA { get; set; }
        public int? SkorB { get; set; }
        public int? SkorC { get; set; }
        public int? SkorD { get; set; }
        public int? SkorE { get; set; }
        public int? SkorF { get; set; }
        public int? SkorG { get; set; }
        public int? SkorH { get; set; }
        public int? SkorI { get; set; }
        public int? SkorJ { get; set; }

        public int A
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "A").Sum(a => a.Nilai);
            }
        }

        public int B
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "B").Sum(a => a.Nilai);
            }
        }

        public int C
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "C").Sum(a => a.Nilai);
            }
        }
        public int D
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "D").Sum(a => a.Nilai);
            }
        }
        public int E
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "E").Sum(a => a.Nilai);
            }
        }
        public int F
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "F").Sum(a => a.Nilai);
            }
        }
        public int G
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "G").Sum(a => a.Nilai);
            }
        }
        public int H
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "H").Sum(a => a.Nilai);
            }
        }
        public int I
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "I").Sum(a => a.Nilai);
            }
        }
        public int J
        {
            get
            {
                return this.Sesi.JawapanCollection.Where(a => a.Trait == "J").Sum(a => a.Nilai);
            }
        }

    }
}
