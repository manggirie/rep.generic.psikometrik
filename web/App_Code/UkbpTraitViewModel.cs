using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.Sph.Domain;

namespace web.sph.App_Code
{
    public class UkbpTraitViewModel : LaporanViewModel
    {
        private readonly SesiUjian m_sesiA;
        private readonly SesiUjian m_sesiB;
        public const string KEBOLEHPERCAYAAN = "Kebolehpercayaan";
        public const string NASIONALISME = "Nasionalisme";
        public const string KETENANGAN = "Ketenangan";
        public const string KECERIAAN = "Keceriaan";
        public const string LUAHAN_PERASAAN = "Luahan Perasaan";
        public const string BERSIMPATI = "Bersimpati";
        public const string BERFIKIRAN_RASIONAL = "Berfikiran Rasional";

        public const string BERSIKAP_ASERTIF = "Bersikap Asertif";
        public const string BERTOLERANSI = "Bertoleransi";
        public const string PENYIMPANGAN_TINGKAH_LAKU = "Penyimpangan Tingkah Laku";
        public const string AKTIF_SOSIAL = "Aktif Sosial";

        public UkbpTraitViewModel(SesiUjian sesiA, SesiUjian sesiB)
        {
            m_sesiA = sesiA;
            m_sesiB = sesiB;
            this.Sesi = sesiA;
       

            this.AddDarkToLight(1, 6, 9, 12, 15, 1, KEBOLEHPERCAYAAN);
            this.AddLightToDark(1, 8, 13, 17, 20, 1, NASIONALISME);
            this.AddDarkToLight(5, 30, 55, 90, 100, 5, KETENANGAN);
            this.AddDarkToLight(5, 25, 50, 85, 100, 5, KECERIAAN);
            this.AddLightToDark(5, 25, 55, 80, 100, 5, LUAHAN_PERASAAN);
            this.AddLightToDark(5, 25, 55, 80, 100, 5, BERSIMPATI);
            this.AddDarkToLight(5, 40, 65, 85, 100, 5, BERFIKIRAN_RASIONAL);

            this.AddLightToDark(5,20,40,70,100,5,AKTIF_SOSIAL);
            this.AddLightToDark(5,20,40,60,100,5,BERSIKAP_ASERTIF);
            this.AddDarkToLight(5,30,55,80,100,5,BERTOLERANSI);
            this.AddLightToDark(5,50,60,70,100,5,PENYIMPANGAN_TINGKAH_LAKU);

        }



        private void AddDarkToLight(int start, int step1, int step2, int step3, int max, int step, string tret)
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
        private void AddLightToDark(int start, int step1, int step2, int step3, int max, int step, string tret)
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

        public ObjectCollection<Td> TdCollection { get; } = new ObjectCollection<Td>();
    }
}