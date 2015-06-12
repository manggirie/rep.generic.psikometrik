using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;

namespace Bespoke.Sph.Workflows_PendaftaranProgramSessiUjian_0
{
    [EntityType(typeof(Workflow))]
    public partial class PendaftaranProgramSessiUjianWorkflow : Bespoke.Sph.Domain.Workflow
    {
        public PendaftaranProgramSessiUjianWorkflow()
        {
            this.Name = "Pendaftaran Program - Sessi Ujian";
            this.Version = 0;
            this.WorkflowDefinitionId = "pendaftaran-program-sessi-ujian";
            this.StatusSesiUjian = "Belum Ambil";
        }

        private Bespoke.epsikologi_pengguna.Domain.Pengguna m_Responden = new Bespoke.epsikologi_pengguna.Domain.Pengguna();
        public Bespoke.epsikologi_pengguna.Domain.Pengguna Responden
        {
            get { return m_Responden; }
            set { m_Responden = value; }
        }

        private Bespoke.epsikologi_pendaftaranprogram.Domain.PendaftaranProgram m_Pendaftaran = new Bespoke.epsikologi_pendaftaranprogram.Domain.PendaftaranProgram();
        public Bespoke.epsikologi_pendaftaranprogram.Domain.PendaftaranProgram Pendaftaran
        {
            get { return m_Pendaftaran; }
            set { m_Pendaftaran = value; }
        }

        private Bespoke.epsikologi_permohonan.Domain.Permohonan m_Permohonan = new Bespoke.epsikologi_permohonan.Domain.Permohonan();
        public Bespoke.epsikologi_permohonan.Domain.Permohonan Permohonan
        {
            get { return m_Permohonan; }
            set { m_Permohonan = value; }
        }

        public System.String UjianList { get; set; }
        public System.String CurrentUjian { get; set; }
        private Bespoke.epsikologi_sesiujian.Domain.SesiUjian m_SesiUjian = new Bespoke.epsikologi_sesiujian.Domain.SesiUjian();
        public Bespoke.epsikologi_sesiujian.Domain.SesiUjian SesiUjian
        {
            get { return m_SesiUjian; }
            set { m_SesiUjian = value; }
        }

        public System.String StatusSesiUjian { get; set; }



        public override async Task<ActivityExecutionResult> StartAsync()
        {
            this.SerializedDefinitionStoreId = "wd.pendaftaran-program-sessi-ujian.0";
            var result = await this.GetRespondenAsync().ConfigureAwait(false);
            return result;
        }


        public override async Task<ActivityExecutionResult> ExecuteAsync(string activityId, string correlation = null)
        {
            this.SerializedDefinitionStoreId = "wd.pendaftaran-program-sessi-ujian.0";
            ActivityExecutionResult result = null;
            switch (activityId)
            {
                case "a7168626-d0e1-498a-8abb-ae4a37aa8fcc":
                    result = await this.EmailRespondenAsync().ConfigureAwait(false);
                    break;
                case "44cc320d-9e8e-4eef-d531-d6a5aab02556":
                    result = await this.GetRespondenAsync().ConfigureAwait(false);
                    break;
                case "95f60059-0cf5-43ba-d310-7d9b087548a4":
                    result = await this.EndPendaftaranAsync().ConfigureAwait(false);
                    break;
                case "3c628fca-73bc-458b-b34e-912678468dd2":
                    result = await this.GetPermohonanAsync().ConfigureAwait(false);
                    break;
                case "292e4dd2-52d4-4e17-f282-66cb9c4aadcf":
                    result = await this.ExtractUjianListAsync().ConfigureAwait(false);
                    break;
                case "dfc600f9-a1da-4ed9-c148-84bb355b73e8":
                    result = await this.CheckIfUjianListAsync().ConfigureAwait(false);
                    break;
                case "f9b816f6-a1c8-42a9-86fa-838f0375f40b":
                    result = await this.AddNewSesiUjianAsync().ConfigureAwait(false);
                    break;
            }
            result.Correlation = correlation;
            await this.SaveAsync(activityId, result).ConfigureAwait(false);
            return result;
        }

        //exec:a7168626-d0e1-498a-8abb-ae4a37aa8fcc
        public async Task<ActivityExecutionResult> EmailRespondenAsync()
        {
            var result = new ActivityExecutionResult { Status = ActivityExecutionStatus.Success };
            var act = this.GetActivity<NotificationActivity>("a7168626-d0e1-498a-8abb-ae4a37aa8fcc");
            result.NextActivities = new[] { "3c628fca-73bc-458b-b34e-912678468dd2" };

            var @from = await this.TransformFromEmailRespondenAsync(act.From);
            var to = await this.TransformToEmailRespondenAsync(act.To);
            var subject = await this.TransformSubjectEmailRespondenAsync(act.Subject);
            var body = await this.TransformBodyEmailRespondenAsync(act.Body);
            var cc = await this.TransformBodyEmailRespondenAsync(act.Cc);
            var bcc = await this.TransformBodyEmailRespondenAsync(act.Bcc);

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
                    await session.SubmitChanges("Email responden").ConfigureAwait(false);
                }
            }


            return result;
        }

        //exec:44cc320d-9e8e-4eef-d531-d6a5aab02556
        public async Task<ActivityExecutionResult> GetRespondenAsync()
        {

            var result = new ActivityExecutionResult { Status = ActivityExecutionStatus.Success };
            var item = this;
            var context = new SphDataContext();
            this.Responden = await context.LoadOneAsync<Bespoke.epsikologi_pengguna.Domain.Pengguna>(x => x.MyKad == this.Pendaftaran.MyKad);
            result.NextActivities = new[] { "a7168626-d0e1-498a-8abb-ae4a37aa8fcc" };


            return result;
        }

        //exec:95f60059-0cf5-43ba-d310-7d9b087548a4
        public Task<ActivityExecutionResult> EndPendaftaranAsync()
        {
            var result = new ActivityExecutionResult { Status = ActivityExecutionStatus.Success };
            result.NextActivities = new string[] { };
            this.State = "Completed";

            return Task.FromResult(result);
        }

        //exec:3c628fca-73bc-458b-b34e-912678468dd2
        public async Task<ActivityExecutionResult> GetPermohonanAsync()
        {

            var result = new ActivityExecutionResult { Status = ActivityExecutionStatus.Success };
            var item = this;
            var context = new SphDataContext();
            this.Permohonan = await context.LoadOneAsync<Bespoke.epsikologi_permohonan.Domain.Permohonan>(x => x.PermohonanNo == this.Pendaftaran.NoPermohonan);
            var ujian = "";
            if (this.Permohonan.isIBK)
                ujian += ";IBK";
            if (this.Permohonan.isISO)
                ujian += ";ISO";
            if (this.Permohonan.isIBK)
                ujian += ";IBK";
            if (this.Permohonan.isIP)
                ujian += ";IP";
            if (this.Permohonan.isIPU)
                ujian += ";IPU";
            if (this.Permohonan.isUKHLP)
                ujian += ";UKHLP";
            if (this.Permohonan.isUKBP)
                ujian += ";UKBP";
            if (this.Permohonan.isPPKP)
                ujian += ";PPKP";

            this.UjianList = ujian;
            result.NextActivities = new[] { "dfc600f9-a1da-4ed9-c148-84bb355b73e8" };


            return result;
        }

        //exec:292e4dd2-52d4-4e17-f282-66cb9c4aadcf
        public Task<ActivityExecutionResult> ExtractUjianListAsync()
        {

            var result = new ActivityExecutionResult { Status = ActivityExecutionStatus.Success };
            var item = this;

            Console.WriteLine("Ujian List : {0}", this.UjianList);
            this.CurrentUjian = this.UjianList.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            this.UjianList = this.UjianList.Replace(";" + this.CurrentUjian, "");

            Console.WriteLine("Current Ujian : {0}", this.CurrentUjian);
            result.NextActivities = new[] { "f9b816f6-a1c8-42a9-86fa-838f0375f40b" };


            return Task.FromResult(result);
        }

        //exec:dfc600f9-a1da-4ed9-c148-84bb355b73e8
        public Task<ActivityExecutionResult> CheckIfUjianListAsync()
        {
            var result = new ActivityExecutionResult { Status = ActivityExecutionStatus.Success };
            var branch1 = this.CheckIfUjianListAdaUjian();
            if (branch1)
            {
                result.NextActivities = new[] { "292e4dd2-52d4-4e17-f282-66cb9c4aadcf" };
                return Task.FromResult(result);
            }
            result.NextActivities = new[] { "95f60059-0cf5-43ba-d310-7d9b087548a4" };

            return Task.FromResult(result);
        }

        //exec:f9b816f6-a1c8-42a9-86fa-838f0375f40b
        public async Task<ActivityExecutionResult> AddNewSesiUjianAsync()
        {
            var item = this.SesiUjian;
            item.Id = Guid.NewGuid().ToString();
            item.NamaProgram = this.Permohonan.PermohonanNo;
            item.NamaPengguna = this.Responden.Nama;
            item.MyKad = this.Responden.MyKad;
            item.NamaUjian = this.CurrentUjian;
            item.Status = this.StatusSesiUjian;
            var context = new Bespoke.Sph.Domain.SphDataContext();
            using (var session = context.OpenSession())
            {
                session.Attach(item);
                await session.SubmitChanges();
            }
            var result = new ActivityExecutionResult { Status = ActivityExecutionStatus.Success };
            result.NextActivities = new[] { "dfc600f9-a1da-4ed9-c148-84bb355b73e8" };

            return result;
        }

    }
}
