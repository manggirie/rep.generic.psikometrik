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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;


namespace web.sph.App_Code
{
    public class HlpTraitViewModel
    {
      private Bespoke.epsikologi_sesiujian.Domain.SesiUjian m_sesi;
      public HlpTraitViewModel(Bespoke.epsikologi_sesiujian.Domain.SesiUjian sesi)
      {
        // Skor Rendah
        // Skor Sederhana Rendah
        // Skor Sedarhana Tinggi
        // Skor Tinggi
          m_sesi = sesi;
      }

      public int KB { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "KB").Sum(a => a.Nilai); } }
      public int FR { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "FR").Sum(a => a.Nilai); } }
      public int KT { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "KT").Sum(a => a.Nilai); } }
      public int KC { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "KC").Sum(a => a.Nilai); } }
      public int LP { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "LP").Sum(a => a.Nilai); } }
      public int AS { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "AS").Sum(a => a.Nilai); } }
      public int AF { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "AF").Sum(a => a.Nilai); } }
      public int TL { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "TL").Sum(a => a.Nilai); } }
      public int SM { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "SM").Sum(a => a.Nilai); } }
      public int DT { get{ return m_sesi.JawapanCollection.Where(a => a.Trait == "DT").Sum(a => a.Nilai); } }
    }
}
