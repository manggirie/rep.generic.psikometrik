using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_sesiujian.Domain
{
    public class Jawapan : DomainObject
    {
        public Jawapan()
        {
        }
        [XmlAttribute]
        public string SoalanNo { get; set; }

        [XmlAttribute]
        public string JawapanPilihan { get; set; }

        [XmlAttribute]
        public string Trait { get; set; }

        [XmlAttribute]
        public string SeksyenSoalan { get; set; }

        [XmlAttribute]
        public int Nilai { get; set; }

    }






}
