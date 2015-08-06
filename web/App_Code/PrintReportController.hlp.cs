using System.Threading.Tasks;
using System.Web.Mvc;
using Bespoke.epsikologi_hlprecomendation.Domain;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_skorhlp.Domain;
using Bespoke.Sph.Domain;

namespace web.sph.App_Code
{
    public partial class PrintReportController : Controller
    {

        [Route("indikator/hlp/{id}")]
        public async Task<ActionResult> HlpIndikator(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Pengguna>(x => x.MyKad == sesi.MyKad);

            var query = context.CreateQueryable<SkorHlp>();
            var lo = await context.LoadAsync(query, size: 1000);
            var scoreTables = lo.ItemCollection;


            var rq = context.CreateQueryable<HlpRecomendation>();
            var rlo = await context.LoadAsync(rq, size: 200);
            var recommendations = rlo.ItemCollection;


            if (null == sesi)
                return HttpNotFound("Cannot find SesiUjian " + id);
            if (null == user)
                return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

            var vm = new HlpTraitViewModel(sesi, user, scoreTables.ToArray(), recommendations.ToArray());
            var viewName = "Indikator-Hlp-" + user.Jantina;
            return Pdf(vm,viewName, x => View(viewName, x));
        }

        [Route("trait/hlp/{id}")]
        public async Task<ActionResult> PrintTraitForHlp(string id)
        {
            var context = new SphDataContext();
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Pengguna>(x => x.MyKad == sesi.MyKad);

            var query = context.CreateQueryable<SkorHlp>();
            var lo = await context.LoadAsync(query, size: 1000);
            var scoreTables = lo.ItemCollection;


            var rq = context.CreateQueryable<HlpRecomendation>();
            var rlo = await context.LoadAsync(rq, size: 200);
            var recommendations = rlo.ItemCollection;


            if (null == sesi)
                return HttpNotFound("Cannot find SesiUjian " + id);
            if (null == user)
                return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

            var vm = new HlpTraitViewModel(sesi, user, scoreTables.ToArray(), recommendations.ToArray());
            var viewName = "Trait-Hlp-" + user.Jantina;
            return Pdf(vm, viewName, x => View(viewName, x));
        }



    }
}
