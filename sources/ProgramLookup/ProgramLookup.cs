using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_programlookup.Domain
{
    public class ProgramLookup : Entity
    {
        public ProgramLookup()
        {
            var rc = new RuleContext(this);
        }

        public override string ToString()
        {
            return "ProgramLookup:" + ProgramNo;
        }       //member:ProgramNo
        [XmlAttribute]
        public string ProgramNo { get; set; }

        //member:NamaProgram
        [XmlAttribute]
        public string NamaProgram { get; set; }

    }
}
