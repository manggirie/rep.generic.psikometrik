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

namespace epsikologi.Adapters.dbo.MsSqlSupat
{
   public class Tbl_KodJabatan : DomainObject
   {
     
        public override string ToString()
        {
            return "Tbl_KodJabatan:" + "";
        }       //member:Kod_Jabatan
      [XmlAttribute]
      public string Kod_Jabatan{get;set;}

       //member:Kod_Kementerian
      [XmlAttribute]
      public string Kod_Kementerian{get;set;}

       //member:Jenis_Agensi
      [XmlAttribute]
      public string Jenis_Agensi{get;set;}

       //member:Sektor
      [XmlAttribute]
      public string Sektor{get;set;}

       //member:Jabatan
      [XmlAttribute]
      public string Jabatan{get;set;}

       //member:Singkatan
      [XmlAttribute]
      public string Singkatan{get;set;}

       //member:Alamat_Jabatan2
      [XmlAttribute]
      public string Alamat_Jabatan2{get;set;}

       //member:Alamat_Jabatan3
      [XmlAttribute]
      public string Alamat_Jabatan3{get;set;}

       //member:Poskod
      [XmlAttribute]
      public string Poskod{get;set;}

       //member:No_Telefon
      [XmlAttribute]
      public string No_Telefon{get;set;}

       //member:No_Fax
      [XmlAttribute]
      public string No_Fax{get;set;}

       //member:Jab_BPO
      [XmlAttribute]
      public string Jab_BPO{get;set;}

       //member:Department
      [XmlAttribute]
      public string Department{get;set;}

       //member:No_KPKetua
      [XmlAttribute]
      public string No_KPKetua{get;set;}

       //member:No_KPPenyelaras
      [XmlAttribute]
      public string No_KPPenyelaras{get;set;}

       //member:Ketua_Jabatan
      [XmlAttribute]
      public string Ketua_Jabatan{get;set;}

       //member:Head_Dept
      [XmlAttribute]
      public string Head_Dept{get;set;}

       //member:Alamat_Jabatan1
      [XmlAttribute]
      public string Alamat_Jabatan1{get;set;}

   }
}
