<Query Kind="Statements">
  <Connection>
    <ID>a6e3d18e-20db-4104-b9ab-aaa1dc7caa1b</ID>
    <Persist>true</Persist>
    <Server>localhost</Server>
    <Database>epsikologi</Database>
  </Connection>
  <Reference Relative="..\web\bin\domain.sph.dll">C:\project\rep.generic.psikometrik\web\bin\domain.sph.dll</Reference>
  <Reference Relative="..\web\bin\Newtonsoft.Json.dll">C:\project\rep.generic.psikometrik\web\bin\Newtonsoft.Json.dll</Reference>
</Query>

var user = new Bespoke.Sph.Domain.UserProfile
{
	UserName = "admin",
	Department = "",
	Designation = "developers",
	FullName = "admin",
	Email = "admin@jpa.gov.my",
	Id = "admin",
	IsLockedOut = false,
	HasChangedDefaultPassword = true,
	StartModule = "dev.home",
	Telephone = ""
};

var up = new UserProfile
{
	Json = Bespoke.Sph.Domain.JsonSerializerService.ToJsonString(user, true),
	Id = "admin",
	ChangedBy = "admin",
	ChangedDate = DateTime.Today,
	CreatedBy = "admin",
	CreatedDate = DateTime.Today,
	Department = user.Department,
	Designation = user.Designation,
	Email = "admin@jpa.gov.my",
	FullName = user.FullName,
	UserName = user.UserName
};
UserProfiles.InsertOnSubmit(up);
SubmitChanges();