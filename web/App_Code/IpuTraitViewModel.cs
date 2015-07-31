using System;
using System.Web.Mvc;
using System.Text;
using Bespoke.Sph.Domain;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;


namespace web.sph.App_Code
{
    public class IpuTraitViewModel
    {

      public IpuTraitViewModel(Bespoke.epsikologi_sesiujian.Domain.SesiUjian sesi)
      {
           Sesi = sesi;
      }

      public int A
      {
        get
        {
          return  this.Sesi.JawapanCollection.Where(a => a.Trait == "A").Sum(a => a.Nilai);
        }
      }

      public int B
      {
        get
        {
          return  this.Sesi.JawapanCollection.Where(a => a.Trait == "B").Sum(a => a.Nilai);
        }
      }

      public int C
      {
        get
        {
          return  this.Sesi.JawapanCollection.Where(a => a.Trait == "C").Sum(a => a.Nilai);
        }
      }
      public int D
      {
        get
        {
          return  this.Sesi.JawapanCollection.Where(a => a.Trait == "D").Sum(a => a.Nilai);
        }
      }
      public int E
      {
        get
        {
          return  this.Sesi.JawapanCollection.Where(a => a.Trait == "E").Sum(a => a.Nilai);
        }
      }
      public int F
      {
        get
        {
          return  this.Sesi.JawapanCollection.Where(a => a.Trait == "F").Sum(a => a.Nilai);
        }
      }
      public int G
      {
        get
        {
          return  this.Sesi.JawapanCollection.Where(a => a.Trait == "G").Sum(a => a.Nilai);
        }
      }
      public int H
      {
        get
        {
          return  this.Sesi.JawapanCollection.Where(a => a.Trait == "H").Sum(a => a.Nilai);
        }
      }
      public int I
      {
        get
        {
          return  this.Sesi.JawapanCollection.Where(a => a.Trait == "I").Sum(a => a.Nilai);
        }
      }
      public int J
      {
        get
        {
          return  this.Sesi.JawapanCollection.Where(a => a.Trait == "J").Sum(a => a.Nilai);
        }
      }

      [JsonIgnore]
      public Bespoke.epsikologi_sesiujian.Domain.SesiUjian Sesi { get; set; }
      public override string ToString()
      {
          var setting = new JsonSerializerSettings();
         setting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
         setting.Formatting = Formatting.Indented;
         return JsonConvert.SerializeObject(this, setting);
      }
    }
}