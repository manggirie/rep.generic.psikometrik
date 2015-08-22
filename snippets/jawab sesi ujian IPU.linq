<Query Kind="Statements">
  <Connection>
    <ID>94368439-6141-4cfd-951f-f0fceeb7a757</ID>
    <Persist>true</Persist>
    <Server>(localdb)\Projects</Server>
    <Database>epsikologi</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference Relative="..\web\bin\domain.sph.dll">C:\project\rep.generic.psikometrik\web\bin\domain.sph.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.Pengguna.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.Pengguna.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.SesiUjian.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.SesiUjian.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.Soalan.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.Soalan.dll</Reference>
  <Reference Relative="..\web\bin\Newtonsoft.Json.dll">C:\project\rep.generic.psikometrik\web\bin\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Bespoke.Sph.Domain</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

var list = SesiUjians.Where (su => su.Status == "Belum Ambil" && su.NamaUjian == "IPU");
var questions = Soalans.Where (s => s.NamaUjian == "IPU").Select (s => s.Json.DeserializeFromJson<Bespoke.epsikologi_soalan.Domain.Soalan>()).ToList();
Console.WriteLine (questions.Count);

var random = new Random(0);
foreach (var u in list)
{
	var test = u.Json.DeserializeFromJson<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>();
	test.Status = "Diambil";
	test.MasaTamat = DateTime.Now;
	test.MasaMula = DateTime.Now.AddMinutes(-10);
	test.TarikhUjian = DateTime.Today;
	
	var jawapan = from q in questions
					let n = random.Next(100)
					let m = (q.TeksSoalan.Length + n) % 2
					let p = (m == 0 ? 0 : 1)
					let i = q.PilihanJawapanCollection.Count == 1 ? 0 : p
					let a = q.PilihanJawapanCollection[i]
					select new Bespoke.epsikologi_sesiujian.Domain.Jawapan
					{
						WebId = Guid.NewGuid().ToString(),
						JawapanPilihan = a.Teks,
						Trait = q.Trait,
						Nilai = a.Nilai,
						SeksyenSoalan = q.SeksyenSoalan,
						SoalanNo = q.SoalanNo
					};
	test.JawapanCollection.ClearAndAddRange(jawapan);

	
	using (var client = new HttpClient())
	{
		var content = new StringContent(test.ToJsonString(true));
		var response = await client.PostAsync("http://localhost:50230/SesiUjian/Save", content);
		Console.WriteLine ("{0}:{1}", test.MyKad, response.StatusCode);	
		
	}
	
}