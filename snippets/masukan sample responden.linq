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
  <Reference Relative="..\web\bin\epsikologi.Pengguna.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.Pengguna.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.SesiUjian.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.SesiUjian.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.Soalan.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.Soalan.dll</Reference>
  <Reference Relative="..\web\bin\Newtonsoft.Json.dll">C:\project\rep.generic.psikometrik\web\bin\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Bespoke.Sph.Domain</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

var cikgu = Penggunas.Single (p => p.MyKad == "CikguGerik2").Json.DeserializeFromJson<Bespoke.epsikologi_pengguna.Domain.Pengguna>();
for (int i = 103; i < 200; i++)
{
	var guru = cikgu.Clone();
	guru.Id = "CikguGerik" + i;
	guru.Nama = "Cikgu Gerik " + i;
	guru.MyKad = "CikguGerik" + i;
	guru.WebId = Guid.NewGuid().ToString();
	guru.Emel = string.Format("CikguGerik{0}@gmail.com",i);
	Console.WriteLine (guru.MyKad);
	
	using (var client = new HttpClient())
	{
		
			
		var content = new StringContent(guru.ToJsonString(true));
		var res = await client.PostAsync("http://localhost:50230/pengguna/save", content);
		Console.WriteLine (guru.MyKad + "->" +res.StatusCode);
		
	}
}