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

    public static class ConfigHelper
    {
    	public static void RegisterDependencies()
    	{
      		ObjectBuilder.AddCacheList<IRepository<Bespoke.epsikologi_permohonan.Domain.Permohonan>>(
                    new Bespoke.Sph.SqlRepository.SqlRepository<Bespoke.epsikologi_permohonan.Domain.Permohonan>());

            ObjectBuilder.AddCacheList<IRepository<Bespoke.epsikologi_ujian.Domain.Ujian>>(
                    new Bespoke.Sph.SqlRepository.SqlRepository<Bespoke.epsikologi_ujian.Domain.Ujian>());

            ObjectBuilder.AddCacheList<IRepository<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>>(
                    new Bespoke.Sph.SqlRepository.SqlRepository<Bespoke.epsikologi_sesiujian.Domain.SesiUjian>());

            ObjectBuilder.AddCacheList<IRepository<Bespoke.epsikologi_soalan.Domain.Soalan>>(
                    new Bespoke.Sph.SqlRepository.SqlRepository<Bespoke.epsikologi_soalan.Domain.Soalan>());

            ObjectBuilder.AddCacheList<IRepository<Bespoke.epsikologi_percubaansesi.Domain.PercubaanSesi>>(
                    new Bespoke.Sph.SqlRepository.SqlRepository<Bespoke.epsikologi_percubaansesi.Domain.PercubaanSesi>());

            ObjectBuilder.AddCacheList<IRepository<Bespoke.epsikologi_pengguna.Domain.Pengguna>>(
                    new Bespoke.Sph.SqlRepository.SqlRepository<Bespoke.epsikologi_pengguna.Domain.Pengguna>());

            ObjectBuilder.AddCacheList<IRepository<Bespoke.epsikologi_skorhlp.Domain.SkorHlp>>(
                    new Bespoke.Sph.SqlRepository.SqlRepository<Bespoke.epsikologi_skorhlp.Domain.SkorHlp>());

            ObjectBuilder.AddCacheList<IRepository<Bespoke.epsikologi_hlprecomendation.Domain.HlpRecomendation>>(
                    new Bespoke.Sph.SqlRepository.SqlRepository<Bespoke.epsikologi_hlprecomendation.Domain.HlpRecomendation>());

            ObjectBuilder.AddCacheList<IRepository<Bespoke.epsikologi_iprecommendation.Domain.IpRecommendation>>(
                    new Bespoke.Sph.SqlRepository.SqlRepository<Bespoke.epsikologi_iprecommendation.Domain.IpRecommendation>());

            ObjectBuilder.AddCacheList<IRepository<Bespoke.epsikologi_ibkkodkerjaya.Domain.IbkKodKerjaya>>(
                    new Bespoke.Sph.SqlRepository.SqlRepository<Bespoke.epsikologi_ibkkodkerjaya.Domain.IbkKodKerjaya>());

            ObjectBuilder.AddCacheList<IRepository<Bespoke.epsikologi_ibkrecommendation.Domain.IbkRecommendation>>(
                    new Bespoke.Sph.SqlRepository.SqlRepository<Bespoke.epsikologi_ibkrecommendation.Domain.IbkRecommendation>());

            ObjectBuilder.AddCacheList<IRepository<Bespoke.epsikologi_ipurecommendation.Domain.IpuRecommendation>>(
                    new Bespoke.Sph.SqlRepository.SqlRepository<Bespoke.epsikologi_ipurecommendation.Domain.IpuRecommendation>());

            ObjectBuilder.AddCacheList<IRepository<Bespoke.epsikologi_skoripu.Domain.SkorIPU>>(
                    new Bespoke.Sph.SqlRepository.SqlRepository<Bespoke.epsikologi_skoripu.Domain.SkorIPU>());

    	}
    }
}
