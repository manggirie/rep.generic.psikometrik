using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_pendaftaranprogram.Domain
{
    public class PendaftaranProgram : Entity
    {
        public PendaftaranProgram()
        {
            var rc = new RuleContext(this);
        }

        public override string ToString()
        {
            return "PendaftaranProgram:" + PendaftaranNo;
        }       //member:PendaftaranNo
        [XmlAttribute]
        public string PendaftaranNo { get; set; }

        //member:NamaProgram
        [XmlAttribute]
        public string NamaProgram { get; set; }

        //member:NamaPengguna
        [XmlAttribute]
        public string NamaPengguna { get; set; }

        //member:TarikhDaftar
        [XmlAttribute]
        public DateTime TarikhDaftar { get; set; }

        //member:MyKad
        [XmlAttribute]
        public string MyKad { get; set; }

        //member:NoPermohonan
        [XmlAttribute]
        public string NoPermohonan { get; set; }

    }
}
