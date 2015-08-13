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
	if(q.NamaUjian != "Ujian Khas Biasiswa Persekutuan(UKBP)")continue;
	//Console.WriteLine (q.SoalanNo);
	var options = q.PilihanJawapanCollection.Count;
	if(options != 2)
	{
		Console.WriteLine (q);
	}
	if(!q.PilihanJawapanCollection[0].Teks.StartsWith("a)"))
	{
		Console.WriteLine (q);
	}
	
	if(!q.PilihanJawapanCollection[1].Teks.StartsWith("b)"))
	{
		Console.WriteLine (q);
	}
	
	count++;
	
}
Console.WriteLine (count);