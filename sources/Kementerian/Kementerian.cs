using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_kementerian.Domain
{
    public class Kementerian : Entity
    {
        public Kementerian()
        {
            var rc = new RuleContext(this);
        }

        public override string ToString()
        {
            return "Kementerian:" + KementerianNo;
        }       //member:KementerianNo
        [XmlAttribute]
        public string KementerianNo { get; set; }

        //member:NamaKementerian
        [XmlAttribute]
        public string NamaKementerian { get; set; }

        //member:Negeri
        [XmlAttribute]
        public string Negeri { get; set; }

        //member:KumpulanAgensi
        [XmlAttribute]
        public string KumpulanAgensi { get; set; }

    }
}
