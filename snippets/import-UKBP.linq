<Query Kind="Statements">
  <Reference Relative="..\web\bin\domain.sph.dll">C:\project\rep.generic.psikometrik\web\bin\domain.sph.dll</Reference>
  <Reference Relative="..\output\epsikologi.Soalan.dll">C:\project\rep.generic.psikometrik\output\epsikologi.Soalan.dll</Reference>
  <Reference Relative="..\web\bin\Newtonsoft.Json.dll">C:\project\rep.generic.psikometrik\web\bin\Newtonsoft.Json.dll</Reference>
  <Namespace>Bespoke.epsikologi_soalan.Domain</Namespace>
  <Namespace>Bespoke.Sph.Domain</Namespace>
</Query>

var sections = new []{"A","B"};

foreach(var sec in sections)
{
var lines = File.ReadAllLines(string.Format(@"C:\project\rep.generic.psikometrik\import\UKBP.{0}.csv", sec));

var soalan = new Soalan();
foreach (var text in lines)
{
	var number =  Strings.RegexInt32Value(text,@"(?<num>[0-9]{1,3}),.*?", "num");
	var qt = Strings.RegexSingleValue(text, @"[0-9]{1,3},(?<text>.*?)$", "text");
	if(number.HasValue)
	{
		if(!string.IsNullOrWhiteSpace(soalan.Id))
			File.WriteAllText(@"C:\project\rep.generic.psikometrik\import\UKBP\data_" + soalan.SoalanNo + ".json", soalan.ToJsonString(true));
		
		if(qt.StartsWith("\"")){
			qt = qt.Substring(1, qt.Length - 2).Replace("\"\"", "\"");
		}
			
		soalan = new Soalan{};
		soalan.TeksSoalan = qt.Replace(",,,,,,,,,","").Replace(".,...", "");
		soalan.NamaUjian = "Ujian Khas Biasiswa Persekutuan(UKBP)";
		soalan.SoalanNo = string.Format("UKBP-{0}-{1:00000}",sec,number);
		soalan.NoRujukan = soalan.SoalanNo;
		soalan.Susunan = Convert.ToDecimal(number ?? 0);
		soalan.CreatedBy = "import";
		soalan.CreatedDate = DateTime.Now;
		soalan.ChangedDate = DateTime.Now;
		soalan.ChangedBy = "import";
		soalan.Id = soalan.SoalanNo;
		soalan.SeksyenSoalan = sec;
		
		soalan.PilihanJawapanCollection.Add(new PilihanJawapan{ Nilai = 1, Teks = "Ya"});
		soalan.PilihanJawapanCollection.Add(new PilihanJawapan{ Nilai = 0, Teks = "Tidak"});
	}
}

if(!string.IsNullOrWhiteSpace(soalan.Id))
	File.WriteAllText(@"C:\project\rep.generic.psikometrik\import\UKBP\data_" + soalan.SoalanNo + ".json", soalan.ToJsonString(true));

}