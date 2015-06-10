using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_sesiujian.Domain
{
    public class SesiUjian : Entity
    {
        public SesiUjian()
        {
            var rc = new RuleContext(this);
        }

        public override string ToString()
        {
            return "SesiUjian:" + SesiNo;
        }       //member:SesiNo
        [XmlAttribute]
        public string SesiNo { get; set; }

        //member:NamaUjian
        [XmlAttribute]
        public string NamaUjian { get; set; }

        //member:NamaPengguna
        [XmlAttribute]
        public string NamaPengguna { get; set; }

        //member:NamaProgram
        [XmlAttribute]
        public string NamaProgram { get; set; }

        //member:TarikhUjian
        public DateTime? TarikhUjian { get; set; }

        //member:MyKad
        [XmlAttribute]
        public string MyKad { get; set; }

        //member:MasaMula
        public DateTime? MasaMula { get; set; }

        //member:MasaTamat
        public DateTime? MasaTamat { get; set; }

        //member:JawapanCollection
        private readonly ObjectCollection<Jawapan> m_jawapanCollection = new ObjectCollection<Jawapan>();
        public ObjectCollection<Jawapan> JawapanCollection
        {
            get { return m_jawapanCollection; }
        }

        //member:Status
        [XmlAttribute]
        public string Status { get; set; }

    }
}
