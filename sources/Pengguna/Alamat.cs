using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_pengguna.Domain
{
    public class Alamat : DomainObject
    {
        public Alamat()
        {
        }
        [XmlAttribute]
        public string Alamat1 { get; set; }

        [XmlAttribute]
        public string Alamat2 { get; set; }

        [XmlAttribute]
        public string Alamat3 { get; set; }

        [XmlAttribute]
        public string Poskod { get; set; }

        [XmlAttribute]
        public string Bandar { get; set; }

        [XmlAttribute]
        public string Negeri { get; set; }

    }







}
