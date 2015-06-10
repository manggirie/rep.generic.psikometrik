using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using Bespoke.Sph.SubscribersInfrastructure;

namespace Bespoke.Sph.TriggerSubscribers
{
   public class PendaftaranprogramDaftarSesiUjianTriggerSubscriber: Subscriber<Bespoke.epsikologi_pendaftaranprogram.Domain.PendaftaranProgram>
   {
  
        public override string QueueName
        {
            get { return "trigger_subs_pendaftaranprogram-daftar-sesi-ujian"; }
        }

        public override string[] RoutingKeys
        {
            get { return new[] { "PendaftaranProgram.#.TambahResponden" }; }
        }


        protected override async Task ProcessMessage(Bespoke.epsikologi_pendaftaranprogram.Domain.PendaftaranProgram item, MessageHeaders header)
        {
            var trigger = @"{
  ""$type"": ""Bespoke.Sph.Domain.Trigger, domain.sph"",
  ""RuleCollection"": {
    ""$type"": ""Bespoke.Sph.Domain.ObjectCollection`1[[Bespoke.Sph.Domain.Rule, domain.sph]], domain.sph"",
    ""$values"": []
  },
  ""ActionCollection"": {
    ""$type"": ""Bespoke.Sph.Domain.ObjectCollection`1[[Bespoke.Sph.Domain.CustomAction, domain.sph]], domain.sph"",
    ""$values"": [
      {
        ""$type"": ""Bespoke.Sph.Domain.EmailAction, domain.sph"",
        ""From"": ""admin-epsikomterik@@jpa.gov.my"",
        ""To"": ""admin-epsikomterik@@jpa.gov.my"",
        ""SubjectTemplate"": ""Pendaftaran Responde"",
        ""BodyTemplate"": ""Salam,\n@Model.NamaPengguna sudah berdaft untuk @Model.NamaProgram"",
        ""Bcc"": """",
        ""Cc"": """",
        ""UseAsync"": true,
        ""Title"": ""Tambah Responden"",
        ""IsActive"": true,
        ""TriggerId"": """",
        ""Note"": """",
        ""CustomActionId"": 0,
        ""UseCode"": false,
        ""WebId"": ""a64acdcf-55e9-4213-af8e-83eb49524ac1""
      },
      {
        ""$type"": ""Bespoke.Sph.Domain.StartWorkflowAction, domain.sph"",
        ""WorkflowDefinitionId"": ""pendaftaran-program-sessi-ujian"",
        ""Name"": """",
        ""Version"": 0,
        ""WorkflowTriggerMapCollection"": {
          ""$type"": ""Bespoke.Sph.Domain.ObjectCollection`1[[Bespoke.Sph.Domain.WorkflowTriggerMap, domain.sph]], domain.sph"",
          ""$values"": [
            {
              ""$type"": ""Bespoke.Sph.Domain.WorkflowTriggerMap, domain.sph"",
              ""VariablePath"": ""Pendaftaran"",
              ""Field"": {
                ""$type"": ""Bespoke.Sph.Domain.FunctionField, domain.sph"",
                ""CodeNamespace"": ""ff2cc0ce01"",
                ""Script"": ""item"",
                ""Name"": ""item"",
                ""Note"": """",
                ""WebId"": ""2cc0ce01-a283-4ec6-9abe-32d194c1bcb3""
              },
              ""WebId"": ""b39e1e5f-4154-4f8c-f6b1-776660f713c8""
            }
          ]
        },
        ""UseAsync"": true,
        ""Title"": ""Pendaftaran Program - Sessi Ujian"",
        ""IsActive"": true,
        ""TriggerId"": """",
        ""Note"": """",
        ""CustomActionId"": 0,
        ""UseCode"": false,
        ""WebId"": ""b9e251be-4591-499a-edca-f2fe00a0121b""
      }
    ]
  },
  ""ReferencedAssemblyCollection"": {
    ""$type"": ""Bespoke.Sph.Domain.ObjectCollection`1[[Bespoke.Sph.Domain.ReferencedAssembly, domain.sph]], domain.sph"",
    ""$values"": []
  },
  ""RequeueFilterCollection"": {
    ""$type"": ""Bespoke.Sph.Domain.ObjectCollection`1[[Bespoke.Sph.Domain.ExceptionFilter, domain.sph]], domain.sph"",
    ""$values"": []
  },
  ""Name"": ""Daftar sesi ujian"",
  ""Entity"": ""PendaftaranProgram"",
  ""TypeOf"": ""Bespoke.epsikologi_pendaftaranprogram.Domain.PendaftaranProgram, epsikologi.PendaftaranProgram"",
  ""Note"": ""Daftar sesi ujian"",
  ""IsActive"": true,
  ""IsFiredOnAdded"": false,
  ""IsFiredOnDeleted"": false,
  ""IsFiredOnChanged"": false,
  ""FiredOnOperations"": ""TambahResponden"",
  ""ClassName"": ""PendaftaranprogramDaftarSesiUjianTriggerSubscriber"",
  ""CodeNamespace"": ""Bespoke.Sph.TriggerSubscribers"",
  ""CreatedBy"": ""admin"",
  ""Id"": ""pendaftaranprogram-daftar-sesi-ujian"",
  ""CreatedDate"": ""2015-06-05T11:08:31.4843957+08:00"",
  ""ChangedBy"": ""admin"",
  ""ChangedDate"": ""2015-06-08T15:04:18.1015318+08:00"",
  ""WebId"": ""d51b87f7-23b9-4663-e46c-972e2656a6d2""
}"
                        .DeserializeFromJson<Trigger>();

            this.WriteMessage("Running triggers({0}) with {1} actions and {2} rules", trigger.Name,
                trigger.ActionCollection.Count(x => x.IsActive),
                trigger.RuleCollection.Count);

            foreach (var rule in trigger.RuleCollection)
            {
                try
                {
                    var result = rule.Execute(new RuleContext(item) { Log = header.Log });
                    if (!result)
                    {
                        this.WriteMessage("Rule {0} evaluated to FALSE", rule);
                        return;
                    }
                    this.WriteMessage("Rule {0} evaluated to TRUE", rule);
                }
                catch (Exception e)
                {
                    this.WriteError(e);
                }
            }


            foreach (var customAction in trigger.ActionCollection.Where(a => a.IsActive && !a.UseCode))
            {
                this.WriteMessage(" ==== Executing {0} ======", customAction.Title);
                if (customAction.UseAsync)
                    await customAction.ExecuteAsync(new RuleContext(item)).ConfigureAwait(false);
                else
                    customAction.Execute(new RuleContext(item));

                this.WriteMessage("done...");
            }
        
}

   }
}
