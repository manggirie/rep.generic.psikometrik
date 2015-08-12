using System.Threading.Tasks;
using System.Web.Mvc;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_permohonan.Domain;
using Bespoke.epsikologi_ppkprecommendation.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_skorppkp.Domain;
using Bespoke.epsikologi_ujian.Domain;
using Bespoke.Sph.Domain;

namespace web.sph.App_Code
{
    public partial class PrintReportController
    {

        [Route("ppkp/profile/{id}")]
        public async Task<ActionResult> PrintPpkpProfile(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Pengguna>(x => x.MyKad == sesi.MyKad);

            var ujianTask = context.LoadOneAsync<Ujian>(x => x.Id == sesi.NamaUjian);
            var permohonanTask = context.LoadOneAsync<Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);
            var recommendationTask = context.LoadAsync(context.CreateQueryable<PpkpRecommendation>());
            var skorTask = context.LoadAsync(context.CreateQueryable<SkorPpkp>(), 1, 150, true);
            await Task.WhenAll(ujianTask, permohonanTask, recommendationTask, skorTask);

            var rlo = await recommendationTask;
            var skorLo = await skorTask;

            if (null == sesi)
                return HttpNotFound("Cannot find SesiUjian " + id);
            if (null == user)
                return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

            var vm = new PpkpTraitViewModel(sesi, rlo.ItemCollection.ToArray(), skorLo.ItemCollection.ToArray())
            {
                Permohonan = await permohonanTask,
                Ujian = await ujianTask,
                Pengguna = user
            };

            return View("ppkp.profile", vm);

        }
        [Route("ppkp/umum/{id}")]
        public async Task<ActionResult> PrintPpkpUmum(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Pengguna>(x => x.MyKad == sesi.MyKad);

            var ujianTask = context.LoadOneAsync<Ujian>(x => x.Id == sesi.NamaUjian);
            var permohonanTask = context.LoadOneAsync<Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);
            var recommendationTask = context.LoadAsync(context.CreateQueryable<PpkpRecommendation>());
            var skorTask = context.LoadAsync(context.CreateQueryable<SkorPpkp>(), 1, 150, true);
            await Task.WhenAll(ujianTask, permohonanTask, recommendationTask, skorTask);

            var rlo = await recommendationTask;
            var skorLo = await skorTask;

            if (null == sesi)
                return HttpNotFound("Cannot find SesiUjian " + id);
            if (null == user)
                return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

            var vm = new PpkpTraitViewModel(sesi, rlo.ItemCollection.ToArray(), skorLo.ItemCollection.ToArray())
            {
                Permohonan = await permohonanTask,
                Ujian = await ujianTask,
                Pengguna = user
            };

            return View("ppkp.umum", vm);

        }


        [Route("ppkp/khusus/{id}")]
        public async Task<ActionResult> PrintPpkpKhusus(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Pengguna>(x => x.MyKad == sesi.MyKad);

            var ujianTask = context.LoadOneAsync<Ujian>(x => x.Id == sesi.NamaUjian);
            var permohonanTask = context.LoadOneAsync<Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);
            var recommendationTask = context.LoadAsync(context.CreateQueryable<PpkpRecommendation>());
            var skorTask = context.LoadAsync(context.CreateQueryable<SkorPpkp>(), 1, 150, true);
            await Task.WhenAll(ujianTask, permohonanTask, recommendationTask, skorTask);

            var rlo = await recommendationTask;
            var skorLo = await skorTask;

            if (null == sesi)
                return HttpNotFound("Cannot find SesiUjian " + id);
            if (null == user)
                return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

            var vm = new PpkpTraitViewModel(sesi, rlo.ItemCollection.ToArray(), skorLo.ItemCollection.ToArray())
            {
                Permohonan = await permohonanTask,
                Ujian = await ujianTask,
                Pengguna = user
            };

            return View("ppkp.khusus", vm);

        }



    }
}
