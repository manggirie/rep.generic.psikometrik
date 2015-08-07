using System.Threading.Tasks;
using System.Web.Mvc;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_permohonan.Domain;
using Bespoke.epsikologi_ppkprecommendation.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_ujian.Domain;
using Bespoke.Sph.Domain;

namespace web.sph.App_Code
{
    public partial class PrintReportController
    {

        [Route("trait/ppkp/{id}")]
        public async Task<ActionResult> PrintTraitPpkp(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Pengguna>(x => x.MyKad == sesi.MyKad);

            var ujianTask = context.LoadOneAsync<Ujian>(x => x.Id == sesi.NamaUjian);
            var permohonanTask = context.LoadOneAsync<Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);
            var recommendationTask = context.LoadAsync(context.CreateQueryable<PpkpRecommendation>());
            await Task.WhenAll(ujianTask, permohonanTask, recommendationTask);

            var rlo = await recommendationTask;

            if (null == sesi)
                return HttpNotFound("Cannot find SesiUjian " + id);
            if (null == user)
                return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

            var vm = new PpkpTraitViewModel(sesi, rlo.ItemCollection.ToArray())
            {
                Permohonan = await permohonanTask,
                Ujian = await ujianTask,
                Pengguna = user
            };

            return View("Trait-Ppkp",vm);

        }



    }
}
