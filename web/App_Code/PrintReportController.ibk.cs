using System.Threading.Tasks;
using System.Web.Mvc;
using Bespoke.epsikologi_ibkkodkerjaya.Domain;
using Bespoke.epsikologi_ibkrecommendation.Domain;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_permohonan.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_ujian.Domain;
using Bespoke.Sph.Domain;

namespace web.sph.App_Code
{
    public partial class PrintReportController
    {
        [Route("trait/ibk/{id}")]
        public async Task<ActionResult> PrintIbkTrait(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Pengguna>(x => x.MyKad == sesi.MyKad);
            var ujian = await context.LoadOneAsync<Ujian>(x => x.Id == sesi.NamaUjian);
            var permohonan = await context.LoadOneAsync<Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);

            if (null == sesi)
                return HttpNotFound("Cannot find SesiUjian " + id);

            var vm = new IbkTraitViewModel(sesi)
            {
                Pengguna = user,
                Ujian = ujian,
                Permohonan = permohonan
            };


            var id1 = vm.KodKerjaya.Replace("/", "-");;
            var id2 = id1.Substring(4, 3) + "-" + id1.Substring(0, 3);


            //  if(vm.KodKerjaya != "xxx")
            //    throw new Exception("id1 = " + id1 + " and id2 = " + id2);

            vm.IbkRecommendation = await context.LoadOneAsync<IbkRecommendation>(
              x => x.Id== id1 || x.Id == id2);
            vm.IbkKodKerjaya = await context.LoadOneAsync<IbkKodKerjaya>(x => x.Id == vm.KodKerjaya.Substring(0, 1));


            var viewName = "Trait-Ibk";
            return View(viewName, vm);
        }


    }
}
