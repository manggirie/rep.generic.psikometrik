<Query Kind="Statements">
  <Connection>
    <ID>1057a62e-59d6-42f4-92fe-c1bdad65fdf2</ID>
    <Persist>true</Persist>
    <Server>(localdb)\Projects</Server>
    <Database>epsikologi</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference Relative="..\web\bin\domain.sph.dll">C:\project\rep.generic.psikometrik\web\bin\domain.sph.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.PendaftaranProgram.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.PendaftaranProgram.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.Permohonan.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.Permohonan.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.SesiUjian.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.SesiUjian.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.Soalan.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.Soalan.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.Ujian.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.Ujian.dll</Reference>
  <Reference Relative="..\web\bin\Newtonsoft.Json.dll">C:\project\rep.generic.psikometrik\web\bin\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Bespoke.Sph.Domain</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

const string ujian = "UKBP-B";

var sesi = new Bespoke.epsikologi_sesiujian.Domain.SesiUjian
{
	Id = Guid.NewGuid().ToString(),
	NamaPengguna = "CikguGerik2",
	MyKad = "CikguGerik2",
	ChangedBy = "admin",
	ChangedDate = DateTime.Now,
	CreatedBy = "admin",
	CreatedDate = DateTime.Now,
	TarikhMulaProgram = DateTime.Today.AddDays(-1),
	TarikhTamatProgram = DateTime.Today.AddDays(5),
	NamaUjian = ujian,
	NamaProgram = "BTSA UJIAN PSIKOLOGI /1/1/2015",
	SesiNo = Guid.NewGuid().ToString(),
	Status = "Belum Ambil",
	WebId = Guid.NewGuid().ToString()

};

var item = new SesiUjian
{
	ChangedBy = sesi.ChangedBy,
	ChangedDate = sesi.ChangedDate,
	CreatedBy = sesi.CreatedBy,
	CreatedDate = sesi.CreatedDate,
	Id = sesi.Id,
	Json = sesi.ToJsonString(true),
	MyKad = sesi.MyKad,
	NamaPengguna = sesi.NamaPengguna,
	NamaProgram = sesi.NamaProgram,
	NamaUjian = sesi.NamaUjian,
	Status = sesi.Status,
	TarikhMulaProgram = sesi.TarikhMulaProgram,
	TarikhTamatProgram = sesi.TarikhTamatProgram
};
SesiUjians.InsertOnSubmit(item);
SubmitChanges();

using (var client = new HttpClient() { BaseAddress = new Uri("http://localhost:9200")})
{
	var json = Newtonsoft.Json.JsonConvert.SerializeObject(sesi);
	var content = new StringContent(json);
	var response = await client.PutAsync("epsikologi/sesiujian/" + sesi.Id, content);
	Console.WriteLine(response.StatusCode);
}