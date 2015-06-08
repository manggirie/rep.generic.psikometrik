using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;
using epsikologi.Adapters.dbo.MsSqlSupat;

namespace epsikologi.Adapters.dbo.MsSqlSupat
{
    public partial class MsSqlSupat
    {
        public string ConnectionString { set; get; }
    }
}
