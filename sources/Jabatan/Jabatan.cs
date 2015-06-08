using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_jabatan.Domain
{
    public class Jabatan : Entity
    {
        public Jabatan()
        {
            var rc = new RuleContext(this);
        }

        public override string ToString()
        {
            return "Jabatan:" + JabatanNo;
        }       //member:JabatanNo
        [XmlAttribute]
        public string JabatanNo { get; set; }

        //member:NamaJabatan
        [XmlAttribute]
        public string NamaJabatan { get; set; }

        //member:AgensiNo
        [XmlAttribute]
        public string AgensiNo { get; set; }

        //member:Kementerian
        [XmlAttribute]
        public string Kementerian { get; set; }

        //member:AgensiKumpulan
        [XmlAttribute]
        public string AgensiKumpulan { get; set; }

    }
}
