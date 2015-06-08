using Bespoke.Sph.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Xml.Serialization;
using System.Web.Mvc;
using Bespoke.Sph.Web.Helpers;
using epsikologi.Adapters.epsikometrik.MySqlEpsikometrik;

namespace epsikologi.Adapters.epsikometrik.MySqlEpsikometrik
{
   public partial class MySqlEpsikometrik
   {
      public string ConnectionString{set;get;}
   }
}
