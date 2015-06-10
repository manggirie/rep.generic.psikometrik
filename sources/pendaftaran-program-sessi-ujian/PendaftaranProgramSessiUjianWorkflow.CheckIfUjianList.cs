using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;

namespace Bespoke.Sph.Workflows_PendaftaranProgramSessiUjian_0
{
    public partial class PendaftaranProgramSessiUjianWorkflow
    {



        [System.Diagnostics.Contracts.PureAttribute]
        private bool CheckIfUjianListAdaUjian()
        {
            var item = this;
            return this.UjianList != "";
        }

    }
}
