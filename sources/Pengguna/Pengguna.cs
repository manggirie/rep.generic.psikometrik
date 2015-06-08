using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_pengguna.Domain
{
    public class Pengguna : Entity
    {
        public Pengguna()
        {
            var rc = new RuleContext(this);
            this.Alamat = new Alamat();
        }

        public override string ToString()
        {
            return "Pengguna:" + MyKad;
        }       //member:Nama
        [XmlAttribute]
        public string Nama { get; set; }

        //member:MyKad
        [XmlAttribute]
        public string MyKad { get; set; }

        //member:IsUrusetia
        [XmlAttribute]
        public bool IsUrusetia { get; set; }

        //member:IsPenyelaras
        [XmlAttribute]
        public bool IsPenyelaras { get; set; }

        //member:IsResponden
        [XmlAttribute]
        public bool IsResponden { get; set; }

        //member:Jantina
        [XmlAttribute]
        public string Jantina { get; set; }

        //member:StatusPerkahwinan
        [XmlAttribute]
        public string StatusPerkahwinan { get; set; }

        //member:Pekerjaan
        [XmlAttribute]
        public string Pekerjaan { get; set; }

        //member:Jawatan
        [XmlAttribute]
        public string Jawatan { get; set; }

        //member:JenisPerkhidmatan
        [XmlAttribute]
        public string JenisPerkhidmatan { get; set; }

        //member:Skim
        [XmlAttribute]
        public string Skim { get; set; }

        //member:Gred
        [XmlAttribute]
        public string Gred { get; set; }

        //member:Agensi
        [XmlAttribute]
        public string Agensi { get; set; }

        //member:Warganegara
        [XmlAttribute]
        public string Warganegara { get; set; }

        //member:KumpulanUmur
        [XmlAttribute]
        public string KumpulanUmur { get; set; }

        //member:Saraan
        [XmlAttribute]
        public string Saraan { get; set; }

        //member:Alamat
        public Alamat Alamat { get; set; }

        //member:Emel
        [XmlAttribute]
        public string Emel { get; set; }

        //member:Telefon
        [XmlAttribute]
        public string Telefon { get; set; }

        //member:Fax
        [XmlAttribute]
        public string Fax { get; set; }

        //member:TarikhDaftar
        [XmlAttribute]
        public DateTime TarikhDaftar { get; set; }

        //member:TarikhKemaskini
        [XmlAttribute]
        public DateTime TarikhKemaskini { get; set; }

        //member:KumpulanJawatan
        [XmlAttribute]
        public string KumpulanJawatan { get; set; }

        //member:NegeriBekerja
        [XmlAttribute]
        public string NegeriBekerja { get; set; }

    }
}
