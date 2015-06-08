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
   public class permohonanAdapter
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

       public async Task<int> DeleteAsync(int id)
       {
           using(var conn = new MySqlConnection(this.ConnectionString))
           using(var cmd = new MySqlCommand(@"DELETE FROM epsikometrik.permohonan WHERE
`ID` = @ID
", conn))
           {

               cmd.Parameters.AddWithValue("@ID", id);               await conn.OpenAsync();
               return await cmd.ExecuteNonQueryAsync();
           }
       }

       public async Task<object> InsertAsync(permohonan item)
       {
           using(var conn = new MySqlConnection(this.ConnectionString))
           using(var cmd = new MySqlCommand(@"INSERT INTO `epsikometrik`.`permohonan` (`nama_program`,
`catatan_program`,
`bil_responden`,
`bil_program`,
`siri_program`,
`tahun_program`,
`capaian_ujian`,
`jabatan`,
`_kod_agensi`,
`_kod_permohonan`,
`_kod_permohonan_status`,
`_kod_permohonan_justifikasi`,
`_kod_tadbiran`,
`_kod_ujian_status`,
`tarikh_mula`,
`tarikh_tamat`,
`tarikh_daftar`,
`tarikh_lulus`,
`tarikh_kemaskini`,
`pengguna`
)
VALUES(
@nama_program,
@catatan_program,
@bil_responden,
@bil_program,
@siri_program,
@tahun_program,
@capaian_ujian,
@jabatan,
@_kod_agensi,
@_kod_permohonan,
@_kod_permohonan_status,
@_kod_permohonan_justifikasi,
@_kod_tadbiran,
@_kod_ujian_status,
@tarikh_mula,
@tarikh_tamat,
@tarikh_daftar,
@tarikh_lulus,
@tarikh_kemaskini,
@pengguna
)
", conn))
           {
               cmd.Parameters.AddWithValue("@nama_program", item.nama_program);
               cmd.Parameters.AddWithValue("@catatan_program", item.catatan_program);
               cmd.Parameters.AddWithValue("@bil_responden", item.bil_responden);
               cmd.Parameters.AddWithValue("@bil_program", item.bil_program);
               cmd.Parameters.AddWithValue("@siri_program", item.siri_program);
               cmd.Parameters.AddWithValue("@tahun_program", item.tahun_program);
               cmd.Parameters.AddWithValue("@capaian_ujian", item.capaian_ujian);
               cmd.Parameters.AddWithValue("@jabatan", item.jabatan);
               cmd.Parameters.AddWithValue("@_kod_agensi", item._kod_agensi);
               cmd.Parameters.AddWithValue("@_kod_permohonan", item._kod_permohonan);
               cmd.Parameters.AddWithValue("@_kod_permohonan_status", item._kod_permohonan_status);
               cmd.Parameters.AddWithValue("@_kod_permohonan_justifikasi", item._kod_permohonan_justifikasi);
               cmd.Parameters.AddWithValue("@_kod_tadbiran", item._kod_tadbiran);
               cmd.Parameters.AddWithValue("@_kod_ujian_status", item._kod_ujian_status);
               cmd.Parameters.AddWithValue("@tarikh_mula", item.tarikh_mula);
               cmd.Parameters.AddWithValue("@tarikh_tamat", item.tarikh_tamat);
               cmd.Parameters.AddWithValue("@tarikh_daftar", item.tarikh_daftar);
               cmd.Parameters.AddWithValue("@tarikh_lulus", item.tarikh_lulus);
               cmd.Parameters.AddWithValue("@tarikh_kemaskini", item.tarikh_kemaskini);
               cmd.Parameters.AddWithValue("@pengguna", item.pengguna.ToDbNull());
               await conn.OpenAsync();
               return await cmd.ExecuteNonQueryAsync();
           }
       }

       public async Task<object> UpdateAsync(permohonan item)
       {
           using(var conn = new MySqlConnection(this.ConnectionString))
           using(var cmd = new MySqlCommand(@"UPDATE  `epsikometrik`.`permohonan` SET `nama_program` = @nama_program,
`catatan_program` = @catatan_program,
`bil_responden` = @bil_responden,
`bil_program` = @bil_program,
`siri_program` = @siri_program,
`tahun_program` = @tahun_program,
`capaian_ujian` = @capaian_ujian,
`jabatan` = @jabatan,
`_kod_agensi` = @_kod_agensi,
`_kod_permohonan` = @_kod_permohonan,
`_kod_permohonan_status` = @_kod_permohonan_status,
`_kod_permohonan_justifikasi` = @_kod_permohonan_justifikasi,
`_kod_tadbiran` = @_kod_tadbiran,
`_kod_ujian_status` = @_kod_ujian_status,
`tarikh_mula` = @tarikh_mula,
`tarikh_tamat` = @tarikh_tamat,
`tarikh_daftar` = @tarikh_daftar,
`tarikh_lulus` = @tarikh_lulus,
`tarikh_kemaskini` = @tarikh_kemaskini,
`pengguna` = @pengguna
 WHERE 
`ID` = @ID
", conn))
           {
               cmd.Parameters.AddWithValue("@nama_program", item.nama_program);
               cmd.Parameters.AddWithValue("@catatan_program", item.catatan_program);
               cmd.Parameters.AddWithValue("@bil_responden", item.bil_responden);
               cmd.Parameters.AddWithValue("@bil_program", item.bil_program);
               cmd.Parameters.AddWithValue("@siri_program", item.siri_program);
               cmd.Parameters.AddWithValue("@tahun_program", item.tahun_program);
               cmd.Parameters.AddWithValue("@capaian_ujian", item.capaian_ujian);
               cmd.Parameters.AddWithValue("@jabatan", item.jabatan);
               cmd.Parameters.AddWithValue("@_kod_agensi", item._kod_agensi);
               cmd.Parameters.AddWithValue("@_kod_permohonan", item._kod_permohonan);
               cmd.Parameters.AddWithValue("@_kod_permohonan_status", item._kod_permohonan_status);
               cmd.Parameters.AddWithValue("@_kod_permohonan_justifikasi", item._kod_permohonan_justifikasi);
               cmd.Parameters.AddWithValue("@_kod_tadbiran", item._kod_tadbiran);
               cmd.Parameters.AddWithValue("@_kod_ujian_status", item._kod_ujian_status);
               cmd.Parameters.AddWithValue("@tarikh_mula", item.tarikh_mula);
               cmd.Parameters.AddWithValue("@tarikh_tamat", item.tarikh_tamat);
               cmd.Parameters.AddWithValue("@tarikh_daftar", item.tarikh_daftar);
               cmd.Parameters.AddWithValue("@tarikh_lulus", item.tarikh_lulus);
               cmd.Parameters.AddWithValue("@tarikh_kemaskini", item.tarikh_kemaskini);
               cmd.Parameters.AddWithValue("@pengguna", item.pengguna.ToDbNull());
               await conn.OpenAsync();
               return await cmd.ExecuteNonQueryAsync();
           }
       }

       public async Task<LoadOperation<permohonan>> LoadAsync(string sql, int page = 1, int size = 40, bool includeTotal = false)       {
           if (!sql.ToString().Contains("ORDER"))
               sql +="\r\nORDER BY `ID`";
           var translator = new MySqlPagingTranslator();
           sql = translator.Translate(sql, page, size);

           using(var conn = new MySqlConnection(this.ConnectionString))
           using(var cmd = new MySqlCommand( sql, conn))
           {
               var lo = new LoadOperation<permohonan>
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
                       var item = new permohonan();
                       item.ID = (int)reader["ID"];
                       item.nama_program = (string)reader["nama_program"];
                       item.catatan_program = (string)reader["catatan_program"];
                       item.bil_responden = (int)reader["bil_responden"];
                       item.bil_program = (int)reader["bil_program"];
                       item.siri_program = (int)reader["siri_program"];
                       item.tahun_program = (int)reader["tahun_program"];
                       item.capaian_ujian = (string)reader["capaian_ujian"];
                       item.jabatan = (string)reader["jabatan"];
                       item._kod_agensi = (string)reader["_kod_agensi"];
                       item._kod_permohonan = (int)reader["_kod_permohonan"];
                       item._kod_permohonan_status = (int)reader["_kod_permohonan_status"];
                       item._kod_permohonan_justifikasi = (int)reader["_kod_permohonan_justifikasi"];
                       item._kod_tadbiran = (int)reader["_kod_tadbiran"];
                       item._kod_ujian_status = (int)reader["_kod_ujian_status"];
                       item.tarikh_mula = (string)reader["tarikh_mula"];
                       item.tarikh_tamat = (string)reader["tarikh_tamat"];
                       var __temp18 = (MySql.Data.Types.MySqlDateTime)reader["tarikh_daftar"];
                       if(__temp18.IsValidDateTime ) item.tarikh_daftar = __temp18.GetDateTime();
                       var __temp19 = (MySql.Data.Types.MySqlDateTime)reader["tarikh_lulus"];
                       if(__temp19.IsValidDateTime ) item.tarikh_lulus = __temp19.GetDateTime();
                       var __temp20 = (MySql.Data.Types.MySqlDateTime)reader["tarikh_kemaskini"];
                       if(__temp20.IsValidDateTime ) item.tarikh_kemaskini = __temp20.GetDateTime();
                       item.pengguna = reader["pengguna"].ReadNullableString();

                       lo.ItemCollection.Add(item);
                   }
               }
               return lo;
           }
       }

       public async Task<permohonan> LoadOneAsync(int ID)
       {
           using(var conn = new MySqlConnection(this.ConnectionString))
           using(var cmd = new MySqlCommand(@"SELECT * FROM `epsikometrik`.`permohonan` WHERE 
`ID` = @ID
", conn))
           {
               cmd.Parameters.AddWithValue("@ID", ID);
               await conn.OpenAsync();
               using(var reader = await cmd.ExecuteReaderAsync())
               {
                   while(await reader.ReadAsync())
                   {
                       var item = new permohonan();
                       item.ID = (int)reader["ID"];
                       item.nama_program = (string)reader["nama_program"];
                       item.catatan_program = (string)reader["catatan_program"];
                       item.bil_responden = (int)reader["bil_responden"];
                       item.bil_program = (int)reader["bil_program"];
                       item.siri_program = (int)reader["siri_program"];
                       item.tahun_program = (int)reader["tahun_program"];
                       item.capaian_ujian = (string)reader["capaian_ujian"];
                       item.jabatan = (string)reader["jabatan"];
                       item._kod_agensi = (string)reader["_kod_agensi"];
                       item._kod_permohonan = (int)reader["_kod_permohonan"];
                       item._kod_permohonan_status = (int)reader["_kod_permohonan_status"];
                       item._kod_permohonan_justifikasi = (int)reader["_kod_permohonan_justifikasi"];
                       item._kod_tadbiran = (int)reader["_kod_tadbiran"];
                       item._kod_ujian_status = (int)reader["_kod_ujian_status"];
                       item.tarikh_mula = (string)reader["tarikh_mula"];
                       item.tarikh_tamat = (string)reader["tarikh_tamat"];
                       var __temp18 = (MySql.Data.Types.MySqlDateTime)reader["tarikh_daftar"];
                       if(__temp18.IsValidDateTime ) item.tarikh_daftar = __temp18.GetDateTime();
                       var __temp19 = (MySql.Data.Types.MySqlDateTime)reader["tarikh_lulus"];
                       if(__temp19.IsValidDateTime ) item.tarikh_lulus = __temp19.GetDateTime();
                       var __temp20 = (MySql.Data.Types.MySqlDateTime)reader["tarikh_kemaskini"];
                       if(__temp20.IsValidDateTime ) item.tarikh_kemaskini = __temp20.GetDateTime();
                       item.pengguna = reader["pengguna"].ReadNullableString();

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
