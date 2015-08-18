<Query Kind="Statements">
  <Connection>
    <ID>42586aa0-3dee-4da7-83d7-54d6b1bd2079</ID>
    <Persist>true</Persist>
    <Server>(localdb)\Projects</Server>
    <Database>epsikologi</Database>
  </Connection>
</Query>

var tests = SesiUjians.Where(s =>s.Status == "Belum Ambil").Take (100);
SesiUjians.DeleteAllOnSubmit(tests);
SubmitChanges();