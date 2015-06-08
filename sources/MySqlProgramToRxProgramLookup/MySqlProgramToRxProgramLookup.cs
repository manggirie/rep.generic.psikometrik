using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace epsikologi.Integrations.Transforms
{
    public class MySqlProgramToRxProgramLookup
    {
        public Task<Bespoke.epsikologi_programlookup.Domain.ProgramLookup> TransformAsync(epsikologi.Adapters.epsikometrik.MySqlEpsikometrik.program item)
        {
            var dest = new Bespoke.epsikologi_programlookup.Domain.ProgramLookup();

            //:FormattingFunctoid:f7c10c09-54d0-4145-e3e8-b5e2567a58ae
            var format0 = "{0:00000}";
            var value0 = item.ID;


            dest.ProgramNo = string.Format("{0:00000}", value0);
            dest.Id = string.Format("{0:00000}", value0);
            dest.NamaProgram = item.nama_program;
            dest.CreatedBy = item.pengguna;
            dest.CreatedDate = item.tarikh_wujud;
            dest.ChangedDate = item.tarikh_kemaskini;
            dest.ChangedBy = item.pengguna;
            dest.WebId = item.WebId;


            return Task.FromResult(dest);
        }

        //TODO : return the list of destinations objects
    }
}
