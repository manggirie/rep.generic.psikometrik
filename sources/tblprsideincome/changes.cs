using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_tblprsideincome.Domain
{
    public class changes : DomainObject
    {
        public changes()
        {
        }
        [XmlAttribute]
        public int sideincomeid { get; set; }

        [XmlAttribute]
        public string sidesourcecd { get; set; }

        [XmlAttribute]
        public string sidetail { get; set; }

        public int? cobiodataid { get; set; }

        public decimal? siamt { get; set; }

        [XmlAttribute]
        public string sitypecd { get; set; }

        [XmlAttribute]
        public string recurringcd { get; set; }

        public int? trxid { get; set; }

        public int? modifiedby { get; set; }

        [XmlAttribute]
        public string modifieddt { get; set; }

        [XmlAttribute]
        public string reason { get; set; }

        private readonly ObjectCollection<ChangesSupportingDocument> m_changesSupportingDocumentCollection = new ObjectCollection<ChangesSupportingDocument>();
        public ObjectCollection<ChangesSupportingDocument> ChangesSupportingDocumentCollection
        {
            get { return m_changesSupportingDocumentCollection; }
        }

        [XmlAttribute]
        public string trxtype { get; set; }

    }











    public class ChangesSupportingDocument : DomainObject
    {
        public ChangesSupportingDocument()
        {
        }
        [XmlAttribute]
        public string storeid { get; set; }

        [XmlAttribute]
        public string description { get; set; }

        [XmlAttribute]
        public string title { get; set; }

    }






}
