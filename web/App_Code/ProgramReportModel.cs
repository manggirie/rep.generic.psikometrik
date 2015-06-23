using System;
using System.Web.Mvc;
using System.Text;
using Bespoke.Sph.Domain;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using System.Collections.Concurrent;
using System.IO;
using System.Diagnostics;

namespace web.sph.App_Code
{

    public class ProgramReportModel
    {
        public int Siri {get;set;}
        public int Tahun {get;set;}
        public int Bil {get;set;}
        public string Ujian {get;set;}
        public string Program {get; set;}
    }
}