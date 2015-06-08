using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;

namespace Bespoke.epsikologi_tblprsideincome.Domain
{
    public class tblprsideincome : Entity
    {
        public tblprsideincome()
        {
            var rc = new RuleContext(this);
            this.changes = new changes();
            this.Testdata = new Testdata();
        }

        public override string ToString()
        {
            return "tblprsideincome:" + sideincomeid;
        }       //member:sideincomeid
        [XmlAttribute]
        public int sideincomeid { get; set; }

        //member:sidesourcecd
        [XmlAttribute]
        public string sidesourcecd { get; set; }

        //member:sidetail
        [XmlAttribute]
        public string sidetail { get; set; }

        //member:cobiodataid
        public int? cobiodataid { get; set; }

        //member:siamt
        public decimal? siamt { get; set; }

        //member:sitypecd
        [XmlAttribute]
        public string sitypecd { get; set; }

        //member:recurringcd
        [XmlAttribute]
        public string recurringcd { get; set; }

        //member:changes
        public changes changes { get; set; }

        //member:modifiedby
        public int? modifiedby { get; set; }

        //member:modifieddt
        public DateTime? modifieddt { get; set; }

        //member:SupportingDocumentCollection
        private readonly ObjectCollection<SupportingDocument> m_supportingDocumentCollection = new ObjectCollection<SupportingDocument>();
        public ObjectCollection<SupportingDocument> SupportingDocumentCollection
        {
            get { return m_supportingDocumentCollection; }
        }

        //member:Testdata
        public Testdata Testdata { get; set; }

    }
}
