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
   public class penggunaAdapter
   {
       public async Task<T> ExecuteScalarAsync<T>(string sql)
       {
           using(var conn = new MySqlConnection(this.ConnectionString))
           using(var cmd = new MySqlCommand(sql, conn))
           {
               await conn.OpenAsync();
               var dbval = await cmd.ExecuteScalarAsync();
               if(dbval == System.DBNull.Value)
                   return default(T);
               return (T)dbval;
           }
       }

       public async Task<int> DeleteAsync(string nokp)
       {
           using(var conn = new MySqlConnection(this.ConnectionString))
           using(var cmd = new MySqlCommand(@"DELETE FROM epsikometrik.pengguna WHERE
`nokp` = @nokp
", conn))
           {

               cmd.Parameters.AddWithValue("@nokp", nokp);               await conn.OpenAsync();
               return await cmd.ExecuteNonQueryAsync();
           }
       }

       public async Task<object> InsertAsync(pengguna item)
       {
           using(var conn = new MySqlConnection(this.ConnectionString))
           using(var cmd = new MySqlCommand(@"INSERT INTO `epsikometrik`.`pengguna` (`nokp`,
`nama`,
`katalaluan`,
`_kod_pekerjaan`,
`_kod_jawatan`,
`_kod_klasifikasi`,
`_kod_skim`,
`_kod_gred`,
`_kod_agensi`,
`_kod_jantina`,
`_kod_kahwin`,
`_kod_warganegara`,
`_kod_umur`,
`_kod_saraan`,
`_kod_pengguna`,
`_kod_status_aktif`,
`catatan_status_aktif`,
`tarikh_daftar`,
`tarikh_kemaskini`
)
VALUES(
@nokp,
@nama,
@katalaluan,
@_kod_pekerjaan,
@_kod_jawatan,
@_kod_klasifikasi,
@_kod_skim,
@_kod_gred,
@_kod_agensi,
@_kod_jantina,
@_kod_kahwin,
@_kod_warganegara,
@_kod_umur,
@_kod_saraan,
@_kod_pengguna,
@_kod_status_aktif,
@catatan_status_aktif,
@tarikh_daftar,
@tarikh_kemaskini
)
", conn))
           {
               cmd.Parameters.AddWithValue("@nokp", item.nokp);
               cmd.Parameters.AddWithValue("@nama", item.nama);
               cmd.Parameters.AddWithValue("@katalaluan", item.katalaluan);
               cmd.Parameters.AddWithValue("@_kod_pekerjaan", item._kod_pekerjaan);
               cmd.Parameters.AddWithValue("@_kod_jawatan", item._kod_jawatan);
               cmd.Parameters.AddWithValue("@_kod_klasifikasi", item._kod_klasifikasi);
               cmd.Parameters.AddWithValue("@_kod_skim", item._kod_skim);
               cmd.Parameters.AddWithValue("@_kod_gred", item._kod_gred);
               cmd.Parameters.AddWithValue("@_kod_agensi", item._kod_agensi);
               cmd.Parameters.AddWithValue("@_kod_jantina", item._kod_jantina);
               cmd.Parameters.AddWithValue("@_kod_kahwin", item._kod_kahwin);
               cmd.Parameters.AddWithValue("@_kod_warganegara", item._kod_warganegara);
               cmd.Parameters.AddWithValue("@_kod_umur", item._kod_umur);
               cmd.Parameters.AddWithValue("@_kod_saraan", item._kod_saraan);
               cmd.Parameters.AddWithValue("@_kod_pengguna", item._kod_pengguna);
               cmd.Parameters.AddWithValue("@_kod_status_aktif", item._kod_status_aktif);
               cmd.Parameters.AddWithValue("@catatan_status_aktif", item.catatan_status_aktif);
               cmd.Parameters.AddWithValue("@tarikh_daftar", item.tarikh_daftar);
               cmd.Parameters.AddWithValue("@tarikh_kemaskini", item.tarikh_kemaskini);
               await conn.OpenAsync();
               return await cmd.ExecuteNonQueryAsync();
           }
       }

       public async Task<object> UpdateAsync(pengguna item)
       {
           using(var conn = new MySqlConnection(this.ConnectionString))
           using(var cmd = new MySqlCommand(@"UPDATE  `epsikometrik`.`pengguna` SET `nokp` = @nokp,
`nama` = @nama,
`katalaluan` = @katalaluan,
`_kod_pekerjaan` = @_kod_pekerjaan,
`_kod_jawatan` = @_kod_jawatan,
`_kod_klasifikasi` = @_kod_klasifikasi,
`_kod_skim` = @_kod_skim,
`_kod_gred` = @_kod_gred,
`_kod_agensi` = @_kod_agensi,
`_kod_jantina` = @_kod_jantina,
`_kod_kahwin` = @_kod_kahwin,
`_kod_warganegara` = @_kod_warganegara,
`_kod_umur` = @_kod_umur,
`_kod_saraan` = @_kod_saraan,
`_kod_pengguna` = @_kod_pengguna,
`_kod_status_aktif` = @_kod_status_aktif,
`catatan_status_aktif` = @catatan_status_aktif,
`tarikh_daftar` = @tarikh_daftar,
`tarikh_kemaskini` = @tarikh_kemaskini
 WHERE 
`nokp` = @nokp
", conn))
           {
               cmd.Parameters.AddWithValue("@nokp", item.nokp);
               cmd.Parameters.AddWithValue("@nama", item.nama);
               cmd.Parameters.AddWithValue("@katalaluan", item.katalaluan);
               cmd.Parameters.AddWithValue("@_kod_pekerjaan", item._kod_pekerjaan);
               cmd.Parameters.AddWithValue("@_kod_jawatan", item._kod_jawatan);
               cmd.Parameters.AddWithValue("@_kod_klasifikasi", item._kod_klasifikasi);
               cmd.Parameters.AddWithValue("@_kod_skim", item._kod_skim);
               cmd.Parameters.AddWithValue("@_kod_gred", item._kod_gred);
               cmd.Parameters.AddWithValue("@_kod_agensi", item._kod_agensi);
               cmd.Parameters.AddWithValue("@_kod_jantina", item._kod_jantina);
               cmd.Parameters.AddWithValue("@_kod_kahwin", item._kod_kahwin);
               cmd.Parameters.AddWithValue("@_kod_warganegara", item._kod_warganegara);
               cmd.Parameters.AddWithValue("@_kod_umur", item._kod_umur);
               cmd.Parameters.AddWithValue("@_kod_saraan", item._kod_saraan);
               cmd.Parameters.AddWithValue("@_kod_pengguna", item._kod_pengguna);
               cmd.Parameters.AddWithValue("@_kod_status_aktif", item._kod_status_aktif);
               cmd.Parameters.AddWithValue("@catatan_status_aktif", item.catatan_status_aktif);
               cmd.Parameters.AddWithValue("@tarikh_daftar", item.tarikh_daftar);
               cmd.Parameters.AddWithValue("@tarikh_kemaskini", item.tarikh_kemaskini);
               await conn.OpenAsync();
               return await cmd.ExecuteNonQueryAsync();
           }
       }

       public async Task<LoadOperation<pengguna>> LoadAsync(string sql, int page = 1, int size = 40, bool includeTotal = false)       {
           if (!sql.ToString().Contains("ORDER"))
               sql +="\r\nORDER BY `nokp`";
           var translator = new MySqlPagingTranslator();
           sql = translator.Translate(sql, page, size);

           using(var conn = new MySqlConnection(this.ConnectionString))
           using(var cmd = new MySqlCommand( sql, conn))
           {
               var lo = new LoadOperation<pengguna>
                            {
                               CurrentPage = page,
                               Filter = sql,
                               PageSize = size,
                            };
               await conn.OpenAsync();
               using(var reader = await cmd.ExecuteReaderAsync())
               {
                   while(await reader.ReadAsync())
                   {
                       var item = new pengguna();
                       item.nokp = (string)reader["nokp"];
                       item.nama = (string)reader["nama"];
                       item.katalaluan = (string)reader["katalaluan"];
                       item._kod_pekerjaan = (int)reader["_kod_pekerjaan"];
                       item._kod_jawatan = (int)reader["_kod_jawatan"];
                       item._kod_klasifikasi = (string)reader["_kod_klasifikasi"];
                       item._kod_skim = (string)reader["_kod_skim"];
                       item._kod_gred = (string)reader["_kod_gred"];
                       item._kod_agensi = (string)reader["_kod_agensi"];
                       item._kod_jantina = (string)reader["_kod_jantina"];
                       item._kod_kahwin = (int)reader["_kod_kahwin"];
                       item._kod_warganegara = (int)reader["_kod_warganegara"];
                       item._kod_umur = (int)reader["_kod_umur"];
                       item._kod_saraan = (string)reader["_kod_saraan"];
                       item._kod_pengguna = (int)reader["_kod_pengguna"];
                       item._kod_status_aktif = (int)reader["_kod_status_aktif"];
                       item.catatan_status_aktif = (string)reader["catatan_status_aktif"];
                       var __temp18 = (MySql.Data.Types.MySqlDateTime)reader["tarikh_daftar"];
                       if(__temp18.IsValidDateTime ) item.tarikh_daftar = __temp18.GetDateTime();
                       var __temp19 = (MySql.Data.Types.MySqlDateTime)reader["tarikh_kemaskini"];
                       if(__temp19.IsValidDateTime ) item.tarikh_kemaskini = __temp19.GetDateTime();

                       lo.ItemCollection.Add(item);
                   }
               }
               return lo;
           }
       }

       public async Task<pengguna> LoadOneAsync(string nokp)
       {
           using(var conn = new MySqlConnection(this.ConnectionString))
           using(var cmd = new MySqlCommand(@"SELECT * FROM `epsikometrik`.`pengguna` WHERE 
`nokp` = @nokp
", conn))
           {
               cmd.Parameters.AddWithValue("@nokp", nokp);
               await conn.OpenAsync();
               using(var reader = await cmd.ExecuteReaderAsync())
               {
                   while(await reader.ReadAsync())
                   {
                       var item = new pengguna();
                       item.nokp = (string)reader["nokp"];
                       item.nama = (string)reader["nama"];
                       item.katalaluan = (string)reader["katalaluan"];
                       item._kod_pekerjaan = (int)reader["_kod_pekerjaan"];
                       item._kod_jawatan = (int)reader["_kod_jawatan"];
                       item._kod_klasifikasi = (string)reader["_kod_klasifikasi"];
                       item._kod_skim = (string)reader["_kod_skim"];
                       item._kod_gred = (string)reader["_kod_gred"];
                       item._kod_agensi = (string)reader["_kod_agensi"];
                       item._kod_jantina = (string)reader["_kod_jantina"];
                       item._kod_kahwin = (int)reader["_kod_kahwin"];
                       item._kod_warganegara = (int)reader["_kod_warganegara"];
                       item._kod_umur = (int)reader["_kod_umur"];
                       item._kod_saraan = (string)reader["_kod_saraan"];
                       item._kod_pengguna = (int)reader["_kod_pengguna"];
                       item._kod_status_aktif = (int)reader["_kod_status_aktif"];
                       item.catatan_status_aktif = (string)reader["catatan_status_aktif"];
                       var __temp18 = (MySql.Data.Types.MySqlDateTime)reader["tarikh_daftar"];
                       if(__temp18.IsValidDateTime ) item.tarikh_daftar = __temp18.GetDateTime();
                       var __temp19 = (MySql.Data.Types.MySqlDateTime)reader["tarikh_kemaskini"];
                       if(__temp19.IsValidDateTime ) item.tarikh_kemaskini = __temp19.GetDateTime();

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
               var conn = ConfigurationManager.ConnectionStrings["MySqlEpsikometrik"];
               if(null != conn)return conn.ConnectionString;
               return @"Server=localhost;Database=epsikometrik;Uid=root;Pwd=;Allow User Variables=true;Allow Zero Datetime=true;";
           }
       }

   }
}
