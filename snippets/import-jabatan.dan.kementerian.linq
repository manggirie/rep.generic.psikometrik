<Query Kind="Statements">
  <Reference Relative="..\web\bin\domain.sph.dll">C:\project\rep.generic.psikometrik\web\bin\domain.sph.dll</Reference>
  <Reference Relative="..\lib\EPPlus.dll">C:\project\rep.generic.psikometrik\lib\EPPlus.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.Jabatan.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.Jabatan.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.Kementerian.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.Kementerian.dll</Reference>
  <Reference Relative="..\web\bin\Newtonsoft.Json.dll">C:\project\rep.generic.psikometrik\web\bin\Newtonsoft.Json.dll</Reference>
  <Namespace>Bespoke.Sph.Domain</Namespace>
  <Namespace>OfficeOpenXml</Namespace>
</Query>

var excel = new ExcelPackage(new FileInfo(@"C:\project\rep.generic.psikometrik\docs\Senarai agensi mengikut Kementerian.xlsx"));
var ws = excel.Workbook.Worksheets["Sheet1"];

var departments = from l in Enumerable.Range(5,725)
				  let ministry = ws.Cells["A" + l].Value.ToString()
				  let ka  = ws.Cells["B" + l].Value.ToString()
				  let agensi  = ws.Cells["C" + l].Value.ToString()
				  let id = Bespoke.Sph.Domain.Strings.ToIdFormat(agensi)+(ministry.Contains("PENTADBIRAN KERAJAAN NEGERI") ? ("-" + ministry.Replace("PENTADBIRAN KERAJAAN NEGERI", "").Trim()) : "").ToLowerInvariant()
				  let id0 = id.Length > 50 ? id.Substring(0, 50) : id
				  let id2 = id0.Replace("--", "-")
				  let ak = ka.Replace("PERKHIDMATAN AWAM PERSEKUTUAN", "PAP")
				  .Replace("BADAN BERKANUN PERSEKUTUAN", "BBP")
				  .Replace("PIHAK BERKUASA TEMPATAN", "PBT")
				  .Replace("PERKHIDMATAN AWAM NEGERI", "PAN")
				  .Replace("BADAN BERKANUN NEGERI", "BBN")

				  select new Bespoke.epsikologi_jabatan.Domain.Jabatan
				  {
				  	Kementerian = ministry,
					AgensiKumpulan = ak,
					NamaJabatan = agensi,
					Id = id2,
					JabatanNo = id2,
					CreatedBy = "erymuzuan",
					CreatedDate = DateTime.Now,
					ChangedBy = "admin",
					ChangedDate = DateTime.Now,
					WebId = Guid.NewGuid().ToString(),
					AgensiNo = "NA"

				  };
departments.GroupBy(d => d.Id).Select(d => new { d.Key, Count = d.Count() }).Where(d => d.Count > 1).Dump();
departments.GroupBy(d => d.Kementerian).Select(d => new { d.Key, Count = d.Count() }).OrderByDescending(d => d.Count).Dump();

departments.ToList().ForEach(d => File.WriteAllText($@"C:\project\rep.generic.psikometrik\sources\Jabatan\{d.Id}.json", Bespoke.Sph.Domain.JsonSerializerService.ToJsonString(d, true)));

var ministries = from d in departments
				 let id = Bespoke.Sph.Domain.Strings.ToIdFormat(d.Kementerian)
				 let id2 = id.Length > 50 ? id.Substring(0, 50) : id
				 let state = d.Kementerian.Contains("PENTADBIRAN KERAJAAN NEGERI") ? d.Kementerian.Replace("PENTADBIRAN KERAJAAN NEGERI","").Trim() : ""
				 select new Bespoke.epsikologi_kementerian.Domain.Kementerian
				 {
				 	NamaKementerian = d.Kementerian,
					Abbreviation = d.NamaJabatan,
					KementerianNo = id2,
					 KumpulanAgensi = d.AgensiKumpulan,
					 Id = id2,
					 CreatedBy = "erymuzuan",
					 CreatedDate = DateTime.Now,
					 ChangedBy = "admin",
					 ChangedDate = DateTime.Now,
					 WebId = Guid.NewGuid().ToString(),
					 Negeri = state

				 };
ministries.Dump();

ministries.ToList().ForEach(d => File.WriteAllText($@"C:\project\rep.generic.psikometrik\sources\Kementerian\{d.Id}.json", Bespoke.Sph.Domain.JsonSerializerService.ToJsonString(d,true)));

