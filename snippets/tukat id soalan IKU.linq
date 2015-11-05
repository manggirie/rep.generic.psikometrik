<Query Kind="Statements">
  <Reference Relative="..\web\bin\domain.sph.dll">C:\project\rep.generic.psikometrik\web\bin\domain.sph.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.Soalan.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.Soalan.dll</Reference>
  <Reference Relative="..\web\bin\Newtonsoft.Json.dll">C:\project\rep.generic.psikometrik\web\bin\Newtonsoft.Json.dll</Reference>
  <Namespace>Bespoke.Sph.Domain</Namespace>
  <Namespace>Bespoke.epsikologi_soalan.Domain</Namespace>
</Query>

int count = 0;
foreach (var f in Directory.GetFiles(@"C:\project\rep.generic.psikometrik\sources\Soalan","*.json"))
{

	var q = f.DeserializeFromJsonFile<Soalan>();
	if(q.NamaUjian != "099ba25a-6f3d-4508-bacb-a0052fc0158a")continue;
	
	q.NamaUjian = "IKU";
	q.Id = q.SoalanNo;
	File.Delete(f);
	
	
	File.WriteAllText(@"C:\project\rep.generic.psikometrik\sources\Soalan\" + q.Id + ".json", q.ToJsonString(true));
	

	
}
Console.WriteLine (count);