using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bespoke.Sph.Domain;
using System.Threading.Tasks;
using System.Web;
using Bespoke.epsikologi_ipurecommendation.Domain;
using Bespoke.epsikologi_skoripu.Domain;
using Bespoke.epsikologi_ipupercentilenorms.Domain;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_permohonan.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_soalan.Domain;
using Bespoke.epsikologi_ujian.Domain;
using OfficeOpenXml;


namespace web.sph.App_Code
{
    public partial class PrintReportController
    {


        [Route("xls/sesiujian")]
        public async Task<ActionResult> ExportSesiUjianByYearToExcel(ProgramReportModel model)
        {
            var year = model.Tahun;
            if (0 == year)
            {
                year = DateTime.Now.Year;
            }
            var temp = Path.GetTempFileName() + ".xlsx";
            System.IO.File.Copy(Server.MapPath("~/App_Data/template/laporan-sesi-ujian.xlsx"), temp, true);


            var file = new FileInfo(temp);
            var excel = new ExcelPackage(file);
            var ws = excel.Workbook.Worksheets["IPU"];

            var context = new SphDataContext();
            //var program = await context.GetScalarAsync<SesiUjian, string>(x => x.TarikhUjian.Value.Year == year, x => x.NamaProgram);

            var query = context.CreateQueryable<SesiUjian>()
                .Where(s => s.NamaProgram == model.Program)
                .Where(s => s.Status == "Diambil")
                .Where(s => s.NamaUjian == model.Ujian);
            var sesiLo = await context.LoadAsync(query, 1, 200, true);
            var sesi = sesiLo.ItemCollection;

            while (sesiLo.HasNextPage)
            {
                sesiLo = await context.LoadAsync(query, sesiLo.CurrentPage + 1, 200, true);
                sesi.AddRange(sesiLo.ItemCollection);
            }

            var soalanQuery = context.CreateQueryable<Soalan>()
                                   .Where(s => s.NamaUjian ==model.Ujian);
            var soalanLo = await context.LoadAsync(soalanQuery, 1, 200, true);
            var soalans = soalanLo.ItemCollection.OrderBy(s => s.Susunan);
            
            var column1 = 4;
            foreach (var soalan in soalans)
            {
                column1++;
                ws.Cells[2, column1].Value = "Q" + soalan.Susunan;
            }
            var row = 2;
            foreach (var s in sesi)
            {
                row++;
                ws.InsertRow(row, 1, row);
                ws.Cells[row, 1].Value = s.NamaPengguna;
                ws.Cells[row, 2].Value = model.Ujian;
                ws.Cells[row, 3].Value = s.MyKad;
                ws.Cells[row, 4].Value = $"{s.TarikhUjian:dd/MM/yyyy HH:mm}";
                var column = 4;
                foreach (var t in soalans)
                {
                    column += 1;
                    var jawapan = s.JawapanCollection.FirstOrDefault(j => j.SoalanNo == t.SoalanNo);
                    var score = jawapan != null ? jawapan.Nilai : 0;
                    ws.Cells[row, column].Value = score;

                }

            }
            excel.Save();
            excel.Dispose();
            return Json(new { success = true, path = Path.GetFileName(temp) });
            //return File(temp, MimeMapping.GetMimeMapping(".xlsx"), "Laporan Markah dan Percentile IPU.xlsx");
        }


    }
}
