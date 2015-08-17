using Bespoke.epsikologi_sesiujian.Domain;

namespace web.sph.App_Code
{
    public class UkbpTraitViewModel : LaporanViewModel
    {
        private readonly SesiUjian m_sesiA;
        private readonly SesiUjian m_sesiB;

        public UkbpTraitViewModel(SesiUjian sesiA, SesiUjian sesiB)
        {
            m_sesiA = sesiA;
            m_sesiB = sesiB;
            this.Sesi = sesiA;
        }
    }
}