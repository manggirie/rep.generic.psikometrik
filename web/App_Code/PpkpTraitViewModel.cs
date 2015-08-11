using System.Collections.Generic;
using System.Linq;
using Bespoke.epsikologi_ppkprecommendation.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_skorppkp.Domain;
using Newtonsoft.Json;
using WebGrease.Css.Extensions;

namespace web.sph.App_Code
{
    public class PpkpTraitViewModel : LaporanViewModel
    {
        public class SkorKestabilanEmosiType
        {
            private readonly PpkpTraitViewModel m_vm;
            private readonly IList<SkorPpkp> m_list = new List<SkorPpkp>();
            public SkorKestabilanEmosiType(PpkpTraitViewModel vm, SkorPpkp[] list)
            {
                m_vm = vm;
                m_list.Clear();
                list.ForEach(x => m_list.Add(x));
            }

            public IEnumerable<SkorPpkp> Emosi
            {
                get
                {
                    var a1 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "A1").Sum(x => x.Nilai);
                    var a2 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "A2").Sum(x => x.Nilai);
                    var a3 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "A3").Sum(x => x.Nilai);
                    var a4 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "A4").Sum(x => x.Nilai);

                    var scr1 = m_list.Single(x => x.NilaiMin <= a1 && a1 <= x.NilaiMax && x.Tret == "A1");
                    var scr2 = m_list.Single(x => x.NilaiMin <= a2 && a2 <= x.NilaiMax && x.Tret == "A2");
                    var scr3 = m_list.Single(x => x.NilaiMin <= a3 && a3 <= x.NilaiMax && x.Tret == "A3");
                    var scr4 = m_list.Single(x => x.NilaiMin <= a4 && a4 <= x.NilaiMax && x.Tret == "A4");

                    var list = new List<SkorPpkp> { scr1, scr2, scr2, scr3, scr4 };
                    return list;
                }
            }
            public IEnumerable<SkorPpkp> GayaBekerja
            {
                get
                {
                    var b1 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "B1").Sum(x => x.Nilai);
                    var b2 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "B2").Sum(x => x.Nilai);
                    var b3 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "B3").Sum(x => x.Nilai);
                    var b4 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "B4").Sum(x => x.Nilai);
                    var b5 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "B5").Sum(x => x.Nilai);

                    var scr1 = m_list.Single(x => x.NilaiMin <= b1 && b1 <= x.NilaiMax && x.Tret == "B1");
                    var scr2 = m_list.Single(x => x.NilaiMin <= b2 && b2 <= x.NilaiMax && x.Tret == "B2");
                    var scr3 = m_list.Single(x => x.NilaiMin <= b3 && b3 <= x.NilaiMax && x.Tret == "B3");
                    var scr4 = m_list.Single(x => x.NilaiMin <= b4 && b4 <= x.NilaiMax && x.Tret == "B4");
                    var scr5 = m_list.Single(x => x.NilaiMin <= b5 && b5 <= x.NilaiMax && x.Tret == "B5");

                    var list = new List<SkorPpkp> { scr1, scr2, scr2, scr3, scr4 , scr5};
                    return list;
                }
            }
            public IEnumerable<SkorPpkp> Pemikiran
            {
                get
                {
                    var c1 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "C1").Sum(x => x.Nilai);
                    var c2 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "C2").Sum(x => x.Nilai);
                    var c3 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "C3").Sum(x => x.Nilai);
                    var c4 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "C4").Sum(x => x.Nilai);

                    var scr1 = m_list.Single(x => x.NilaiMin <= c1 && c1 <= x.NilaiMax && x.Tret == "C1");
                    var scr2 = m_list.Single(x => x.NilaiMin <= c2 && c2 <= x.NilaiMax && x.Tret == "C2");
                    var scr3 = m_list.Single(x => x.NilaiMin <= c3 && c3 <= x.NilaiMax && x.Tret == "C3");
                    var scr4 = m_list.Single(x => x.NilaiMin <= c4 && c4 <= x.NilaiMax && x.Tret == "C4");

                    var list = new List<SkorPpkp> { scr1, scr2, scr2, scr3, scr4 };
                    return list;
                }
            }
            public IEnumerable<SkorPpkp> Interpersonal
            {
                get
                {
                    var d1 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "D1").Sum(x => x.Nilai);
                    var d2 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "D2").Sum(x => x.Nilai);
                    var d3 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "D3").Sum(x => x.Nilai);
                    var d4 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "D4").Sum(x => x.Nilai);
                    var d5 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "D5").Sum(x => x.Nilai);
                    var d6 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "D6").Sum(x => x.Nilai);

                    var scr1 = m_list.Single(x => x.NilaiMin <= d1 && d1 <= x.NilaiMax && x.Tret == "C1");
                    var scr2 = m_list.Single(x => x.NilaiMin <= d2 && d2 <= x.NilaiMax && x.Tret == "C2");
                    var scr3 = m_list.Single(x => x.NilaiMin <= d3 && d3 <= x.NilaiMax && x.Tret == "C3");
                    var scr4 = m_list.Single(x => x.NilaiMin <= d4 && d4 <= x.NilaiMax && x.Tret == "C4");
                    var scr5 = m_list.Single(x => x.NilaiMin <= d5 && d5 <= x.NilaiMax && x.Tret == "C5");
                    var scr6 = m_list.Single(x => x.NilaiMin <= d6 && d6 <= x.NilaiMax && x.Tret == "C6");

                    var list = new List<SkorPpkp> { scr1, scr2, scr2, scr3, scr4, scr5, scr6 };
                    return list;
                }
            }
            public IEnumerable<SkorPpkp> Peribadi
            {
                get
                {
                    var e1 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "E1").Sum(x => x.Nilai);
                    var e2 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "E2").Sum(x => x.Nilai);
                    var e3 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "E3").Sum(x => x.Nilai);
                    var e4 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "E4").Sum(x => x.Nilai);
                    var e5 = m_vm.Sesi.JawapanCollection.Where(x => x.Trait == "E5").Sum(x => x.Nilai);

                    var scr1 = m_list.Single(x => x.NilaiMin <= e1 && e1 <= x.NilaiMax && x.Tret == "E1");
                    var scr2 = m_list.Single(x => x.NilaiMin <= e2 && e2 <= x.NilaiMax && x.Tret == "E2");
                    var scr3 = m_list.Single(x => x.NilaiMin <= e3 && e3 <= x.NilaiMax && x.Tret == "E3");
                    var scr4 = m_list.Single(x => x.NilaiMin <= e4 && e4 <= x.NilaiMax && x.Tret == "E4");
                    var scr5 = m_list.Single(x => x.NilaiMin <= e5 && e5 <= x.NilaiMax && x.Tret == "E5");

                    var list = new List<SkorPpkp> { scr1, scr2, scr2, scr3, scr4, scr5 };
                    return list;
                }
            }

        }
        public class ProfilPersonalitiDimensiUmumType
        {
            private readonly PpkpTraitViewModel m_vm;
            public ProfilPersonalitiDimensiUmumType(PpkpTraitViewModel vm)
            {
                m_vm = vm;
            }

            public int Emosi => m_vm.A1 + m_vm.A2 + m_vm.A3 + m_vm.A4;
            public int GayaBekerja => m_vm.B1 + m_vm.B2 + m_vm.B3 + m_vm.B4 + m_vm.B5;
            public int Pemikiran => m_vm.C1 + m_vm.C2 + m_vm.C3 + m_vm.C4;
            public int Interpersonal => m_vm.D1 + m_vm.D2 + m_vm.D3 + m_vm.D4 + m_vm.D5 + m_vm.D6;
            public int Peribadi => m_vm.E1 + m_vm.E2 + m_vm.E3 + m_vm.E4 + m_vm.E5;

        }
        public class ProfilKepimpinanType
        {
            private readonly PpkpTraitViewModel m_vm;
            public ProfilKepimpinanType(PpkpTraitViewModel vm)
            {
                m_vm = vm;
            }

            public int BerdayaTahan => ((m_vm.A1 + m_vm.A2 + m_vm.A3 + m_vm.A4) * 5 / (50 * 4));
            public int Asertif => ((m_vm.B1 + m_vm.B2 + m_vm.B3 + m_vm.B4 + m_vm.B5) * 5 / (50 * 4));
            public int Ekstrovert => ((m_vm.C1 + m_vm.C2 + m_vm.C3 + m_vm.C4) / (40 * 4)) * 5;
            public int Strategis => ((m_vm.D1 + m_vm.D2 + m_vm.D3 + m_vm.D4 + m_vm.D5 + m_vm.D6) * 5 / (60 * 4));
            public int Fokus => ((m_vm.E1 + m_vm.E2 + m_vm.E3 + m_vm.E4 + m_vm.E5) * 5 / (50 * 4));

        }
        public class KesesuaianPenempatanType
        {
            private readonly IList<string> m_trets;
            public KesesuaianPenempatanType(PpkpTraitViewModel vm)
            {
                m_trets = new List<string>
                {
                    vm.Emosi.Tret,
                    vm.Peribadi.Tret,
                    vm.Interpersonal.Tret,
                    vm.Pemikiran.Tret,
                    vm.GayaBekerja.Tret
                };
            }

            public int PembuatDasarDanStrategi
            {
                get
                {
                    var list = new[] { "Berdaya Tahan", "Ekstrovert", "Introvert", "Strategis", "Moderator", "Asertif", "Fokus" };
                    return m_trets.Count(t => list.Contains(t));
                }
            }
            public int PenyelesaiMasalah
            {
                get
                {
                    var list = new[] { "Responsif", "Berdaya Tahan", "Ambivert", "Strategis", "Moderator",
                        "Konvensional", "Perundingan", "Asertif", "Keseimbangan", "Spontan", "Fokus" };
                    return m_trets.Count(t => list.Contains(t));
                }
            }
            public double Penguatkuasa
            {
                get
                {
                    var list = new[] { "Berdaya Tahan", "Ekstrovert", "Ambivert", "Introvert", "Asertif" };

                    return m_trets.Count(t => list.Contains(t)) * 5d / 3d;
                }
            }
            public int OperasiRutin
            {
                get
                {
                    var list = new[] {"Berdaya Tahan", "Responsif", "Ekstrovert", "Introvert", "Strategis",
                        "Konvensional", "Moderator", "Asertif", "Perundingan", "Keseimbangan", "Spontan", "Fokus"};
                    return m_trets.Count(t => list.Contains(t));
                }
            }
            public int PerkhidmatanPelanggan
            {
                get
                {
                    var list = new[] { "Reaktif", "Responsif", "Berdaya Tahan", "Ekstrovert",
                        "Ambivert", "Strategis", "Konvensional", "Submisif", "Perundingan", "Asertif", "Fokus", "Spontan"};
                    return m_trets.Count(t => list.Contains(t));
                }
            }

        }

        private readonly List<PpkpRecommendation> m_recommendationList = new List<PpkpRecommendation>();
        public PpkpTraitViewModel(SesiUjian sesi, PpkpRecommendation[] list, SkorPpkp[] scores)
        {
            Sesi = sesi;
            m_recommendationList.Clear();
            m_recommendationList.AddRange(list);

            this.ProfilPersonaliti = new ProfilPersonalitiDimensiUmumType(this);
            this.ProfilKepimpinan = new ProfilKepimpinanType(this);
            this.KesesuaianPenempatan = new KesesuaianPenempatanType(this);
            this.SkorKestabilanEmosi = new SkorKestabilanEmosiType(this,scores);

        }


        [JsonIgnore]
        public PpkpRecommendation Emosi => m_recommendationList.Single(x => x.Dimensi == "Kestabilan Emosi" && x.NilaiMin <= this.ProfilPersonaliti.Emosi && this.ProfilPersonaliti.Emosi <= x.NilaiMax);

        [JsonIgnore]
        public PpkpRecommendation GayaBekerja => m_recommendationList.Single(x => x.Dimensi == "Cara Gaya Bekerja" && x.NilaiMin <= this.ProfilPersonaliti.GayaBekerja && this.ProfilPersonaliti.GayaBekerja <= x.NilaiMax);
        [JsonIgnore]
        public PpkpRecommendation Pemikiran => m_recommendationList.Single(x => x.Dimensi == "Cara Gaya Pemikiran" && x.NilaiMin <= this.ProfilPersonaliti.Pemikiran && this.ProfilPersonaliti.Pemikiran <= x.NilaiMax);
        [JsonIgnore]
        public PpkpRecommendation Interpersonal => m_recommendationList.Single(x => x.Dimensi == "Hubungan Interpersonal" && x.NilaiMin <= this.ProfilPersonaliti.Interpersonal && this.ProfilPersonaliti.Interpersonal <= x.NilaiMax);
        [JsonIgnore]
        public PpkpRecommendation Peribadi => m_recommendationList.Single(x => x.Dimensi == "Keperibadian" && x.NilaiMin <= this.ProfilPersonaliti.Peribadi && this.ProfilPersonaliti.Peribadi <= x.NilaiMax);

        public ProfilPersonalitiDimensiUmumType ProfilPersonaliti { get; }
        public ProfilKepimpinanType ProfilKepimpinan { get; }
        public KesesuaianPenempatanType KesesuaianPenempatan { get; }
        public SkorKestabilanEmosiType SkorKestabilanEmosi { get; }

        public int A1 => this.Sesi.JawapanCollection.Where(a => a.Trait == "A1").Sum(a => a.Nilai);
        public int A2 => this.Sesi.JawapanCollection.Where(a => a.Trait == "A2").Sum(a => a.Nilai);
        public int A3 => this.Sesi.JawapanCollection.Where(a => a.Trait == "A3").Sum(a => a.Nilai);
        public int A4 => this.Sesi.JawapanCollection.Where(a => a.Trait == "A4").Sum(a => a.Nilai);


        public int B1 => this.Sesi.JawapanCollection.Where(a => a.Trait == "B1").Sum(a => a.Nilai);
        public int B2 => this.Sesi.JawapanCollection.Where(a => a.Trait == "B2").Sum(a => a.Nilai);
        public int B3 => this.Sesi.JawapanCollection.Where(a => a.Trait == "B3").Sum(a => a.Nilai);
        public int B4 => this.Sesi.JawapanCollection.Where(a => a.Trait == "B4").Sum(a => a.Nilai);
        public int B5 => this.Sesi.JawapanCollection.Where(a => a.Trait == "B5").Sum(a => a.Nilai);

        public int C1 => this.Sesi.JawapanCollection.Where(a => a.Trait == "C1").Sum(a => a.Nilai);
        public int C2 => this.Sesi.JawapanCollection.Where(a => a.Trait == "C2").Sum(a => a.Nilai);
        public int C3 => this.Sesi.JawapanCollection.Where(a => a.Trait == "C3").Sum(a => a.Nilai);
        public int C4 => this.Sesi.JawapanCollection.Where(a => a.Trait == "C4").Sum(a => a.Nilai);


        public int D1 => this.Sesi.JawapanCollection.Where(a => a.Trait == "D1").Sum(a => a.Nilai);
        public int D2 => this.Sesi.JawapanCollection.Where(a => a.Trait == "D2").Sum(a => a.Nilai);
        public int D3 => this.Sesi.JawapanCollection.Where(a => a.Trait == "D3").Sum(a => a.Nilai);
        public int D4 => this.Sesi.JawapanCollection.Where(a => a.Trait == "D4").Sum(a => a.Nilai);
        public int D5 => this.Sesi.JawapanCollection.Where(a => a.Trait == "D5").Sum(a => a.Nilai);
        public int D6 => this.Sesi.JawapanCollection.Where(a => a.Trait == "D6").Sum(a => a.Nilai);


        public int E1 => this.Sesi.JawapanCollection.Where(a => a.Trait == "E1").Sum(a => a.Nilai);
        public int E2 => this.Sesi.JawapanCollection.Where(a => a.Trait == "E2").Sum(a => a.Nilai);
        public int E3 => this.Sesi.JawapanCollection.Where(a => a.Trait == "E3").Sum(a => a.Nilai);
        public int E4 => this.Sesi.JawapanCollection.Where(a => a.Trait == "E4").Sum(a => a.Nilai);
        public int E5 => this.Sesi.JawapanCollection.Where(a => a.Trait == "E5").Sum(a => a.Nilai);




    }
}
