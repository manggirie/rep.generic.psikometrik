<Query Kind="Statements">
  <Reference Relative="..\web\bin\domain.sph.dll">C:\project\rep.generic.psikometrik\web\bin\domain.sph.dll</Reference>
  <Reference Relative="..\output\epsikologi.Jabatan.dll">C:\project\rep.generic.psikometrik\output\epsikologi.Jabatan.dll</Reference>
  <Reference Relative="..\output\epsikologi.Kementerian.dll">C:\project\rep.generic.psikometrik\output\epsikologi.Kementerian.dll</Reference>
  <Reference Relative="..\web\bin\Newtonsoft.Json.dll">C:\project\rep.generic.psikometrik\web\bin\Newtonsoft.Json.dll</Reference>
  <Namespace>Bespoke.Sph.Domain</Namespace>
</Query>

foreach (var f in Directory.GetFiles(@"C:\temp\kementerian", "*.json"))
{
	var k = f.DeserializeFromJsonFile<Bespoke.epsikologi_kementerian.Domain.Kementerian>();
	var id = k.NamaKementerian.ToIdFormat();
	k.Id = id.Substring(0, Math.Min(id.Length,50));
	Console.WriteLine (k.Id);
	Console.WriteLine (k);
	
	File.WriteAllText(@"C:\project\rep.generic.psikometrik\sources\Kementerian\" + k.Id + ".json", k.ToJsonString(true));
	
}