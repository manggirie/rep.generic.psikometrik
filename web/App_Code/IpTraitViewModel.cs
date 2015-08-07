using System.Linq;
using Bespoke.epsikologi_iprecommendation.Domain;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
// ReSharper disable InconsistentNaming

namespace web.sph.App_Code
{
    public class IpTraitViewModel : LaporanViewModel
    {
        public IpTraitViewModel(SesiUjian sesi, Pengguna pengguna)
        {
            this.Pengguna = pengguna;
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

        public IpRecommendation Recommendation { get; set; }

        public int OneA { get; }
        public int OneB { get; }

        public int TwoA { get; }
        public int TwoB { get; }

        public int ThreeA { get; }
        public int ThreeB { get; }

        public int FourA { get; }
        public int FourB { get; }

        public int FiveA { get; }
        public int FiveB { get; }

        public int SixA { get; }
        public int SixB { get; }

        public int SevenA { get; }
        public int SevenB { get; }

        public int EightA { get; }
        public int EightB { get; }

        public int NineA { get; }
        public int NineB { get; }

        public int TenA { get; }
        public int TenB { get; }

        public int ElevenA { get; }
        public int ElevenB { get; }

        public int E => OneA + TwoA;
        public int I => OneB + TwoB;

        // kalau sama & lelaki -> E
        public string EI
        {
            get
            {
                if (E == I && this.Pengguna.Jantina == "Lelaki") return "E";
                if (E == I && this.Pengguna.Jantina == "Perempuan") return "I";
                return E > I ? "E" : "I";
            }
        }

        public int S => ThreeA + FourA + FiveA;
        public int N => ThreeB + FourB + FiveB;

        // kalau sama & lelaki -> S
        public string SN
        {
            get
            {

                if (E == I && this.Pengguna.Jantina == "Lelaki") return "S";
                if (E == I && this.Pengguna.Jantina == "Perempuan") return "N";
                return S > N ? "S" : "N";
            }
        }

        public int T => SixA + SevenA + EightA;
        public int F => SixB + SevenB + EightB;
        // kalau sama & lelaki -> T
        public string TF
        {
            get
            {
                if (E == I && this.Pengguna.Jantina == "Lelaki") return "T";
                if (E == I && this.Pengguna.Jantina == "Perempuan") return "F";
                return T > F ? "T" : "F";
            }
        }

        public int J => NineA + TenA + ElevenA;
        public int P => NineB + TenB + ElevenB;

        // kalau sama & lelaki -> J
        public string JP
        {
            get
            {

                if (E == I && this.Pengguna.Jantina == "Lelaki") return "J";
                if (E == I && this.Pengguna.Jantina == "Perempuan") return "P";
                return J > P ? "J" : "P";
            }
        }

        public string Result => EI + SN + TF + JP;


    }
}
