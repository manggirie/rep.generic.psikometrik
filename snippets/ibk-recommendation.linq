<Query Kind="Statements">
  <Reference Relative="..\web\bin\domain.sph.dll">C:\project\rep.generic.psikometrik\web\bin\domain.sph.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.IbkRecommendation.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.IbkRecommendation.dll</Reference>
  <Reference Relative="..\web\bin\Newtonsoft.Json.dll">C:\project\rep.generic.psikometrik\web\bin\Newtonsoft.Json.dll</Reference>
  <Namespace>Bespoke.epsikologi_ibkrecommendation.Domain</Namespace>
  <Namespace>Bespoke.Sph.Domain</Namespace>
</Query>

foreach (var f in Directory.GetFiles(@"C:\project\rep.generic.psikometrik\sources\IbkRecommendation","*.json"))
{

	var q = f.DeserializeFromJsonFile<IbkRecommendation>();
	q.Id = q.Kod.Replace("/", "-");
	var json = q.ToJsonString(true);
	File.WriteAllText(@"C:\project\rep.generic.psikometrik\sources\IbkRecommendation\" + q.Id + ".json", json);
	File.Delete(f);
	
}