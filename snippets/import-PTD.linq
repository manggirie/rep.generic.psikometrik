<Query Kind="Statements">
  <Reference Relative="..\web\bin\domain.sph.dll">C:\project\rep.generic.psikometrik\web\bin\domain.sph.dll</Reference>
  <Reference Relative="..\output\epsikologi.Soalan.dll">C:\project\rep.generic.psikometrik\output\epsikologi.Soalan.dll</Reference>
  <Reference Relative="..\web\bin\Newtonsoft.Json.dll">C:\project\rep.generic.psikometrik\web\bin\Newtonsoft.Json.dll</Reference>
  <Namespace>Bespoke.epsikologi_soalan.Domain</Namespace>
  <Namespace>Bespoke.Sph.Domain</Namespace>
</Query>

var sections = new []{"A","B", "C", "D", "E"};

foreach(var sec in sections)
{
var lines = File.ReadAllLines(string.Format(@"C:\project\rep.generic.psikometrik\import\PTD.{0}.csv", sec));

var soalan = new Soalan();
foreach (var text in lines)
{
	var number =  Strings.RegexInt32Value(text,@"\((?<num>[0-9]{1,3})\).*?", "num");
	var qt = Strings.RegexSingleValue(text, @"\([0-9]{1,3}\)(?<text>.*?)$", "text");
	if(number.HasValue)
	{
		if(!string.IsNullOrWhiteSpace(soalan.Id))
			File.WriteAllText(@"C:\project\rep.generic.psikometrik\import\PTD\data_" + soalan.SoalanNo + ".json", soalan.ToJsonString(true));
			
		soalan = new Soalan{};
		soalan.TeksSoalan = qt.Replace(",,,,,,,,,","").Replace(".,...", "");
		soalan.NamaUjian = "Profil Personaliti & Kerjaya Pegawai(PTD)";
		soalan.SoalanNo = string.Format("PTD-{0}-{1:00000}",sec,number);
		soalan.NoRujukan = soalan.SoalanNo;
		soalan.Susunan = Convert.ToDecimal(number ?? 0);
		soalan.CreatedBy = "import";
		soalan.CreatedDate = DateTime.Now;
		soalan.ChangedDate = DateTime.Now;
		soalan.ChangedBy = "import";
		soalan.Id = soalan.SoalanNo;
		soalan.SeksyenSoalan = sec;
	}else
	{
		var answers = text.Split(new []{","}, StringSplitOptions.RemoveEmptyEntries)
		.Select ((t, i )=> new PilihanJawapan{ Nilai = i, Teks =t});
		soalan.PilihanJawapanCollection.AddRange(answers);
	}
}

if(!string.IsNullOrWhiteSpace(soalan.Id))
	File.WriteAllText(@"C:\project\rep.generic.psikometrik\import\PTD\data_" + soalan.SoalanNo + ".json", soalan.ToJsonString(true));

}