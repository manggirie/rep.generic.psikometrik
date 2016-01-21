<Query Kind="Statements">
  <Connection>
    <ID>a6e3d18e-20db-4104-b9ab-aaa1dc7caa1b</ID>
    <Persist>true</Persist>
    <Server>localhost</Server>
    <Database>epsikologi</Database>
  </Connection>
</Query>

var admin = Aspnet_Users.Single(au => au.UserName == "admin");

var members = Aspnet_Memberships.Where(am => am.UserId != admin.UserId);
Aspnet_Memberships.DeleteAllOnSubmit(members);

var roles = Aspnet_UsersInRoles.Where(r => r.UserId != admin.UserId);
Aspnet_UsersInRoles.DeleteAllOnSubmit(roles);

var otherUsers = Aspnet_Users.Where(au => au.UserName != "admin");
Aspnet_Users.DeleteAllOnSubmit(otherUsers);

var otherProfiles = UserProfiles.Where(up => up.UserName != admin.UserName);
UserProfiles.DeleteAllOnSubmit(otherProfiles);


SubmitChanges();