using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_permohonan.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_ujian.Domain;
using Bespoke.Sph.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace web.sph.App_Code
{
    public class LaporanViewModel
    {
        [JsonIgnore]
        public Pengguna Pengguna { get; set; }
        [JsonIgnore]
        public SesiUjian Sesi { get; set; }

        [JsonIgnore]
        public Ujian Ujian { get; set; }
        [JsonIgnore]
        public Permohonan Permohonan { get; set; }

        public override string ToString()
        {
            var setting = new JsonSerializerSettings();
            setting.Converters.Add(new StringEnumConverter());
            setting.Formatting = Formatting.Indented;
            return JsonConvert.SerializeObject(this, setting);
        }

        public ObjectCollection<Td> TdCollection { get; } = new ObjectCollection<Td>();


        protected void AddDarkToLight(int start, int step1, int step2, int step3, int max, int step, string tret)
        {
            for (int i = start; i < step1; i += step)
            {
                this.TdCollection.Add(new Td { Value = i, Shade = Shades.VeryDark, Tret = tret });
            }
            for (int i = step1; i < step2; i += step)
            {
                this.TdCollection.Add(new Td { Value = i, Shade = Shades.Dark, Tret = tret });
            }
            for (int i = step2; i < step3; i += step)
            {
                this.TdCollection.Add(new Td { Value = i, Shade = Shades.Light, Tret = tret });
            }
            for (int i = step3; i <= max; i += step)
            {
                this.TdCollection.Add(new Td { Value = i, Shade = Shades.VeryLight, Tret = tret });
            }
        }
        protected void AddLightToDark(int start, int step1, int step2, int step3, int max, int step, string tret)
        {
            for (int i = start; i < step1; i += step)
            {
                this.TdCollection.Add(new Td { Value = i, Shade = Shades.VeryLight, Tret = tret });
            }
            for (int i = step1; i < step2; i += step)
            {
                this.TdCollection.Add(new Td { Value = i, Shade = Shades.Light, Tret = tret });
            }
            for (int i = step2; i < step3; i += step)
            {
                this.TdCollection.Add(new Td { Value = i, Shade = Shades.Dark, Tret = tret });
            }
            for (int i = step3; i <= max; i += step)
            {
                this.TdCollection.Add(new Td { Value = i, Shade = Shades.VeryDark, Tret = tret });
            }
        }

    }
}