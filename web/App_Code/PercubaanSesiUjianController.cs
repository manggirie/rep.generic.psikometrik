using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_percubaansesi.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.Sph.Domain;

namespace web.sph.App_Code
{
    [RoutePrefix("sesi-ujian")]
    [Authorize]
    public class PercubaanSesiUjianController : Controller
    {
        public PercubaanSesiUjianController()
        {
            ConfigHelper.RegisterDependencies();
        }

        [HttpPost]
        [Route("timeout/{id}")]
        public async Task<ActionResult> TimeOut(string id)
        {
            var context = new SphDataContext();
            var user = await context.LoadOneAsync<Pengguna>(x => x.MyKad == User.Identity.Name);
            var sesi = await context.LoadOneAsync<SesiUjian>(x => x.Id == id);

            var name = user == null ? "" : user.Nama;
            var setting = (await context.LoadOneAsync<PercubaanSesi>(x => x.MyKad == User.Identity.Name && x.SesiUjianId == id))
                  ?? new PercubaanSesi {
                          No = Guid.NewGuid().ToString(),
                          MyKad = User.Identity.Name,
                          SesiUjianId = id,
                          Bilangan = 0,
                          NamaUjian = sesi.NamaUjian,
                          Program = sesi.NamaProgram,
                          NamaPengguna = name,
                          Tarikh = DateTime.Now,
                          Id = Guid.NewGuid().ToString()
                        };


            setting.Bilangan += 1;
            using(var session = context.OpenSession())
            {
              session.Attach(setting);
              await session.SubmitChanges();
            }


            return Json(setting);
        }
    }
}
