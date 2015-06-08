using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_soalan.Domain
{
    public class Soalan : Entity
    {
        public Soalan()
        {
            var rc = new RuleContext(this);
        }

        public override string ToString()
        {
            return "Soalan:" + NoRujukan;
        }       //member:NoRujukan
        [XmlAttribute]
        public string NoRujukan { get; set; }

        //member:SoalanNo
        [XmlAttribute]
        public string SoalanNo { get; set; }

        //member:TeksSoalan
        [XmlAttribute]
        public string TeksSoalan { get; set; }

        //member:SeksyenSoalan
        [XmlAttribute]
        public string SeksyenSoalan { get; set; }

        //member:PilihanJawapanCollection
        private readonly ObjectCollection<PilihanJawapan> m_pilihanJawapanCollection = new ObjectCollection<PilihanJawapan>();
        public ObjectCollection<PilihanJawapan> PilihanJawapanCollection
        {
            get { return m_pilihanJawapanCollection; }
        }

        //member:Trait
        [XmlAttribute]
        public string Trait { get; set; }

        //member:NamaUjian
        [XmlAttribute]
        public string NamaUjian { get; set; }

    }
}
