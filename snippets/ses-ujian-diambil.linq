<Query Kind="Expression">
  <Connection>
    <ID>1057a62e-59d6-42f4-92fe-c1bdad65fdf2</ID>
    <Persist>true</Persist>
    <Server>(localdb)\Projects</Server>
    <Database>epsikologi</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

SesiUjians.Where(s => s.Status == "Diambil")
.Where(s => s.NamaUjian == "IPU")
.OrderByDescending(s => s.CreatedDate)
.Select(s => new {
s.Id,
s.NamaProgram,
s.NamaPengguna,
s.MyKad,
s.NamaUjian,
s.TarikhUjian
})
.Take(10)