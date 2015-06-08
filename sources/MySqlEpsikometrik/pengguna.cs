using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Xml.Serialization;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Net;
using System.Net.Http;

namespace epsikologi.Adapters.epsikometrik.MySqlEpsikometrik
{
   public class pengguna : DomainObject
   {
     
        public override string ToString()
        {
            return "pengguna:" + nokp;
        }       //member:nokp
      [XmlAttribute]
      public string nokp{get;set;}

       //member:nama
      [XmlAttribute]
      public string nama{get;set;}

       //member:katalaluan
      [XmlAttribute]
      public string katalaluan{get;set;}

       //member:_kod_pekerjaan
      [XmlAttribute]
      public int _kod_pekerjaan{get;set;}

       //member:_kod_jawatan
      [XmlAttribute]
      public int _kod_jawatan{get;set;}

       //member:_kod_klasifikasi
      [XmlAttribute]
      public string _kod_klasifikasi{get;set;}

       //member:_kod_skim
      [XmlAttribute]
      public string _kod_skim{get;set;}

       //member:_kod_gred
      [XmlAttribute]
      public string _kod_gred{get;set;}

       //member:_kod_agensi
      [XmlAttribute]
      public string _kod_agensi{get;set;}

       //member:_kod_jantina
      [XmlAttribute]
      public string _kod_jantina{get;set;}

       //member:_kod_kahwin
      [XmlAttribute]
      public int _kod_kahwin{get;set;}

       //member:_kod_warganegara
      [XmlAttribute]
      public int _kod_warganegara{get;set;}

       //member:_kod_umur
      [XmlAttribute]
      public int _kod_umur{get;set;}

       //member:_kod_saraan
      [XmlAttribute]
      public string _kod_saraan{get;set;}

       //member:_kod_pengguna
      [XmlAttribute]
      public int _kod_pengguna{get;set;}

       //member:_kod_status_aktif
      [XmlAttribute]
      public int _kod_status_aktif{get;set;}

       //member:catatan_status_aktif
      [XmlAttribute]
      public string catatan_status_aktif{get;set;}

       //member:tarikh_daftar
      [XmlAttribute]
      public DateTime tarikh_daftar{get;set;}

       //member:tarikh_kemaskini
      [XmlAttribute]
      public DateTime tarikh_kemaskini{get;set;}

   }
}
