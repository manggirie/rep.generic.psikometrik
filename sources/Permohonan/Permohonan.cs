using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_permohonan.Domain
{
    public class Permohonan : Entity
    {
        public Permohonan()
        {
            var rc = new RuleContext(this);
        }

        public override string ToString()
        {
            return "Permohonan:" + PermohonanNo;
        }       //member:CatatanProgram
        [XmlAttribute]
        public string CatatanProgram { get; set; }

        //member:BilRespondan
        [XmlAttribute]
        public int BilRespondan { get; set; }

        //member:BilProgram
        public int? BilProgram { get; set; }

        //member:SiriProgram
        public int? SiriProgram { get; set; }

        //member:TahunProgram
        [XmlAttribute]
        public string TahunProgram { get; set; }

        //member:NamaProgram
        [XmlAttribute]
        public string NamaProgram { get; set; }

        //member:MaklumatUjian
        [XmlAttribute]
        public string MaklumatUjian { get; set; }

        //member:TarikhMula
        [XmlAttribute]
        public DateTime TarikhMula { get; set; }

        //member:TarikhTamat
        [XmlAttribute]
        public DateTime TarikhTamat { get; set; }

        //member:TujuanProgram
        [XmlAttribute]
        public string TujuanProgram { get; set; }

        //member:isIBK
        [XmlAttribute]
        public bool isIBK { get; set; }

        //member:isISO
        [XmlAttribute]
        public bool isISO { get; set; }

        //member:isIP
        [XmlAttribute]
        public bool isIP { get; set; }

        //member:isIPU
        [XmlAttribute]
        public bool isIPU { get; set; }

        //member:isUKHLP
        [XmlAttribute]
        public bool isUKHLP { get; set; }

        //member:isUKBP
        [XmlAttribute]
        public bool isUKBP { get; set; }

        //member:isPPKP
        [XmlAttribute]
        public bool isPPKP { get; set; }

        //member:JenisPermohonan
        [XmlAttribute]
        public string JenisPermohonan { get; set; }

        //member:StatusPermohonan
        [XmlAttribute]
        public string StatusPermohonan { get; set; }

        //member:Responden
        [XmlAttribute]
        public string Responden { get; set; }

        //member:PermohonanNo
        [XmlAttribute]
        public string PermohonanNo { get; set; }

    }
}
