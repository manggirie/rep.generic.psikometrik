<Query Kind="Statements">
  <Connection>
    <ID>42586aa0-3dee-4da7-83d7-54d6b1bd2079</ID>
    <Persist>true</Persist>
    <Server>(localdb)\Projects</Server>
    <Database>epsikologi</Database>
  </Connection>
  <Reference Relative="..\web\bin\domain.sph.dll">C:\project\rep.generic.psikometrik\web\bin\domain.sph.dll</Reference>
  <Reference Relative="..\web\bin\Newtonsoft.Json.dll">C:\project\rep.generic.psikometrik\web\bin\Newtonsoft.Json.dll</Reference>
</Query>

var user = new Bespoke.Sph.Domain.UserProfile {
UserName = "admin",
Department = "",
Designation = "developers",
FullName = "admin",
Email = "admin@jpa.gov.my",
Id = "admin",
IsLockedOut = false,
HasChangedDefaultPassword = true,
StartModule = "dev.home",
Telephone = ""};

var up = new UserProfile {
Json = Bespoke.Sph.Domain.JsonSerializerService.ToJsonString(user, true),
Id = "admin",
ChangedBy = "admin",
ChangedDate =DateTime.Today,
CreatedBy= "admin",
CreatedDate = DateTime.Today,
Department = user.Department,
Designation = user.Designation,
Email = "admin@jpa.gov.my",
FullName = user.FullName,
UserName = user.UserName
};
UserProfiles.InsertOnSubmit(up);
SubmitChanges();