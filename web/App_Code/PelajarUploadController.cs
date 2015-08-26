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
            if (string.IsNullOrEmpty(id)) return Json(new { success = false, messge = "Store Id is not provided for the excel file" });
            var context = new SphDataContext();
            var store = ObjectBuilder.GetObject<IBinaryStore>();
            var doc = await store.GetContentAsync(id);
            var temp = Path.GetTempFileName() + ".xlsx";
            System.IO.File.WriteAllBytes(temp, doc.Content);

            var file = new FileInfo(temp);
            var excel = new ExcelPackage(file);
            var ws = excel.Workbook.Worksheets["Pelajar"];
            if(null == ws)
                throw new ArgumentException("Cannot open Worksheet Pelajar in " + doc.FileName );


            var permohonan = await context.LoadOneAsync<Permohonan>(x => x.Id == permohonanId);
            if (null == permohonan) return HttpNotFound("Cannot find any Permohonan with Id=" + permohonanId);

            var senaraiDaftar = new List<PendaftaranProgram>();
            var row = 2;
            var name = ws.Cells["A" + row].GetValue<string>();
            var mykad = ws.Cells["B" + row].GetValue<string>();
            var hasRow = !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(mykad);

            while (hasRow)
            {
                var student = new Pengguna
                {
                    Id = mykad,
                    Nama = name,
                    MyKad = mykad,
                    Jantina = ws.Cells["C" + row].GetValue<string>(),
                    Warganegara = "Malaysia",
                    KumpulanUmur = "NA",
                    Emel = ws.Cells["G" + row].GetValue<string>(),
                    Emel2 = null,
                    Pelajar = new Pelajar
                    {
                        Minat = null,
                        BidangPengajian = ws.Cells["D" + row].GetValue<string>(),
                        LuarNegara = ws.Cells["F" + row].GetValue<string>() == "Luar Negara",
                        TahapPendidikan = ws.Cells["E" + row].GetValue<string>()

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
                hasRow = !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(mykad);
            }

            // TODO : verify the user name and email for each record


            return Json(new { success = true, list = senaraiDaftar, status = "OK" });
        }

    }

}
