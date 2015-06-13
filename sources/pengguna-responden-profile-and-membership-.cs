using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using Bespoke.Sph.SubscribersInfrastructure;

namespace Bespoke.Sph.TriggerSubscribers
{
   public class PenggunaRespondenProfileAndMembershipTriggerSubscriber: Subscriber<Bespoke.epsikologi_pengguna.Domain.Pengguna>
   {
  
        public override string QueueName
        {
            get { return "trigger_subs_pengguna-responden-profile-and-membership-"; }
        }

        public override string[] RoutingKeys
        {
            get { return new[] { "Pengguna.added.#" }; }
        }


        protected override async Task ProcessMessage(Bespoke.epsikologi_pengguna.Domain.Pengguna item, MessageHeaders header)
        {
            var trigger = @"{
  ""$type"": ""Bespoke.Sph.Domain.Trigger, domain.sph"",
  ""RuleCollection"": {
    ""$type"": ""Bespoke.Sph.Domain.ObjectCollection`1[[Bespoke.Sph.Domain.Rule, domain.sph]], domain.sph"",
    ""$values"": [
      {
        ""$type"": ""Bespoke.Sph.Domain.Rule, domain.sph"",
        ""Left"": {
          ""$type"": ""Bespoke.Sph.Domain.DocumentField, domain.sph"",
          ""XPath"": """",
          ""NamespacePrefix"": """",
          ""TypeName"": """",
          ""Path"": ""IsResponden"",
          ""Name"": ""IsResponden"",
          ""Note"": """",
          ""WebId"": ""dea6fd36-5e1a-4e1b-af0d-c37ad07706db""
        },
        ""Right"": {
          ""$type"": ""Bespoke.Sph.Domain.ConstantField, domain.sph"",
          ""TypeName"": ""System.Boolean, mscorlib"",
          ""Value"": true,
          ""Name"": ""true"",
          ""Note"": """",
          ""WebId"": ""ef985ac6-bec2-4231-edd5-6725c5cd00d8""
        },
        ""Operator"": ""Eq"",
        ""WebId"": ""562a5b1f-839d-4761-eeb6-2a0b67102084""
      }
    ]
  },
  ""ActionCollection"": {
    ""$type"": ""Bespoke.Sph.Domain.ObjectCollection`1[[Bespoke.Sph.Domain.CustomAction, domain.sph]], domain.sph"",
    ""$values"": [
      {
        ""$type"": ""Bespoke.Sph.Domain.StartWorkflowAction, domain.sph"",
        ""WorkflowDefinitionId"": ""creates-membership-for-new-responden"",
        ""Name"": """",
        ""Version"": 0,
        ""WorkflowTriggerMapCollection"": {
          ""$type"": ""Bespoke.Sph.Domain.ObjectCollection`1[[Bespoke.Sph.Domain.WorkflowTriggerMap, domain.sph]], domain.sph"",
          ""$values"": [
            {
              ""$type"": ""Bespoke.Sph.Domain.WorkflowTriggerMap, domain.sph"",
              ""VariablePath"": ""Pengguna"",
              ""Field"": {
                ""$type"": ""Bespoke.Sph.Domain.FunctionField, domain.sph"",
                ""CodeNamespace"": ""ff3c05ee21"",
                ""Script"": ""item"",
                ""Name"": ""item"",
                ""Note"": """",
                ""WebId"": ""3c05ee21-cfb7-43e4-cfd7-dc06e64147b3""
              },
              ""WebId"": ""e8e636db-e37a-455c-91be-fc49b54812be""
            }
          ]
        },
        ""UseAsync"": true,
        ""Title"": ""Creates membership for new responden"",
        ""IsActive"": true,
        ""TriggerId"": """",
        ""Note"": """",
        ""CustomActionId"": 0,
        ""UseCode"": false,
        ""WebId"": ""393956d1-8847-4097-98cf-2bbdf028e3c2""
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
  ""Name"": ""Responden profile and membership "",
  ""Entity"": ""Pengguna"",
  ""TypeOf"": ""Bespoke.epsikologi_pengguna.Domain.Pengguna, epsikologi.Pengguna"",
  ""Note"": """",
  ""IsActive"": true,
  ""IsFiredOnAdded"": true,
  ""IsFiredOnDeleted"": false,
  ""IsFiredOnChanged"": false,
  ""FiredOnOperations"": """",
  ""ClassName"": ""PenggunaRespondenProfileAndMembershipTriggerSubscriber"",
  ""CodeNamespace"": ""Bespoke.Sph.TriggerSubscribers"",
  ""CreatedBy"": null,
  ""Id"": ""pengguna-responden-profile-and-membership-"",
  ""CreatedDate"": ""0001-01-01T00:00:00"",
  ""ChangedBy"": ""admin"",
  ""ChangedDate"": ""2015-06-13T17:48:50.4033317+08:00"",
  ""WebId"": ""ae419759-6ae0-4c6e-ea9c-c7de9fd59a95""
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
