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
   public class programAdapter
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
           using(var cmd = new MySqlCommand(@"DELETE FROM epsikometrik.program WHERE
`ID` = @ID
", conn))
           {

               cmd.Parameters.AddWithValue("@ID", id);               await conn.OpenAsync();
               return await cmd.ExecuteNonQueryAsync();
           }
       }

       public async Task<object> InsertAsync(program item)
       {
           using(var conn = new MySqlConnection(this.ConnectionString))
           using(var cmd = new MySqlCommand(@"INSERT INTO `epsikometrik`.`program` (`nama_program`,
`tarikh_wujud`,
`tarikh_kemaskini`,
`pengguna`
)
VALUES(
@nama_program,
@tarikh_wujud,
@tarikh_kemaskini,
@pengguna
)
", conn))
           {
               cmd.Parameters.AddWithValue("@nama_program", item.nama_program);
               cmd.Parameters.AddWithValue("@tarikh_wujud", item.tarikh_wujud);
               cmd.Parameters.AddWithValue("@tarikh_kemaskini", item.tarikh_kemaskini);
               cmd.Parameters.AddWithValue("@pengguna", item.pengguna);
               await conn.OpenAsync();
               return await cmd.ExecuteNonQueryAsync();
           }
       }

       public async Task<object> UpdateAsync(program item)
       {
           using(var conn = new MySqlConnection(this.ConnectionString))
           using(var cmd = new MySqlCommand(@"UPDATE  `epsikometrik`.`program` SET `nama_program` = @nama_program,
`tarikh_wujud` = @tarikh_wujud,
`tarikh_kemaskini` = @tarikh_kemaskini,
`pengguna` = @pengguna
 WHERE 
`ID` = @ID
", conn))
           {
               cmd.Parameters.AddWithValue("@nama_program", item.nama_program);
               cmd.Parameters.AddWithValue("@tarikh_wujud", item.tarikh_wujud);
               cmd.Parameters.AddWithValue("@tarikh_kemaskini", item.tarikh_kemaskini);
               cmd.Parameters.AddWithValue("@pengguna", item.pengguna);
               await conn.OpenAsync();
               return await cmd.ExecuteNonQueryAsync();
           }
       }

       public async Task<LoadOperation<program>> LoadAsync(string sql, int page = 1, int size = 40, bool includeTotal = false)       {
           if (!sql.ToString().Contains("ORDER"))
               sql +="\r\nORDER BY `ID`";
           var translator = new MySqlPagingTranslator();
           sql = translator.Translate(sql, page, size);

           using(var conn = new MySqlConnection(this.ConnectionString))
           using(var cmd = new MySqlCommand( sql, conn))
           {
               var lo = new LoadOperation<program>
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
                       var item = new program();
                       item.ID = (int)reader["ID"];
                       item.nama_program = (string)reader["nama_program"];
                       var __temp3 = (MySql.Data.Types.MySqlDateTime)reader["tarikh_wujud"];
                       if(__temp3.IsValidDateTime ) item.tarikh_wujud = __temp3.GetDateTime();
                       var __temp4 = (MySql.Data.Types.MySqlDateTime)reader["tarikh_kemaskini"];
                       if(__temp4.IsValidDateTime ) item.tarikh_kemaskini = __temp4.GetDateTime();
                       item.pengguna = (string)reader["pengguna"];

                       lo.ItemCollection.Add(item);
                   }
               }
               return lo;
           }
       }

       public async Task<program> LoadOneAsync(int ID)
       {
           using(var conn = new MySqlConnection(this.ConnectionString))
           using(var cmd = new MySqlCommand(@"SELECT * FROM `epsikometrik`.`program` WHERE 
`ID` = @ID
", conn))
           {
               cmd.Parameters.AddWithValue("@ID", ID);
               await conn.OpenAsync();
               using(var reader = await cmd.ExecuteReaderAsync())
               {
                   while(await reader.ReadAsync())
                   {
                       var item = new program();
                       item.ID = (int)reader["ID"];
                       item.nama_program = (string)reader["nama_program"];
                       var __temp3 = (MySql.Data.Types.MySqlDateTime)reader["tarikh_wujud"];
                       if(__temp3.IsValidDateTime ) item.tarikh_wujud = __temp3.GetDateTime();
                       var __temp4 = (MySql.Data.Types.MySqlDateTime)reader["tarikh_kemaskini"];
                       if(__temp4.IsValidDateTime ) item.tarikh_kemaskini = __temp4.GetDateTime();
                       item.pengguna = (string)reader["pengguna"];

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
