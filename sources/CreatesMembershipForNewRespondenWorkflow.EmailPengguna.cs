using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;

namespace Bespoke.Sph.Workflows_CreatesMembershipForNewResponden_0
{
    public partial class CreatesMembershipForNewRespondenWorkflow
    {



        private async Task<string> TransformFromEmailPenggunaAsync(string template)
        {

            var razor = ObjectBuilder.GetObject<ITemplateEngine>();
            return await razor.GenerateAsync(template, this).ConfigureAwait(false);
        }


        private async Task<string> TransformToEmailPenggunaAsync(string template)
        {

            var razor = ObjectBuilder.GetObject<ITemplateEngine>();
            return await razor.GenerateAsync(template, this).ConfigureAwait(false);
        }


        private async Task<string> TransformSubjectEmailPenggunaAsync(string template)
        {

            var razor = ObjectBuilder.GetObject<ITemplateEngine>();
            return await razor.GenerateAsync(template, this).ConfigureAwait(false);
        }


        private async Task<string> TransformBodyEmailPenggunaAsync(string template)
        {

            var razor = ObjectBuilder.GetObject<ITemplateEngine>();
            return await razor.GenerateAsync(template, this).ConfigureAwait(false);
        }

    }
}
