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
   public class program : DomainObject
   {
     
        public override string ToString()
        {
            return "program:" + ID;
        }       //member:ID
      [XmlAttribute]
      public int ID{get;set;}

       //member:nama_program
      [XmlAttribute]
      public string nama_program{get;set;}

       //member:tarikh_wujud
      [XmlAttribute]
      public DateTime tarikh_wujud{get;set;}

       //member:tarikh_kemaskini
      [XmlAttribute]
      public DateTime tarikh_kemaskini{get;set;}

       //member:pengguna
      [XmlAttribute]
      public string pengguna{get;set;}

   }
}
