using System.Web.Mvc;
using Bespoke.Sph.Domain;
using System.Threading.Tasks;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_permohonan.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_ujian.Domain;


namespace web.sph.App_Code
{
    public partial class PrintReportController
    {

        [Route("indikator/ukbp/{id}")]
        public async Task<ActionResult> IndikatorUkbp(string id)
        {
            var context = new SphDataContext();
           
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);
            var user = await context.LoadOneAsync<Pengguna>(x => x.MyKad == sesi.MyKad);

            var ujianTask = context.LoadOneAsync<Ujian>(x => x.Id == sesi.NamaUjian);
            var permohonanTask = context.LoadOneAsync<Permohonan>(x => x.PermohonanNo == sesi.NamaProgram);
            await Task.WhenAll(ujianTask, permohonanTask);



            if (null == sesi)
                return HttpNotFound("Cannot find SesiUjian " + id);
            if (null == user)
                return HttpNotFound("Cannot find user with MyKad " + sesi.MyKad);

            SesiUjian sesiA, sesiB;
            if (sesi.NamaUjian == "UKBP-A")
            {
                sesiA = sesi;
                sesiB = await context.LoadOneAsync<SesiUjian>(x => x.NamaUjian == "UKBP-B" && x.NamaProgram == sesi.NamaProgram &&  x.MyKad == sesi.MyKad);
            }
            else
            {
                sesiB = sesi;
                sesiA = await context.LoadOneAsync<SesiUjian>(x => x.NamaUjian == "UKBP-A" && x.NamaProgram == sesi.NamaProgram && x.MyKad == sesi.MyKad);
            }

            var vm = new UkbpTraitViewModel(sesiA, sesiB)
            {
                Permohonan = await permohonanTask,
                Ujian = await ujianTask,
                Pengguna = user
            };

           


            return View("Indikator-UKBP", vm);

        }

        


    }
}
