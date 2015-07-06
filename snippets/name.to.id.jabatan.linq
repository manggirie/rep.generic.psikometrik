<Query Kind="Statements">
  <Connection>
    <ID>1057a62e-59d6-42f4-92fe-c1bdad65fdf2</ID>
    <Persist>true</Persist>
    <Server>(localdb)\Projects</Server>
    <Database>epsikologi</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference Relative="..\web\bin\domain.sph.dll">C:\project\rep.generic.psikometrik\web\bin\domain.sph.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.Jabatan.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.Jabatan.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.Kementerian.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.Kementerian.dll</Reference>
  <Reference Relative="..\web\bin\Newtonsoft.Json.dll">C:\project\rep.generic.psikometrik\web\bin\Newtonsoft.Json.dll</Reference>
  <Namespace>Bespoke.epsikologi_kementerian.Domain</Namespace>
  <Namespace>Bespoke.Sph.Domain</Namespace>
</Query>

foreach (var f in Directory.GetFiles(@"c:\project\rep.generic.psikometrik\sources\Jabatan\", "*.json"))
{
 	var m = f.DeserializeFromJsonFile<Bespoke.epsikologi_jabatan.Domain.Jabatan>();
	var id = m.NamaJabatan.ToIdFormat();
	m.Id = id.Substring(0,Math.Min(id.Length, 50));
	File.WriteAllText(@"c:\project\rep.generic.psikometrik\sources\Jabatan\" + m.Id + ".json", m.ToJsonString(true));
	File.Delete(f);
}