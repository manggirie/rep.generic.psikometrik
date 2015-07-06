<Query Kind="Statements">
  <Connection>
    <ID>94368439-6141-4cfd-951f-f0fceeb7a757</ID>
    <Persist>true</Persist>
    <Server>(localdb)\Projects</Server>
    <Database>epsikologi</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference Relative="..\web\bin\domain.sph.dll">C:\project\rep.generic.psikometrik\web\bin\domain.sph.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.Jabatan.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.Jabatan.dll</Reference>
  <Reference Relative="..\web\bin\epsikologi.Kementerian.dll">C:\project\rep.generic.psikometrik\web\bin\epsikologi.Kementerian.dll</Reference>
  <Reference Relative="..\output\epsikologi.Soalan.dll">C:\project\rep.generic.psikometrik\output\epsikologi.Soalan.dll</Reference>
  <Reference Relative="..\web\bin\Newtonsoft.Json.dll">C:\project\rep.generic.psikometrik\web\bin\Newtonsoft.Json.dll</Reference>
  <Namespace>Bespoke.epsikologi_kementerian.Domain</Namespace>
  <Namespace>Bespoke.Sph.Domain</Namespace>
</Query>



foreach (var row in Soalans)
{
	var m = row.Json.DeserializeFromJson<Bespoke.epsikologi_soalan.Domain.Soalan>();
	var ujian = Strings.RegexSingleValue(row.NamaUjian, @".*?\((?<code>.*?)\)", "code");
	var id = string.Format("{0}-{1}-{2}",ujian, m.SeksyenSoalan, m.NoRujukan);
	id = id
	.Replace("PTD-E-PTD-E-", "PTD-E-")
	.Replace("PTD-D-PTD-D-", "PTD-D-")
	.Replace("PTD-C-PTD-C-", "PTD-C-")
	.Replace("PTD-B-PTD-B-", "PTD-B-")
	.Replace("PTD-A-PTD-A-", "PTD-A-")
	.Replace("UKBP-B-UKBP-B", "UKBP-B")
	.Replace("UKBP-A-UKBP-A", "UKBP-A")
	.Replace("HLP--HLPHLP", "HLP")
	.Replace("HLP--HLP", "HLP")
	.Replace("IP--IP-IP","IP--")
	;
	m.Id = id.Substring(0,Math.Min(id.Length, 50));
	File.WriteAllText(@"c:\project\rep.generic.psikometrik\sources\Soalan\" + m.Id + ".json", m.ToJsonString(true));
	//File.Delete(f);
	Console.WriteLine (m.Id);
}