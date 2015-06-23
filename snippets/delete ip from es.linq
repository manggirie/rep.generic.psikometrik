<Query Kind="Statements">
  <Connection>
    <ID>94368439-6141-4cfd-951f-f0fceeb7a757</ID>
    <Persist>true</Persist>
    <Server>(localdb)\Projects</Server>
    <Database>epsikologi</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>System.Net.Http</Namespace>
</Query>

// DELETE FROM [epsikologi].[Soalan]
// WHERE [NamaUjian] = 'Indeks Personaliti (IP)'
var ips = Soalans.Where (s => s.NamaUjian == "Indeks Personaliti (IP)").Select (s => s.Id);
ips.Dump();

using (var client = new HttpClient())
{
	foreach (var id in ips)
	{
		var response = await client.DeleteAsync("http://localhost:9200/epsikologi/soalan/" + id);
		Console.WriteLine ("{0}", response.StatusCode);	
	}
}