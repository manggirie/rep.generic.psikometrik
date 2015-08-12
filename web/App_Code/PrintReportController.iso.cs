using System.Threading.Tasks;
using System.Web.Mvc;
using Bespoke.epsikologi_isorecommendation.Domain;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_permohonan.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_soalan.Domain;
using Bespoke.epsikologi_ujian.Domain;
using Bespoke.Sph.Domain;

namespace web.sph.App_Code
{
    public partial class PrintReportController
    {

        [Route("trait/iso/{id}")]
        public async Task<ActionResult> PrintTraitIso(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Pengguna>(x => x.MyKad == sesi.MyKad);

            var ujianTask = context.LoadOneAsync<Ujian>(x => x.Id == sesi.NamaUjian);
            var permohonanTask = context.LoadOneAsync<Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);
            var recommendationTask = context.LoadAsync(context.CreateQueryable<IsoRecommendation>());
            var soalanTask = context.LoadAsync(context.CreateQueryable<Soalan>(), 1, 120, true);
            await Task.WhenAll(ujianTask, permohonanTask, recommendationTask, soalanTask);

            var rlo = await recommendationTask;
            var soalanLo = await soalanTask;

            if (null == sesi)
                return HttpNotFound("Cannot find SesiUjian " + id);
            if (null == user)
                return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

            var vm = new IsoTraitViewModel(sesi, user, rlo.ItemCollection, soalanLo.ItemCollection)
            {
                Permohonan = await permohonanTask,
                Ujian = await ujianTask,
                Pengguna = user
            };

            return View("Trait-Iso", vm);

        }

        [Route("indikator/iso/{id}")]
        public async Task<ActionResult> PrintIndikatorIso(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Pengguna>(x => x.MyKad == sesi.MyKad);

            var ujianTask = context.LoadOneAsync<Ujian>(x => x.Id == sesi.NamaUjian);
            var permohonanTask = context.LoadOneAsync<Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);
            var recommendationTask = context.LoadAsync(context.CreateQueryable<IsoRecommendation>());
            var soalanTask = context.LoadAsync(context.CreateQueryable<Soalan>(), 1, 120, true);
            await Task.WhenAll(ujianTask, permohonanTask, recommendationTask, soalanTask);

            var rlo = await recommendationTask;
            var soalanLo = await soalanTask;

            if (null == sesi)
                return HttpNotFound("Cannot find SesiUjian " + id);
            if (null == user)
                return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

            var vm = new IsoTraitViewModel(sesi,user, rlo.ItemCollection, soalanLo.ItemCollection)
            {
                Permohonan = await permohonanTask,
                Ujian = await ujianTask,
                Pengguna = user
            };

            return View("Indikator-Iso", vm);

        }



    }
}
