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

var daftars = 	from u in Penggunas
				where u.IsResponden
				select new Bespoke.epsikologi_pendaftaranprogram.Domain.PendaftaranProgram
				{
					NamaPengguna = u.Nama,
					NamaProgram = "PELAJAR TAJAAN",
					TarikhDaftar = DateTime.Now,
					NoPermohonan = "PELAJAR TAJAAN/1/1/2015",
					PermohonanId = "34fb6526-7222-4fed-8764-21042bfb7ec1",
					Id = Guid.NewGuid().ToString(),
					MyKad = u.MyKad,
					PendaftaranNo = Guid.NewGuid().ToString(),
					WebId = Guid.NewGuid().ToString()
				};

foreach (var g in daftars)
{
	using (var client = new HttpClient())
	{
		var content = new StringContent(g.ToJsonString(true));
		var res = await client.PostAsync("http://localhost:50230/PendaftaranProgram/save", content);
		Console.WriteLine (g.MyKad + "->" +res.StatusCode);
		
	}
}