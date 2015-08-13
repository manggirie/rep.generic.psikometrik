using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bespoke.epsikologi_iprecommendation.Domain;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_permohonan.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_ujian.Domain;
using Bespoke.Sph.Domain;

namespace web.sph.App_Code
{
    public partial class PrintReportController
    {
        
        [Route("trait/ip/{id}")]
        public async Task<ActionResult> PrintPdfTraitIp(string id, int tryCount = 0)
        {
            var vm = await GetIpTraitViewModelAsync(id);
            const string VIEW_NAME = "Trait-IP";
            return Pdf(VIEW_NAME, vm);

        }
        
        private async Task<IpTraitViewModel> GetIpTraitViewModelAsync(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Pengguna>(x => x.MyKad == sesi.MyKad);
            var ujian = await context.LoadOneAsync<Ujian>(x => x.Id == sesi.NamaUjian);
            var permohonan = await context.LoadOneAsync<Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);

            if (null == sesi || null == user)
                return null;

            var vm = new IpTraitViewModel(sesi, user)
            {
                Permohonan = permohonan,
                Ujian = ujian
            };

            vm.Recommendation = await context.LoadOneAsync<IpRecommendation>(x => x.Skor == vm.Result);
            if (null == vm.Recommendation)
                throw new InvalidOperationException("Cannot find IpRecommendation for " + vm.Result);

            return vm;
        }


        [Route("indikator/ip/{id}")]
        public async Task<ActionResult> PrintSesiUjianIndikator(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);

            if (null == sesi)
                return HttpNotFound("Cannot find SesiUjian " + id);

            return View("Indikator-" + sesi.NamaUjian, sesi);
        }


    }
}
