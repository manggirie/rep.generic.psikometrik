using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;

namespace Bespoke.Sph.Workflows_PendaftaranProgramSessiUjian_0
{
    public partial class PendaftaranProgramSessiUjianWorkflow
    {



        private async Task<string> TransformFromEmailRespondenAsync(string template)
        {

            var razor = ObjectBuilder.GetObject<ITemplateEngine>();
            return await razor.GenerateAsync(template, this).ConfigureAwait(false);
        }


        private async Task<string> TransformToEmailRespondenAsync(string template)
        {

            var razor = ObjectBuilder.GetObject<ITemplateEngine>();
            return await razor.GenerateAsync(template, this).ConfigureAwait(false);
        }


        private async Task<string> TransformSubjectEmailRespondenAsync(string template)
        {

            var razor = ObjectBuilder.GetObject<ITemplateEngine>();
            return await razor.GenerateAsync(template, this).ConfigureAwait(false);
        }


        private async Task<string> TransformBodyEmailRespondenAsync(string template)
        {

            var razor = ObjectBuilder.GetObject<ITemplateEngine>();
            return await razor.GenerateAsync(template, this).ConfigureAwait(false);
        }

    }
}
