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

namespace epsikologi.Adapters.dbo.TEST
{
   public class tblPRCOBiodata : DomainObject
   {
     
        public override string ToString()
        {
            return "tblPRCOBiodata:" + COBiodataID;
        }       //member:COBumiStatus
      public bool? COBumiStatus{get;set;}

       //member:EmailNotifyInd
      public bool? EmailNotifyInd{get;set;}

       //member:ReligionCd
      [XmlAttribute]
      public string ReligionCd{get;set;}

       //member:RaceCd
      [XmlAttribute]
      public string RaceCd{get;set;}

       //member:EthnicCd
      [XmlAttribute]
      public string EthnicCd{get;set;}

       //member:ArmyPoliceCd
      [XmlAttribute]
      public string ArmyPoliceCd{get;set;}

       //member:BloodTypeCd
      [XmlAttribute]
      public string BloodTypeCd{get;set;}

       //member:MrtlStatusCd
      [XmlAttribute]
      public string MrtlStatusCd{get;set;}

       //member:NatStatusCd
      [XmlAttribute]
      public string NatStatusCd{get;set;}

       //member:COHPhoneStatus
      [XmlAttribute]
      public string COHPhoneStatus{get;set;}

       //member:COUpdtInd
      [XmlAttribute]
      public string COUpdtInd{get;set;}

       //member:EIICd
      [XmlAttribute]
      public string EIICd{get;set;}

       //member:COOrigTrtry
      [XmlAttribute]
      public string COOrigTrtry{get;set;}

       //member:TitleCd
      [XmlAttribute]
      public string TitleCd{get;set;}

       //member:HighestEduLevelCd
      [XmlAttribute]
      public string HighestEduLevelCd{get;set;}

       //member:GenderCd
      [XmlAttribute]
      public string GenderCd{get;set;}

       //member:COBirthPlaceCd
      [XmlAttribute]
      public string COBirthPlaceCd{get;set;}

       //member:COBirthCountryCd
      [XmlAttribute]
      public string COBirthCountryCd{get;set;}

       //member:NatCd
      [XmlAttribute]
      public string NatCd{get;set;}

       //member:COBirthDt
      public DateTime? COBirthDt{get;set;}

       //member:COLastUpdtDt
      public DateTime? COLastUpdtDt{get;set;}

       //member:COPhoto
      public System.Byte[]? COPhoto{get;set;}

       //member:COBiodataID
      [XmlAttribute]
      public int COBiodataID{get;set;}

       //member:COID
      public int? COID{get;set;}

       //member:CONm
      [XmlAttribute]
      public string CONm{get;set;}

       //member:COEmail
      [XmlAttribute]
      public string COEmail{get;set;}

       //member:COOldID
      [XmlAttribute]
      public string COOldID{get;set;}

       //member:COBirthCertNo
      [XmlAttribute]
      public string COBirthCertNo{get;set;}

       //member:COHPhoneNo
      [XmlAttribute]
      public string COHPhoneNo{get;set;}

       //member:COOffTelNo
      [XmlAttribute]
      public string COOffTelNo{get;set;}

       //member:COOffTelNoExtn
      [XmlAttribute]
      public string COOffTelNoExtn{get;set;}

       //member:APCrtdBy
      [XmlAttribute]
      public string APCrtdBy{get;set;}

   }
}
