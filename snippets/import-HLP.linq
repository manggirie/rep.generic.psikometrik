<Query Kind="Statements">
  <Reference Relative="..\web\bin\domain.sph.dll">C:\project\rep.generic.psikometrik\web\bin\domain.sph.dll</Reference>
  <Reference Relative="..\output\epsikologi.Soalan.dll">C:\project\rep.generic.psikometrik\output\epsikologi.Soalan.dll</Reference>
  <Reference Relative="..\web\bin\Newtonsoft.Json.dll">C:\project\rep.generic.psikometrik\web\bin\Newtonsoft.Json.dll</Reference>
  <Namespace>Bespoke.epsikologi_soalan.Domain</Namespace>
  <Namespace>Bespoke.Sph.Domain</Namespace>
</Query>

var lines = File.ReadAllLines(@"C:\project\rep.generic.psikometrik\import\HLP.csv");

var soalan = new Soalan();
foreach (var text in lines)
{
	var number =  Strings.RegexInt32Value(text,"(?<num>[0-9]{1,3}),.*?", "num");
	var qt = Strings.RegexSingleValue(text, "[0-9]{1,3},(?<text>.*?)$", "text");
	if(number.HasValue)
	{
		if(!string.IsNullOrWhiteSpace(soalan.Id))
			File.WriteAllText(@"C:\project\rep.generic.psikometrik\import\HLP\data_" + soalan.SoalanNo + ".json", soalan.ToJsonString(true));
			
		soalan = new Soalan{};
		soalan.TeksSoalan = qt;
		soalan.NamaUjian = "Ujian Khas Hadiah Latihan Persekutuan(HLP)";
		soalan.SoalanNo = string.Format("HLP-{0:00000}",number);
		soalan.NoRujukan = soalan.SoalanNo;
		soalan.Susunan = Convert.ToDecimal(number ?? 0);
		soalan.CreatedBy = "import";
		soalan.CreatedDate = DateTime.Now;
		soalan.ChangedDate = DateTime.Now;
		soalan.ChangedBy = "import";
		soalan.Id = "HLP" + soalan.SoalanNo;
	}else
	{
		soalan.PilihanJawapanCollection.Add(new PilihanJawapan{ Nilai = 1, Teks = "Ya"});
		soalan.PilihanJawapanCollection.Add(new PilihanJawapan{ Nilai = 0, Teks = "Tidak"});
		 
		 
	}
}
if(!string.IsNullOrWhiteSpace(soalan.Id))
	File.WriteAllText(@"C:\project\rep.generic.psikometrik\import\HLP\data_" + soalan.SoalanNo + ".json", soalan.ToJsonString(true));