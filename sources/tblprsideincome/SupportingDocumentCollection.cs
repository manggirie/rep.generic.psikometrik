using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_tblprsideincome.Domain
{
    public class SupportingDocument : DomainObject
    {
        public SupportingDocument()
        {
        }
        [XmlAttribute]
        public string storeid { get; set; }

        [XmlAttribute]
        public string description { get; set; }

        [XmlAttribute]
        public string title { get; set; }

    }




}
