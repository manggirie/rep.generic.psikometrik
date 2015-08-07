using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_permohonan.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_ujian.Domain;
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
    }
}