using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_ujian.Domain
{
    public class Ujian : Entity
    {
        public Ujian()
        {
            var rc = new RuleContext(this);
        }

        public override string ToString()
        {
            return "Ujian:" + UjianNo;
        }       //member:UjianNo
        [XmlAttribute]
        public string UjianNo { get; set; }

        //member:NamaUjian
        [XmlAttribute]
        public string NamaUjian { get; set; }

    }
}
