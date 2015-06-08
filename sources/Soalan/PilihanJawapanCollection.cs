using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_soalan.Domain
{
    public class PilihanJawapan : DomainObject
    {
        public PilihanJawapan()
        {
        }
        [XmlAttribute]
        public string Teks { get; set; }

        [XmlAttribute]
        public int Nilai { get; set; }

    }



}
