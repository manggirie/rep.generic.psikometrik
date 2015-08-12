using System.Collections.Generic;
using System.Linq;
using Bespoke.epsikologi_isorecommendation.Domain;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_soalan.Domain;

// ReSharper disable InconsistentNaming

namespace web.sph.App_Code
{
    public class IsoTraitViewModel : LaporanViewModel
    {
        private readonly IEnumerable<IsoRecommendation> m_list;
        private readonly IEnumerable<Soalan> m_questions;

        public IsoTraitViewModel(SesiUjian sesi, Pengguna pengguna, IEnumerable<IsoRecommendation> list, IEnumerable<Soalan> questions)
        {
            m_list = list;
            m_questions = questions;
            this.Pengguna = pengguna;
            this.Sesi = sesi;


        }

        public IsoRecommendation Recommendation { get; set; }
        public int A => this.Sesi.JawapanCollection.Where(x => x.Trait == "A").Sum(x => x.Nilai);
        public int B => this.Sesi.JawapanCollection.Where(x => x.Trait == "B").Sum(x => x.Nilai);
        public int C => this.Sesi.JawapanCollection.Where(x => x.Trait == "C").Sum(x => x.Nilai);
        public int D => this.Sesi.JawapanCollection.Where(x => x.Trait == "D").Sum(x => x.Nilai);

        public IsoRecommendation AndaDanStresTahapStres
            => m_list.Single(x => x.NilaiMin <= this.A && this.A <= x.NilaiMax && x.Dimensi == "Anda dan Stres");

        public IsoRecommendation PuncaStresKebimbangan
        {
            get
            {
                var soalans = m_questions.Where(x => x.Kategori == "Kebimbangan").Select(x => x.SoalanNo).ToArray();
                var score = this.Sesi.JawapanCollection.Where(x => x.Trait == "A" && soalans.Contains(x.SoalanNo))
                    .Sum(x => x.Nilai);

                return m_list.Single(x => x.NilaiMin <= score && score <= x.NilaiMax && x.Tret == "Kebimbangan");
            }
        }
        public IsoRecommendation PuncaStresKekecewaan
        {
            get
            {
                var soalans = m_questions.Where(x => x.Kategori == "Kekecewaan").Select(x => x.SoalanNo).ToArray();
                var score = this.Sesi.JawapanCollection.Where(x => x.Trait == "A" && soalans.Contains(x.SoalanNo))
                    .Sum(x => x.Nilai);

                return m_list.Single(x => x.NilaiMin <= score && score <= x.NilaiMax && x.Tret == "Kekecewaan");
            }
        }
        public IsoRecommendation PuncaStresBebananDanStres
        {
            get
            {
                var soalans = m_questions.Where(x => x.Kategori == "Bebanan dan Stres").Select(x => x.SoalanNo).ToArray();
                var score = this.Sesi.JawapanCollection.Where(x => x.Trait == "A" && soalans.Contains(x.SoalanNo))
                    .Sum(x => x.Nilai);

                return m_list.Single(x => x.NilaiMin <= score && score <= x.NilaiMax && x.Tret == "Bebanan dan Stres");
            }
        }

    }
}
