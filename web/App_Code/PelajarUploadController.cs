using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bespoke.epsikologi_pendaftaranprogram.Domain;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_permohonan.Domain;
using Bespoke.Sph.Domain;
using OfficeOpenXml;

namespace web.sph.App_Code
{
    [RoutePrefix("pelajar")]
    [Authorize(Roles = "PengurusanPenggunaJabatan, developers")]
    public class PelajarUploadController : Controller
    {
        public PelajarUploadController()
        {
            ConfigHelper.RegisterDependencies();
        }

        [HttpGet]
        [Route("test")]
        public ActionResult Test()
        {
            return Content("test OK");
        }

        [HttpPost]
        [Route("process-file")]
        public async Task<ActionResult> ProcessFile(string id, string permohonanId)
        {
            if (string.IsNullOrEmpty(id)) return Json(new {success  =false, messge = "Store Id is not provided for the excel file"});
            var context = new SphDataContext();
            var store = ObjectBuilder.GetObject<IBinaryStore>();
            var doc = await store.GetContentAsync(id);
            var temp = Path.GetTempFileName() + ".xlsx";
            System.IO.File.WriteAllBytes(temp, doc.Content);

            var file = new FileInfo(temp);
            var excel = new ExcelPackage(file);
            var ws = excel.Workbook.Worksheets["Pelajar"];


            var permohonan = await context.LoadOneAsync<Permohonan>(x => x.Id == permohonanId);
            if (null == permohonan) return HttpNotFound("Cannot find any Permohonan with Id=" + permohonanId);

            var senaraiDaftar = new List<PendaftaranProgram>();
            var row = 2;
            var name = ws.Cells["A" + row].GetValue<string>();
            var mykad = ws.Cells["B" + row].GetValue<string>();
            while (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(mykad))
            {
                var student = new Pengguna
                {
                    Id = mykad,
                    Nama = name,
                    MyKad =mykad,
                    Jantina = ws.Cells["C" + row].GetValue<string>(),
                    Warganegara = ws.Cells["D" + row].GetValue<string>(),
                    KumpulanUmur = ws.Cells["E" + row].GetValue<string>(),
                    Emel = ws.Cells["F" + row].GetValue<string>(),
                    Emel2 = ws.Cells["G" + row].GetValue<string>(),
                    Pelajar = new Pelajar
                    {
                        Minat = ws.Cells["H" + row].GetValue<string>(),
                        BidangPengajian = ws.Cells["I" + row].GetValue<string>(),
                        LuarNegara = ws.Cells["J" + row].GetValue<string>() == "Ya",
                        TahapPendidikan = ws.Cells["K" + row].GetValue<string>()

                    }
                };

                var exist = await context.GetAnyAsync<Pengguna>(x => x.MyKad == student.MyKad);
                var did = Guid.NewGuid().ToString();
                var daftar = new PendaftaranProgram
                {
                    NamaProgram = permohonan.NamaProgram,
                    MyKad = student.MyKad,
                    TarikhDaftar = DateTime.Now,
                    NamaPengguna = student.Nama,
                    NoPermohonan = permohonan.PermohonanNo,
                    PendaftaranNo = did,
                    PermohonanId = permohonanId,
                    Id = did,
                    WebId = did

                };
                senaraiDaftar.Add(daftar);
                using (var session = context.OpenSession())
                {
                    if (!exist)
                        session.Attach(student);
                    session.Attach(daftar);
                    await session.SubmitChanges("import-pelajar");
                }

                //increment
                row++;
                name = ws.Cells["A" + row].GetValue<string>();
                mykad = ws.Cells["B" + row].GetValue<string>();
            }



            return Json(new { success = true, list = senaraiDaftar, status = "OK" });
        }

    }

}
