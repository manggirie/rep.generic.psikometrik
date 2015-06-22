<Query Kind="Statements">
  <Reference Relative="..\web\bin\domain.sph.dll">C:\project\rep.generic.psikometrik\web\bin\domain.sph.dll</Reference>
  <Reference Relative="..\output\epsikologi.Soalan.dll">C:\project\rep.generic.psikometrik\output\epsikologi.Soalan.dll</Reference>
  <Reference Relative="..\web\bin\Newtonsoft.Json.dll">C:\project\rep.generic.psikometrik\web\bin\Newtonsoft.Json.dll</Reference>
  <Namespace>Bespoke.epsikologi_soalan.Domain</Namespace>
  <Namespace>Bespoke.Sph.Domain</Namespace>
</Query>

var lines = File.ReadAllLines(@"C:\project\rep.generic.psikometrik\import\INDEKS PERSONALITI JPA.txt");

var soalan = new Soalan();
decimal count = decimal.Zero;
foreach (var text in lines)
{
	var number =  Strings.RegexInt32Value(text,"(?<num>[0-9]{1,3})\\..*?", "num");
	var qt = Strings.RegexSingleValue(text, "[0-9]{1,3}\\.(?<text>.*?)$", "text");
	if(number.HasValue)
	{
		if(!string.IsNullOrWhiteSpace(soalan.Id))
			File.WriteAllText(@"C:\project\rep.generic.psikometrik\import\index-personaliti\data_" + soalan.SoalanNo + ".json", soalan.ToJsonString(true));
			
		soalan = new Soalan{};
		soalan.TeksSoalan = qt;
		soalan.NamaUjian = "Indeks Personaliti (IP)";
		soalan.SoalanNo = string.Format("IP-{0:00000}",number);
		soalan.NoRujukan = soalan.SoalanNo;
		soalan.Susunan = count++;
		soalan.CreatedBy = "import";
		soalan.CreatedDate = DateTime.Now;
		soalan.ChangedDate = DateTime.Now;
		soalan.ChangedBy = "import";
		soalan.Id = "IP-" + soalan.SoalanNo;
	}else
	{
		 var choice = Strings.RegexSingleValue(text, @"\((?<choice>[a-z]{1})\).*?", "choice");
		 var ans = Strings.RegexSingleValue(text, @"\([a-z]{1}\)(?<answ>.*?)$", "answ");
		 if(!string.IsNullOrWhiteSpace(ans))
		 {
		 
			var jawapan = new PilihanJawapan{
				Teks = ans,
				Nilai = choice == "a" ? 1 : 0
			};
			soalan.PilihanJawapanCollection.Add(jawapan);
		 }
		 
	}
}

if(!string.IsNullOrWhiteSpace(soalan.Id))
	File.WriteAllText(@"C:\project\rep.generic.psikometrik\import\index-personaliti\data_" + soalan.SoalanNo + ".json", soalan.ToJsonString(true));
		