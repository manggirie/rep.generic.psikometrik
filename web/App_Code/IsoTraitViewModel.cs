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
        /*
Ukuran Stres Kerja
Life Balance
Stress Vulnerability
Bebanan Hidup
Kebimbangan
Kebosanan & Kesunyian
Kekecewaan
Urus Masa
Reaksi Stres

            IsoRecommendation count by Tret
Bebanan dan Stres
Berisiko Menghadapi Stres
Kebimbangan
Kehidupan Seimbang
Kekecewaan
Keperluan Pengurusan Masa
Reaksi Terhadap Stres
Tahap Stres
Ukuran Stres
Kebosanan dan Kesunyian


    */
        public IsoRecommendation PuncaStresKebimbangan => this.GetRecommendation("A", "Kebimbangan");
        public IsoRecommendation PuncaStresKekecewaan => this.GetRecommendation("A", "Kekecewaan");
        public IsoRecommendation PuncaStresBebananDanStres => this.GetRecommendation("A", "Bebanan Hidup", "Bebanan dan Stres");
        public IsoRecommendation PuncaStresKebosananDanKesunyian => this.GetRecommendation("A", "Kebosanan & Kesunyian", "Kebosanan dan Kesunyian");

        public IsoRecommendation TindakBalasStresUkuranStres => this.GetRecommendation("B", "Ukuran Stres Kerja", "Ukuran Stres");
        public IsoRecommendation TindakBalasStresReaksiTerhadapStres => this.GetRecommendation("B", "Reaksi Stres", "Reaksi Terhadap Stres");

        public IsoRecommendation OrientasiTerhadapStresKehidupanSeimbang => this.GetRecommendation("C", "Life Balance", "Kehidupan Seimbang");
        public IsoRecommendation OrientasiTerhadapStresKeperluanPengurusanMasa => this.GetRecommendation("C", "Urus Masa", "Keperluan Pengurusan Masa");

        public IsoRecommendation KecenderunganStresBerisikoMenghadapiStres => this.GetRecommendation("D", "Stress Vulnerability", "Berisiko Menghadapi Stres");

        private IsoRecommendation GetRecommendation(string tret, string categorySoalan, string tretRecommendation = null)
        {
            var soalans = m_questions.Where(x => x.Kategori == categorySoalan).Select(x => x.SoalanNo).ToArray();
            var score = this.Sesi.JawapanCollection.Where(x => x.Trait == tret && soalans.Contains(x.SoalanNo))
                .Sum(x => x.Nilai);
            if (string.IsNullOrWhiteSpace(tretRecommendation))
                tretRecommendation = categorySoalan;
            return m_list.Single(x => x.NilaiMin <= score && score <= x.NilaiMax && x.Tret == tretRecommendation);
        }

    }
}
