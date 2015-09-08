using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bespoke.epsikologi_pengguna.Domain;
using Bespoke.Sph.Domain;

namespace Hrmis.Adapter
{
    public class HrmisAdapter
    {
        public async Task<Pengguna> GetUserDetailsByIcNo(string icno)
        {
            var sql = @"SELECT * FROM VCO WHERE ICNO = @IcNo";

            using (var conn = new SqlConnection(this.ConnectionString))
            using (var cmd = new SqlCommand(sql,conn))
            {
                await conn.OpenAsync();
                cmd.Parameters.AddWithValue("IcNo", icno);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var pengguna = new Pengguna();
                        pengguna.Nama = reader["CONm"].ReadNullableString();
                        pengguna.Emel = reader["COEmail"].ReadNullableString();
                        pengguna.StatusPerkahwinan = reader["MrtlStatus"].ReadNullableString();
                        pengguna.Telefon = reader["COHPhoneNoRasmi"].ReadNullableString();
                        pengguna.Jantina = reader["Gender"].ReadNullableString();
                        pengguna.Bahagian = reader["ServGrpTitle"].ReadNullableString();
                        return pengguna;
                    }

                    return null;
                }
              

            }
        }

        public string ConnectionString
        {
            get
            {
                var conn = System.Configuration.ConfigurationManager.ConnectionStrings["hrmis"];
                if (null != conn) return conn.ConnectionString;
                return @"Server=ketupat;Database=DBHRMISLIVE;User Id=hrmis;Password=ghm171209isr;";
            }
        }
    }
}
