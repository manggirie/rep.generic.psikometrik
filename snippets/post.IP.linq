<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>System.Net.Http</Namespace>
</Query>

var ips = Directory.GetFiles(@"C:\project\rep.generic.psikometrik\import\index-personaliti","*.json");
using (var client = new HttpClient())
{
	foreach (var id in ips)
	{
		var content = new StringContent(File.ReadAllText(id));
		var response = await client.PostAsync("http://localhost:50230/soalan/save", content);
		Console.WriteLine ("{0}:{1}", Path.GetFileNameWithoutExtension(id), response.StatusCode);	
	}
}