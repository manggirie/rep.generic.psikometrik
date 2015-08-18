using Bespoke.epsikologi_hlprecomendation.Domain;
using Bespoke.epsikologi_ibkkodkerjaya.Domain;
using Bespoke.epsikologi_ibkrecommendation.Domain;
using Bespoke.epsikologi_iprecommendation.Domain;
using Bespoke.epsikologi_ipupercentilenorms.Domain;
using Bespoke.epsikologi_ipurecommendation.Domain;
using Bespoke.epsikologi_isorecommendation.Domain;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.epsikologi_percubaansesi.Domain;
using Bespoke.epsikologi_permohonan.Domain;
using Bespoke.epsikologi_ppkprecommendation.Domain;
using Bespoke.epsikologi_sesiujian.Domain;
using Bespoke.epsikologi_skorhlp.Domain;
using Bespoke.epsikologi_skoripu.Domain;
using Bespoke.epsikologi_skorppkp.Domain;
using Bespoke.epsikologi_skorukbp.Domain;
using Bespoke.epsikologi_soalan.Domain;
using Bespoke.epsikologi_ujian.Domain;
using Bespoke.Sph.Domain;
using Bespoke.Sph.SqlRepository;

namespace web.sph.App_Code
{

    public static class ConfigHelper
    {
    	public static void RegisterDependencies()
    	{
      		ObjectBuilder.AddCacheList<IRepository<Permohonan>>(new SqlRepository<Permohonan>());
            ObjectBuilder.AddCacheList<IRepository<Ujian>>(new SqlRepository<Ujian>());
            ObjectBuilder.AddCacheList<IRepository<SesiUjian>>(new SqlRepository<SesiUjian>());
            ObjectBuilder.AddCacheList<IRepository<Soalan>>(new SqlRepository<Soalan>());
            ObjectBuilder.AddCacheList<IRepository<PercubaanSesi>>(new SqlRepository<PercubaanSesi>());
            ObjectBuilder.AddCacheList<IRepository<Pengguna>>(new SqlRepository<Pengguna>());

            ObjectBuilder.AddCacheList<IRepository<SkorHlp>>(new SqlRepository<SkorHlp>());
            ObjectBuilder.AddCacheList<IRepository<HlpRecomendation>>(new SqlRepository<HlpRecomendation>());

            ObjectBuilder.AddCacheList<IRepository<IpRecommendation>>(new SqlRepository<IpRecommendation>());

            ObjectBuilder.AddCacheList<IRepository<IbkKodKerjaya>>(new SqlRepository<IbkKodKerjaya>());
            ObjectBuilder.AddCacheList<IRepository<IbkRecommendation>>(new SqlRepository<IbkRecommendation>());

            ObjectBuilder.AddCacheList<IRepository<IpuRecommendation>>( new SqlRepository<IpuRecommendation>());
            ObjectBuilder.AddCacheList<IRepository<SkorIPU>>( new SqlRepository<SkorIPU>());
            ObjectBuilder.AddCacheList<IRepository<IpuPercentileNorms>>( new SqlRepository<IpuPercentileNorms>());

            ObjectBuilder.AddCacheList<IRepository<PpkpRecommendation>>(new SqlRepository<PpkpRecommendation>());
            ObjectBuilder.AddCacheList<IRepository<SkorPpkp>>(new SqlRepository<SkorPpkp>());

            ObjectBuilder.AddCacheList<IRepository<IsoRecommendation>>(new SqlRepository<IsoRecommendation>());
            ObjectBuilder.AddCacheList<IRepository<SkorUkbp>>(new SqlRepository<SkorUkbp>());
            
    	}
    }
}
