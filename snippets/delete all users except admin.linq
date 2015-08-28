<Query Kind="Statements">
  <Connection>
    <ID>42586aa0-3dee-4da7-83d7-54d6b1bd2079</ID>
    <Persist>true</Persist>
    <Server>(localdb)\Projects</Server>
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
