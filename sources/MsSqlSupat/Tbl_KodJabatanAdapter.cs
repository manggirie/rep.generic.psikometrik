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
    public class Tbl_KodJabatanAdapter
    {
        public async Task<T> ExecuteScalarAsync<T>(string sql)
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                await conn.OpenAsync();
                var dbval = await cmd.ExecuteScalarAsync();
                if (dbval == System.DBNull.Value)
                    return default(T);
                return (T)dbval;
            }
        }

        public async Task<int> DeleteAsync()
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            using (var cmd = new SqlCommand(@"DELETE FROM [dbo].[Tbl_KodJabatan] WHERE

", conn))
            {
                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<object> InsertAsync(Tbl_KodJabatan item)
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            using (var cmd = new SqlCommand(@"INSERT INTO [dbo].[Tbl_KodJabatan] ([Kod_Jabatan],
[Kod_Kementerian],
[Jenis_Agensi],
[Sektor],
[Jabatan],
[Singkatan],
[Alamat_Jabatan2],
[Alamat_Jabatan3],
[Poskod],
[No_Telefon],
[No_Fax],
[Jab_BPO],
[Department],
[No_KPKetua],
[No_KPPenyelaras],
[Ketua_Jabatan],
[Head_Dept],
[Alamat_Jabatan1]
)
VALUES(
@Kod_Jabatan,
@Kod_Kementerian,
@Jenis_Agensi,
@Sektor,
@Jabatan,
@Singkatan,
@Alamat_Jabatan2,
@Alamat_Jabatan3,
@Poskod,
@No_Telefon,
@No_Fax,
@Jab_BPO,
@Department,
@No_KPKetua,
@No_KPPenyelaras,
@Ketua_Jabatan,
@Head_Dept,
@Alamat_Jabatan1
)
", conn))
            {
                cmd.Parameters.AddWithValue("@Kod_Jabatan", item.Kod_Jabatan);
                cmd.Parameters.AddWithValue("@Kod_Kementerian", item.Kod_Kementerian);
                cmd.Parameters.AddWithValue("@Jenis_Agensi", item.Jenis_Agensi.ToDbNull());
                cmd.Parameters.AddWithValue("@Sektor", item.Sektor.ToDbNull());
                cmd.Parameters.AddWithValue("@Jabatan", item.Jabatan.ToDbNull());
                cmd.Parameters.AddWithValue("@Singkatan", item.Singkatan.ToDbNull());
                cmd.Parameters.AddWithValue("@Alamat_Jabatan2", item.Alamat_Jabatan2.ToDbNull());
                cmd.Parameters.AddWithValue("@Alamat_Jabatan3", item.Alamat_Jabatan3.ToDbNull());
                cmd.Parameters.AddWithValue("@Poskod", item.Poskod.ToDbNull());
                cmd.Parameters.AddWithValue("@No_Telefon", item.No_Telefon.ToDbNull());
                cmd.Parameters.AddWithValue("@No_Fax", item.No_Fax.ToDbNull());
                cmd.Parameters.AddWithValue("@Jab_BPO", item.Jab_BPO.ToDbNull());
                cmd.Parameters.AddWithValue("@Department", item.Department.ToDbNull());
                cmd.Parameters.AddWithValue("@No_KPKetua", item.No_KPKetua.ToDbNull());
                cmd.Parameters.AddWithValue("@No_KPPenyelaras", item.No_KPPenyelaras.ToDbNull());
                cmd.Parameters.AddWithValue("@Ketua_Jabatan", item.Ketua_Jabatan.ToDbNull());
                cmd.Parameters.AddWithValue("@Head_Dept", item.Head_Dept.ToDbNull());
                cmd.Parameters.AddWithValue("@Alamat_Jabatan1", item.Alamat_Jabatan1.ToDbNull());
                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<object> UpdateAsync(Tbl_KodJabatan item)
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            using (var cmd = new SqlCommand(@"UPDATE  [dbo].[Tbl_KodJabatan] SET [Kod_Jabatan] = @Kod_Jabatan,
[Kod_Kementerian] = @Kod_Kementerian,
[Jenis_Agensi] = @Jenis_Agensi,
[Sektor] = @Sektor,
[Jabatan] = @Jabatan,
[Singkatan] = @Singkatan,
[Alamat_Jabatan2] = @Alamat_Jabatan2,
[Alamat_Jabatan3] = @Alamat_Jabatan3,
[Poskod] = @Poskod,
[No_Telefon] = @No_Telefon,
[No_Fax] = @No_Fax,
[Jab_BPO] = @Jab_BPO,
[Department] = @Department,
[No_KPKetua] = @No_KPKetua,
[No_KPPenyelaras] = @No_KPPenyelaras,
[Ketua_Jabatan] = @Ketua_Jabatan,
[Head_Dept] = @Head_Dept,
[Alamat_Jabatan1] = @Alamat_Jabatan1
 WHERE 

", conn))
            {
                cmd.Parameters.AddWithValue("@Kod_Jabatan", item.Kod_Jabatan);
                cmd.Parameters.AddWithValue("@Kod_Kementerian", item.Kod_Kementerian);
                cmd.Parameters.AddWithValue("@Jenis_Agensi", item.Jenis_Agensi.ToDbNull());
                cmd.Parameters.AddWithValue("@Sektor", item.Sektor.ToDbNull());
                cmd.Parameters.AddWithValue("@Jabatan", item.Jabatan.ToDbNull());
                cmd.Parameters.AddWithValue("@Singkatan", item.Singkatan.ToDbNull());
                cmd.Parameters.AddWithValue("@Alamat_Jabatan2", item.Alamat_Jabatan2.ToDbNull());
                cmd.Parameters.AddWithValue("@Alamat_Jabatan3", item.Alamat_Jabatan3.ToDbNull());
                cmd.Parameters.AddWithValue("@Poskod", item.Poskod.ToDbNull());
                cmd.Parameters.AddWithValue("@No_Telefon", item.No_Telefon.ToDbNull());
                cmd.Parameters.AddWithValue("@No_Fax", item.No_Fax.ToDbNull());
                cmd.Parameters.AddWithValue("@Jab_BPO", item.Jab_BPO.ToDbNull());
                cmd.Parameters.AddWithValue("@Department", item.Department.ToDbNull());
                cmd.Parameters.AddWithValue("@No_KPKetua", item.No_KPKetua.ToDbNull());
                cmd.Parameters.AddWithValue("@No_KPPenyelaras", item.No_KPPenyelaras.ToDbNull());
                cmd.Parameters.AddWithValue("@Ketua_Jabatan", item.Ketua_Jabatan.ToDbNull());
                cmd.Parameters.AddWithValue("@Head_Dept", item.Head_Dept.ToDbNull());
                cmd.Parameters.AddWithValue("@Alamat_Jabatan1", item.Alamat_Jabatan1.ToDbNull());
                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<LoadOperation<Tbl_KodJabatan>> LoadAsync(string sql, int page = 1, int size = 40, bool includeTotal = false)
        {
            if (!sql.ToString().Contains("ORDER"))
                sql += "\r\nORDER BY [Kod_Jabatan]";
            var translator = new SqlPagingTranslator();
            sql = translator.Translate(sql, page, size);

            using (var conn = new SqlConnection(this.ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                var lo = new LoadOperation<Tbl_KodJabatan>
                {
                    CurrentPage = page,
                    Filter = sql,
                    PageSize = size,
                };
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var item = new Tbl_KodJabatan();
                        item.Kod_Jabatan = (string)reader["Kod_Jabatan"];
                        item.Kod_Kementerian = (string)reader["Kod_Kementerian"];
                        item.Jenis_Agensi = reader["Jenis_Agensi"].ReadNullableString();
                        item.Sektor = reader["Sektor"].ReadNullableString();
                        item.Jabatan = reader["Jabatan"].ReadNullableString();
                        item.Singkatan = reader["Singkatan"].ReadNullableString();
                        item.Alamat_Jabatan2 = reader["Alamat_Jabatan2"].ReadNullableString();
                        item.Alamat_Jabatan3 = reader["Alamat_Jabatan3"].ReadNullableString();
                        item.Poskod = reader["Poskod"].ReadNullableString();
                        item.No_Telefon = reader["No_Telefon"].ReadNullableString();
                        item.No_Fax = reader["No_Fax"].ReadNullableString();
                        item.Jab_BPO = reader["Jab_BPO"].ReadNullableString();
                        item.Department = reader["Department"].ReadNullableString();
                        item.No_KPKetua = reader["No_KPKetua"].ReadNullableString();
                        item.No_KPPenyelaras = reader["No_KPPenyelaras"].ReadNullableString();
                        item.Ketua_Jabatan = reader["Ketua_Jabatan"].ReadNullableString();
                        item.Head_Dept = reader["Head_Dept"].ReadNullableString();
                        item.Alamat_Jabatan1 = reader["Alamat_Jabatan1"].ReadNullableString();

                        lo.ItemCollection.Add(item);
                    }
                }
                return lo;
            }
        }

        public async Task<Tbl_KodJabatan> LoadOneAsync()
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            using (var cmd = new SqlCommand(@"SELECT * FROM dbo.Tbl_KodJabatan WHERE 

", conn))
            {
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var item = new Tbl_KodJabatan();
                        item.Kod_Jabatan = (string)reader["Kod_Jabatan"];
                        item.Kod_Kementerian = (string)reader["Kod_Kementerian"];
                        item.Jenis_Agensi = reader["Jenis_Agensi"].ReadNullableString();
                        item.Sektor = reader["Sektor"].ReadNullableString();
                        item.Jabatan = reader["Jabatan"].ReadNullableString();
                        item.Singkatan = reader["Singkatan"].ReadNullableString();
                        item.Alamat_Jabatan2 = reader["Alamat_Jabatan2"].ReadNullableString();
                        item.Alamat_Jabatan3 = reader["Alamat_Jabatan3"].ReadNullableString();
                        item.Poskod = reader["Poskod"].ReadNullableString();
                        item.No_Telefon = reader["No_Telefon"].ReadNullableString();
                        item.No_Fax = reader["No_Fax"].ReadNullableString();
                        item.Jab_BPO = reader["Jab_BPO"].ReadNullableString();
                        item.Department = reader["Department"].ReadNullableString();
                        item.No_KPKetua = reader["No_KPKetua"].ReadNullableString();
                        item.No_KPPenyelaras = reader["No_KPPenyelaras"].ReadNullableString();
                        item.Ketua_Jabatan = reader["Ketua_Jabatan"].ReadNullableString();
                        item.Head_Dept = reader["Head_Dept"].ReadNullableString();
                        item.Alamat_Jabatan1 = reader["Alamat_Jabatan1"].ReadNullableString();

                        return item;
                    }
                }
                return null;
            }
        }

        public string ConnectionString
        {
            get
            {
                var conn = ConfigurationManager.ConnectionStrings["MsSqlSupat"];
                if (null != conn) return conn.ConnectionString;
                return @"Data Source=(localdb)\Projects;Initial Catalog=SUPAT;Integrated Security=True;MultipleActiveResultSets=True";
            }
        }

    }
}
