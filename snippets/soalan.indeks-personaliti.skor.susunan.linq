<Query Kind="Statements">
  <Connection>
    <ID>1057a62e-59d6-42f4-92fe-c1bdad65fdf2</ID>
    <Persist>true</Persist>
    <Server>(localdb)\Projects</Server>
    <Database>epsikologi</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference Relative="..\web\bin\domain.sph.dll">C:\project\rep.generic.psikometrik\web\bin\domain.sph.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.Soalan.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.Soalan.dll</Reference>
  <Reference Relative="..\web\bin\Newtonsoft.Json.dll">C:\project\rep.generic.psikometrik\web\bin\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>Bespoke.Sph.Domain</Namespace>
</Query>

var list = Soalans.Where (s => s.NamaUjian == "Indeks Personaliti (IP)");
using (var client = new HttpClient())
{
	foreach (var q in list)
	{
		var no =int.Parse(q.SoalanNo);
		var skor = no % 11 == 0 ? 11 : no% 11;
		Console.WriteLine ("{0} -> Skor {1}", no, skor);
		
		var item = JsonSerializerService.DeserializeFromJson<Bespoke.epsikologi_soalan.Domain.Soalan>(q.Json);
		item.Trait = string.Format("Skor {0}",skor);
		item.Susunan = Convert.ToDecimal(no);
		
		var content = new StringContent(item.ToJsonString(true));
		var res = await client.PostAsync("http://localhost:50230/soalan/save", content);
		Console.WriteLine (res.StatusCode);
	}
}