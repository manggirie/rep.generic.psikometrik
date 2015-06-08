using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_tblprsideincome.Domain
{
    public class Testdata : DomainObject
    {
        public Testdata()
        {
        }
        [XmlAttribute]
        public string NameMe { get; set; }

    }


}
