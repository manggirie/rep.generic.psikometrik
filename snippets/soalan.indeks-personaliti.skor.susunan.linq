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
		var res = await client.DeleteAsync("http://localhost:50230/soalan/remove/" + q.Id);
		Console.WriteLine (res.StatusCode);
	}
}