using System.Web.Mvc;
using Bespoke.Sph.Domain;
using System.Threading.Tasks;
using Bespoke.epsikologi_ipurecommendation.Domain;
using Bespoke.epsikologi_skoripu.Domain;
using Bespoke.epsikologi_ipupercentilenorms.Domain;


namespace web.sph.App_Code
{
    public partial class PrintReportController
    {

        [Route("trait/ppkp/{id}")]
        public async Task<ActionResult> PrintTraitPpkp(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Bespoke.epsikologi_pengguna.Domain.Pengguna>(x => x.MyKad == sesi.MyKad);

            var ujianTask = context.LoadOneAsync<Bespoke.epsikologi_ujian.Domain.Ujian>(x => x.Id == sesi.NamaUjian);
            var permohonanTask = context.LoadOneAsync<Bespoke.epsikologi_permohonan.Domain.Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);
            await Task.WhenAll(ujianTask, permohonanTask);

            if (null == sesi)
                return HttpNotFound("Cannot find SesiUjian " + id);
            if (null == user)
                return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

            var vm = new PpkpTraitViewModel(sesi)
            {
                Permohonan = await permohonanTask,
                Ujian = await ujianTask,
                Pengguna = user
            };



            return Pdf(vm, "Trait-Ppkp", x => x);

        }



    }
}
