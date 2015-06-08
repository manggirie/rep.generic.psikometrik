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
   public class permohonan : DomainObject
   {
     
        public override string ToString()
        {
            return "permohonan:" + ID;
        }       //member:ID
      [XmlAttribute]
      public int ID{get;set;}

       //member:nama_program
      [XmlAttribute]
      public string nama_program{get;set;}

       //member:catatan_program
      [XmlAttribute]
      public string catatan_program{get;set;}

       //member:bil_responden
      [XmlAttribute]
      public int bil_responden{get;set;}

       //member:bil_program
      [XmlAttribute]
      public int bil_program{get;set;}

       //member:siri_program
      [XmlAttribute]
      public int siri_program{get;set;}

       //member:tahun_program
      [XmlAttribute]
      public int tahun_program{get;set;}

       //member:capaian_ujian
      [XmlAttribute]
      public string capaian_ujian{get;set;}

       //member:jabatan
      [XmlAttribute]
      public string jabatan{get;set;}

       //member:_kod_agensi
      [XmlAttribute]
      public string _kod_agensi{get;set;}

       //member:_kod_permohonan
      [XmlAttribute]
      public int _kod_permohonan{get;set;}

       //member:_kod_permohonan_status
      [XmlAttribute]
      public int _kod_permohonan_status{get;set;}

       //member:_kod_permohonan_justifikasi
      [XmlAttribute]
      public int _kod_permohonan_justifikasi{get;set;}

       //member:_kod_tadbiran
      [XmlAttribute]
      public int _kod_tadbiran{get;set;}

       //member:_kod_ujian_status
      [XmlAttribute]
      public int _kod_ujian_status{get;set;}

       //member:tarikh_mula
      [XmlAttribute]
      public string tarikh_mula{get;set;}

       //member:tarikh_tamat
      [XmlAttribute]
      public string tarikh_tamat{get;set;}

       //member:tarikh_daftar
      [XmlAttribute]
      public DateTime tarikh_daftar{get;set;}

       //member:tarikh_lulus
      [XmlAttribute]
      public DateTime tarikh_lulus{get;set;}

       //member:tarikh_kemaskini
      [XmlAttribute]
      public DateTime tarikh_kemaskini{get;set;}

       //member:pengguna
      [XmlAttribute]
      public string pengguna{get;set;}

   }
}
