using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;

namespace Bespoke.Sph.Workflows_CreatesMembershipForNewResponden_0
{
    [EntityType(typeof(Workflow))]
    public partial class CreatesMembershipForNewRespondenWorkflow : Bespoke.Sph.Domain.Workflow
    {
        public CreatesMembershipForNewRespondenWorkflow()
        {
            this.Name = "Creates membership for new responden";
            this.Version = 0;
            this.WorkflowDefinitionId = "creates-membership-for-new-responden";
        }

        private Bespoke.epsikologi_pengguna.Domain.Pengguna m_Pengguna = new Bespoke.epsikologi_pengguna.Domain.Pengguna();
        public Bespoke.epsikologi_pengguna.Domain.Pengguna Pengguna
        {
            get { return m_Pengguna; }
            set { m_Pengguna = value; }
        }

        public System.String Password { get; set; }



        public override async Task<ActivityExecutionResult> StartAsync()
        {
            this.SerializedDefinitionStoreId = "wd.creates-membership-for-new-responden.0";
            var result = await this.CreateProfileAndMembershipAsync().ConfigureAwait(false);
            return result;
        }


        public override async Task<ActivityExecutionResult> ExecuteAsync(string activityId, string correlation = null)
        {
            this.SerializedDefinitionStoreId = "wd.creates-membership-for-new-responden.0";
            ActivityExecutionResult result = null;
            switch (activityId)
            {
                case "6527072e-1302-4de8-cdbe-52968cfea0a4":
                    result = await this.CreateProfileAndMembershipAsync().ConfigureAwait(false);
                    break;
                case "56dd17a3-d01d-44d6-a2c1-d1fb0587655e":
                    result = await this.SiapAsync().ConfigureAwait(false);
                    break;
                case "41cddfd4-0781-44b1-db25-cbef78c61352":
                    result = await this.EmailPenggunaAsync().ConfigureAwait(false);
                    break;
                case "e3dc1536-e258-4a73-fb1d-7b59f8fabc35":
                    result = await this.AdaEmel2Async().ConfigureAwait(false);
                    break;
                case "659b3c30-7a39-488e-da26-56b627c6050b":
                    result = await this.Email2Async().ConfigureAwait(false);
                    break;
            }
            result.Correlation = correlation;
            await this.SaveAsync(activityId, result).ConfigureAwait(false);
            return result;
        }

        //exec:6527072e-1302-4de8-cdbe-52968cfea0a4
        public async Task<ActivityExecutionResult> CreateProfileAndMembershipAsync()
        {

            var result = new ActivityExecutionResult { Status = ActivityExecutionStatus.Success };
            var item = this;
            this.Password = System.Web.Security.Membership.GeneratePassword(6, 0);

            var context = new SphDataContext();
            var designation = await context.LoadOneAsync<Designation>(d => d.Name == "Responden");
            var roles = designation.RoleCollection.ToArray();
            var profile = new UserProfile
            {
                Id = this.Pengguna.Id,
                UserName = this.Pengguna.MyKad,
                Email = this.Pengguna.Emel,
                FullName = this.Pengguna.Nama,
                Designation = "Responden",
                HasChangedDefaultPassword = false,
                RoleTypes = string.Join(",", roles),
                StartModule = "responden-home",
                Language = "en-US"

            };
            var exist = System.Web.Security.Membership.GetUser(profile.UserName);
            Console.WriteLine("done mapping user profile");

            if (null == exist)
            {
                System.Web.Security.Membership.CreateUser(profile.UserName, this.Password, profile.Email);
                System.Web.Security.Roles.AddUserToRoles(profile.UserName, roles);

                Console.WriteLine("insert into user profile");
                using (var session = context.OpenSession())
                {
                    session.Attach(profile);
                    await session.SubmitChanges();
                }

                //IsCreated = true;
            }

            result.NextActivities = new[] { "41cddfd4-0781-44b1-db25-cbef78c61352" };


            return result;
        }

        //exec:56dd17a3-d01d-44d6-a2c1-d1fb0587655e
        public Task<ActivityExecutionResult> SiapAsync()
        {
            var result = new ActivityExecutionResult { Status = ActivityExecutionStatus.Success };
            result.NextActivities = new string[] { };
            this.State = "Completed";

            return Task.FromResult(result);
        }

        //exec:41cddfd4-0781-44b1-db25-cbef78c61352
        public async Task<ActivityExecutionResult> EmailPenggunaAsync()
        {
            var result = new ActivityExecutionResult { Status = ActivityExecutionStatus.Success };
            var act = this.GetActivity<NotificationActivity>("41cddfd4-0781-44b1-db25-cbef78c61352");
            result.NextActivities = new[] { "e3dc1536-e258-4a73-fb1d-7b59f8fabc35" };

            var @from = await this.TransformFromEmailPenggunaAsync(act.From);
            var to = await this.TransformToEmailPenggunaAsync(act.To);
            var subject = await this.TransformSubjectEmailPenggunaAsync(act.Subject);
            var body = await this.TransformBodyEmailPenggunaAsync(act.Body);
            var cc = await this.TransformBodyEmailPenggunaAsync(act.Cc);
            var bcc = await this.TransformBodyEmailPenggunaAsync(act.Bcc);

            var client = new System.Net.Mail.SmtpClient();
            var mm = new System.Net.Mail.MailMessage();
            mm.Subject = subject;
            mm.Body = body;
            mm.From = new System.Net.Mail.MailAddress(@from);
            mm.To.Add(to);
            if (!string.IsNullOrWhiteSpace(cc))
                mm.CC.Add(cc);
            if (!string.IsNullOrWhiteSpace(bcc))
                mm.Bcc.Add(bcc);
            await client.SendMailAsync(mm).ConfigureAwait(false);


            var context = new SphDataContext();
            foreach (var et in to.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var et1 = et;
                var user = await context.LoadOneAsync<UserProfile>(u => u.Email == et1);
                if (null == user) continue;
                var message = new Message
                {
                    Subject = subject,
                    UserName = user.UserName,
                    Body = body,
                    Id = Strings.GenerateId()
                };
                using (var session = context.OpenSession())
                {
                    session.Attach(message);
                    await session.SubmitChanges("Email Pengguna").ConfigureAwait(false);
                }
            }


            return result;
        }

        //exec:e3dc1536-e258-4a73-fb1d-7b59f8fabc35
        public Task<ActivityExecutionResult> AdaEmel2Async()
        {
            var result = new ActivityExecutionResult { Status = ActivityExecutionStatus.Success };
            var branch1 = this.AdaEmel2Ya();
            if (branch1)
            {
                result.NextActivities = new[] { "659b3c30-7a39-488e-da26-56b627c6050b" };
                return Task.FromResult(result);
            }
            result.NextActivities = new[] { "56dd17a3-d01d-44d6-a2c1-d1fb0587655e" };

            return Task.FromResult(result);
        }

        //exec:659b3c30-7a39-488e-da26-56b627c6050b
        public async Task<ActivityExecutionResult> Email2Async()
        {
            var result = new ActivityExecutionResult { Status = ActivityExecutionStatus.Success };
            var act = this.GetActivity<NotificationActivity>("659b3c30-7a39-488e-da26-56b627c6050b");
            result.NextActivities = new[] { "56dd17a3-d01d-44d6-a2c1-d1fb0587655e" };

            var @from = await this.TransformFromEmail2Async(act.From);
            var to = await this.TransformToEmail2Async(act.To);
            var subject = await this.TransformSubjectEmail2Async(act.Subject);
            var body = await this.TransformBodyEmail2Async(act.Body);
            var cc = await this.TransformBodyEmail2Async(act.Cc);
            var bcc = await this.TransformBodyEmail2Async(act.Bcc);

            var client = new System.Net.Mail.SmtpClient();
            var mm = new System.Net.Mail.MailMessage();
            mm.Subject = subject;
            mm.Body = body;
            mm.From = new System.Net.Mail.MailAddress(@from);
            mm.To.Add(to);
            if (!string.IsNullOrWhiteSpace(cc))
                mm.CC.Add(cc);
            if (!string.IsNullOrWhiteSpace(bcc))
                mm.Bcc.Add(bcc);
            await client.SendMailAsync(mm).ConfigureAwait(false);


            var context = new SphDataContext();
            foreach (var et in to.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var et1 = et;
                var user = await context.LoadOneAsync<UserProfile>(u => u.Email == et1);
                if (null == user) continue;
                var message = new Message
                {
                    Subject = subject,
                    UserName = user.UserName,
                    Body = body,
                    Id = Strings.GenerateId()
                };
                using (var session = context.OpenSession())
                {
                    session.Attach(message);
                    await session.SubmitChanges("Email 2").ConfigureAwait(false);
                }
            }


            return result;
        }

    }
}
