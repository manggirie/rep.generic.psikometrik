using System.Linq;
using Bespoke.epsikologi_sesiujian.Domain;

namespace web.sph.App_Code
{
    public class PpkpTraitViewModel : LaporanViewModel
    {
        public class ProfilPersonalitiDimensiUmumType
        {
            private readonly PpkpTraitViewModel m_vm;
            public ProfilPersonalitiDimensiUmumType(PpkpTraitViewModel vm)
            {
                m_vm = vm;
            }

            public int KestabilanEmosi => m_vm.A1 + m_vm.A2 + m_vm.A3 + m_vm.A4;
            public int CaraGayaBekerja => m_vm.B1 + m_vm.B2 + m_vm.B3 + m_vm.B4 + m_vm.B5;
            public int CaraGayaPemikiran => m_vm.C1 + m_vm.C2 + m_vm.C3 + m_vm.C4;
            public int HubunganInterpersonal => m_vm.D1 + m_vm.D2 + m_vm.D3 + m_vm.D4 + m_vm.D5 + m_vm.D6;
            public int Keperibadian => m_vm.E1 + m_vm.E2 + m_vm.E3 + m_vm.E4 + m_vm.E5;

        }
        public PpkpTraitViewModel(SesiUjian sesi)
        {
            Sesi = sesi;
            this.ProfilPersonalitiDimensiUmum = new ProfilPersonalitiDimensiUmumType(this);
        }
        
        public ProfilPersonalitiDimensiUmumType ProfilPersonalitiDimensiUmum{ get;}



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
