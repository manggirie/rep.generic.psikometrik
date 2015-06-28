using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace epsikologi.Integrations.Transforms
{
    public class SupatJabatanToKementerian
    {
        public Task<Bespoke.epsikologi_kementerian.Domain.Kementerian> TransformAsync(epsikologi.Adapters.dbo.MsSqlSupat.Tbl_KodJabatan item)
        {
            var dest = new Bespoke.epsikologi_kementerian.Domain.Kementerian();


            dest.KementerianNo = item.Kod_Kementerian;
            dest.NamaKementerian = item.Jabatan;
            dest.Negeri = item.Singkatan;
            dest.KumpulanAgensi = item.Alamat_Jabatan2;
            dest.CreatedBy = item.No_Fax;
            dest.Id = item.Kod_Jabatan;
            dest.CreatedDate = DateTime.Parse("2015-06-06");


            return Task.FromResult(dest);
        }

        //TODO : return the list of destinations objects
    }
}
