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
using epsikologi.Adapters.dbo.TEST;

namespace epsikologi.Adapters.dbo.TEST
{
    public class tblPRCOBiodataAdapter
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

        public async Task<int> DeleteAsync(int cOBiodataID)
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            using (var cmd = new SqlCommand(@"DELETE FROM [dbo].[tblPRCOBiodata] WHERE
[COBiodataID] = @COBiodataID
", conn))
            {

                cmd.Parameters.AddWithValue("@COBiodataID", cOBiodataID); await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<object> InsertAsync(tblPRCOBiodata item)
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            using (var cmd = new SqlCommand(@"INSERT INTO [dbo].[tblPRCOBiodata] ([COBumiStatus],
[EmailNotifyInd],
[ReligionCd],
[RaceCd],
[EthnicCd],
[ArmyPoliceCd],
[BloodTypeCd],
[MrtlStatusCd],
[NatStatusCd],
[COHPhoneStatus],
[COUpdtInd],
[EIICd],
[COOrigTrtry],
[TitleCd],
[HighestEduLevelCd],
[GenderCd],
[COBirthPlaceCd],
[COBirthCountryCd],
[NatCd],
[COBirthDt],
[COLastUpdtDt],
[COPhoto],
[COBiodataID],
[COID],
[CONm],
[COEmail],
[COOldID],
[COBirthCertNo],
[COHPhoneNo],
[COOffTelNo],
[COOffTelNoExtn],
[APCrtdBy]
)
VALUES(
@COBumiStatus,
@EmailNotifyInd,
@ReligionCd,
@RaceCd,
@EthnicCd,
@ArmyPoliceCd,
@BloodTypeCd,
@MrtlStatusCd,
@NatStatusCd,
@COHPhoneStatus,
@COUpdtInd,
@EIICd,
@COOrigTrtry,
@TitleCd,
@HighestEduLevelCd,
@GenderCd,
@COBirthPlaceCd,
@COBirthCountryCd,
@NatCd,
@COBirthDt,
@COLastUpdtDt,
@COPhoto,
@COBiodataID,
@COID,
@CONm,
@COEmail,
@COOldID,
@COBirthCertNo,
@COHPhoneNo,
@COOffTelNo,
@COOffTelNoExtn,
@APCrtdBy
)
", conn))
            {
                cmd.Parameters.AddWithValue("@COBumiStatus", item.COBumiStatus.ToDbNull());
                cmd.Parameters.AddWithValue("@EmailNotifyInd", item.EmailNotifyInd.ToDbNull());
                cmd.Parameters.AddWithValue("@ReligionCd", item.ReligionCd.ToDbNull());
                cmd.Parameters.AddWithValue("@RaceCd", item.RaceCd.ToDbNull());
                cmd.Parameters.AddWithValue("@EthnicCd", item.EthnicCd.ToDbNull());
                cmd.Parameters.AddWithValue("@ArmyPoliceCd", item.ArmyPoliceCd.ToDbNull());
                cmd.Parameters.AddWithValue("@BloodTypeCd", item.BloodTypeCd.ToDbNull());
                cmd.Parameters.AddWithValue("@MrtlStatusCd", item.MrtlStatusCd.ToDbNull());
                cmd.Parameters.AddWithValue("@NatStatusCd", item.NatStatusCd.ToDbNull());
                cmd.Parameters.AddWithValue("@COHPhoneStatus", item.COHPhoneStatus.ToDbNull());
                cmd.Parameters.AddWithValue("@COUpdtInd", item.COUpdtInd.ToDbNull());
                cmd.Parameters.AddWithValue("@EIICd", item.EIICd.ToDbNull());
                cmd.Parameters.AddWithValue("@COOrigTrtry", item.COOrigTrtry.ToDbNull());
                cmd.Parameters.AddWithValue("@TitleCd", item.TitleCd.ToDbNull());
                cmd.Parameters.AddWithValue("@HighestEduLevelCd", item.HighestEduLevelCd.ToDbNull());
                cmd.Parameters.AddWithValue("@GenderCd", item.GenderCd.ToDbNull());
                cmd.Parameters.AddWithValue("@COBirthPlaceCd", item.COBirthPlaceCd.ToDbNull());
                cmd.Parameters.AddWithValue("@COBirthCountryCd", item.COBirthCountryCd.ToDbNull());
                cmd.Parameters.AddWithValue("@NatCd", item.NatCd.ToDbNull());
                cmd.Parameters.AddWithValue("@COBirthDt", item.COBirthDt.ToDbNull());
                cmd.Parameters.AddWithValue("@COLastUpdtDt", item.COLastUpdtDt.ToDbNull());
                cmd.Parameters.AddWithValue("@COPhoto", item.COPhoto.ToDbNull());
                cmd.Parameters.AddWithValue("@COBiodataID", item.COBiodataID);
                cmd.Parameters.AddWithValue("@COID", item.COID.ToDbNull());
                cmd.Parameters.AddWithValue("@CONm", item.CONm.ToDbNull());
                cmd.Parameters.AddWithValue("@COEmail", item.COEmail.ToDbNull());
                cmd.Parameters.AddWithValue("@COOldID", item.COOldID.ToDbNull());
                cmd.Parameters.AddWithValue("@COBirthCertNo", item.COBirthCertNo.ToDbNull());
                cmd.Parameters.AddWithValue("@COHPhoneNo", item.COHPhoneNo.ToDbNull());
                cmd.Parameters.AddWithValue("@COOffTelNo", item.COOffTelNo.ToDbNull());
                cmd.Parameters.AddWithValue("@COOffTelNoExtn", item.COOffTelNoExtn.ToDbNull());
                cmd.Parameters.AddWithValue("@APCrtdBy", item.APCrtdBy.ToDbNull());
                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<object> UpdateAsync(tblPRCOBiodata item)
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            using (var cmd = new SqlCommand(@"UPDATE  [dbo].[tblPRCOBiodata] SET [COBumiStatus] = @COBumiStatus,
[EmailNotifyInd] = @EmailNotifyInd,
[ReligionCd] = @ReligionCd,
[RaceCd] = @RaceCd,
[EthnicCd] = @EthnicCd,
[ArmyPoliceCd] = @ArmyPoliceCd,
[BloodTypeCd] = @BloodTypeCd,
[MrtlStatusCd] = @MrtlStatusCd,
[NatStatusCd] = @NatStatusCd,
[COHPhoneStatus] = @COHPhoneStatus,
[COUpdtInd] = @COUpdtInd,
[EIICd] = @EIICd,
[COOrigTrtry] = @COOrigTrtry,
[TitleCd] = @TitleCd,
[HighestEduLevelCd] = @HighestEduLevelCd,
[GenderCd] = @GenderCd,
[COBirthPlaceCd] = @COBirthPlaceCd,
[COBirthCountryCd] = @COBirthCountryCd,
[NatCd] = @NatCd,
[COBirthDt] = @COBirthDt,
[COLastUpdtDt] = @COLastUpdtDt,
[COPhoto] = @COPhoto,
[COBiodataID] = @COBiodataID,
[COID] = @COID,
[CONm] = @CONm,
[COEmail] = @COEmail,
[COOldID] = @COOldID,
[COBirthCertNo] = @COBirthCertNo,
[COHPhoneNo] = @COHPhoneNo,
[COOffTelNo] = @COOffTelNo,
[COOffTelNoExtn] = @COOffTelNoExtn,
[APCrtdBy] = @APCrtdBy
 WHERE 
[COBiodataID] = @COBiodataID
", conn))
            {
                cmd.Parameters.AddWithValue("@COBumiStatus", item.COBumiStatus.ToDbNull());
                cmd.Parameters.AddWithValue("@EmailNotifyInd", item.EmailNotifyInd.ToDbNull());
                cmd.Parameters.AddWithValue("@ReligionCd", item.ReligionCd.ToDbNull());
                cmd.Parameters.AddWithValue("@RaceCd", item.RaceCd.ToDbNull());
                cmd.Parameters.AddWithValue("@EthnicCd", item.EthnicCd.ToDbNull());
                cmd.Parameters.AddWithValue("@ArmyPoliceCd", item.ArmyPoliceCd.ToDbNull());
                cmd.Parameters.AddWithValue("@BloodTypeCd", item.BloodTypeCd.ToDbNull());
                cmd.Parameters.AddWithValue("@MrtlStatusCd", item.MrtlStatusCd.ToDbNull());
                cmd.Parameters.AddWithValue("@NatStatusCd", item.NatStatusCd.ToDbNull());
                cmd.Parameters.AddWithValue("@COHPhoneStatus", item.COHPhoneStatus.ToDbNull());
                cmd.Parameters.AddWithValue("@COUpdtInd", item.COUpdtInd.ToDbNull());
                cmd.Parameters.AddWithValue("@EIICd", item.EIICd.ToDbNull());
                cmd.Parameters.AddWithValue("@COOrigTrtry", item.COOrigTrtry.ToDbNull());
                cmd.Parameters.AddWithValue("@TitleCd", item.TitleCd.ToDbNull());
                cmd.Parameters.AddWithValue("@HighestEduLevelCd", item.HighestEduLevelCd.ToDbNull());
                cmd.Parameters.AddWithValue("@GenderCd", item.GenderCd.ToDbNull());
                cmd.Parameters.AddWithValue("@COBirthPlaceCd", item.COBirthPlaceCd.ToDbNull());
                cmd.Parameters.AddWithValue("@COBirthCountryCd", item.COBirthCountryCd.ToDbNull());
                cmd.Parameters.AddWithValue("@NatCd", item.NatCd.ToDbNull());
                cmd.Parameters.AddWithValue("@COBirthDt", item.COBirthDt.ToDbNull());
                cmd.Parameters.AddWithValue("@COLastUpdtDt", item.COLastUpdtDt.ToDbNull());
                cmd.Parameters.AddWithValue("@COPhoto", item.COPhoto.ToDbNull());
                cmd.Parameters.AddWithValue("@COBiodataID", item.COBiodataID);
                cmd.Parameters.AddWithValue("@COID", item.COID.ToDbNull());
                cmd.Parameters.AddWithValue("@CONm", item.CONm.ToDbNull());
                cmd.Parameters.AddWithValue("@COEmail", item.COEmail.ToDbNull());
                cmd.Parameters.AddWithValue("@COOldID", item.COOldID.ToDbNull());
                cmd.Parameters.AddWithValue("@COBirthCertNo", item.COBirthCertNo.ToDbNull());
                cmd.Parameters.AddWithValue("@COHPhoneNo", item.COHPhoneNo.ToDbNull());
                cmd.Parameters.AddWithValue("@COOffTelNo", item.COOffTelNo.ToDbNull());
                cmd.Parameters.AddWithValue("@COOffTelNoExtn", item.COOffTelNoExtn.ToDbNull());
                cmd.Parameters.AddWithValue("@APCrtdBy", item.APCrtdBy.ToDbNull());
                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<LoadOperation<tblPRCOBiodata>> LoadAsync(string sql, int page = 1, int size = 40, bool includeTotal = false)
        {
            if (!sql.ToString().Contains("ORDER"))
                sql += "\r\nORDER BY [COBiodataID]";
            var translator = new SqlPagingTranslator();
            sql = translator.Translate(sql, page, size);

            using (var conn = new SqlConnection(this.ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                var lo = new LoadOperation<tblPRCOBiodata>
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
                        var item = new tblPRCOBiodata();
                        item.COBumiStatus = reader["COBumiStatus"].ReadNullable<bool>();
                        item.EmailNotifyInd = reader["EmailNotifyInd"].ReadNullable<bool>();
                        item.ReligionCd = reader["ReligionCd"].ReadNullableString();
                        item.RaceCd = reader["RaceCd"].ReadNullableString();
                        item.EthnicCd = reader["EthnicCd"].ReadNullableString();
                        item.ArmyPoliceCd = reader["ArmyPoliceCd"].ReadNullableString();
                        item.BloodTypeCd = reader["BloodTypeCd"].ReadNullableString();
                        item.MrtlStatusCd = reader["MrtlStatusCd"].ReadNullableString();
                        item.NatStatusCd = reader["NatStatusCd"].ReadNullableString();
                        item.COHPhoneStatus = reader["COHPhoneStatus"].ReadNullableString();
                        item.COUpdtInd = reader["COUpdtInd"].ReadNullableString();
                        item.EIICd = reader["EIICd"].ReadNullableString();
                        item.COOrigTrtry = reader["COOrigTrtry"].ReadNullableString();
                        item.TitleCd = reader["TitleCd"].ReadNullableString();
                        item.HighestEduLevelCd = reader["HighestEduLevelCd"].ReadNullableString();
                        item.GenderCd = reader["GenderCd"].ReadNullableString();
                        item.COBirthPlaceCd = reader["COBirthPlaceCd"].ReadNullableString();
                        item.COBirthCountryCd = reader["COBirthCountryCd"].ReadNullableString();
                        item.NatCd = reader["NatCd"].ReadNullableString();
                        item.COBirthDt = reader["COBirthDt"].ReadNullable<DateTime>();
                        item.COLastUpdtDt = reader["COLastUpdtDt"].ReadNullable<DateTime>();
                        item.COPhoto = reader["COPhoto"].ReadNullable<System.Byte[]>();
                        item.COBiodataID = (int)reader["COBiodataID"];
                        item.COID = reader["COID"].ReadNullable<int>();
                        item.CONm = reader["CONm"].ReadNullableString();
                        item.COEmail = reader["COEmail"].ReadNullableString();
                        item.COOldID = reader["COOldID"].ReadNullableString();
                        item.COBirthCertNo = reader["COBirthCertNo"].ReadNullableString();
                        item.COHPhoneNo = reader["COHPhoneNo"].ReadNullableString();
                        item.COOffTelNo = reader["COOffTelNo"].ReadNullableString();
                        item.COOffTelNoExtn = reader["COOffTelNoExtn"].ReadNullableString();
                        item.APCrtdBy = reader["APCrtdBy"].ReadNullableString();

                        lo.ItemCollection.Add(item);
                    }
                }
                return lo;
            }
        }

        public async Task<tblPRCOBiodata> LoadOneAsync(int COBiodataID)
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            using (var cmd = new SqlCommand(@"SELECT * FROM dbo.tblPRCOBiodata WHERE 
[COBiodataID] = @COBiodataID
", conn))
            {
                cmd.Parameters.AddWithValue("@COBiodataID", COBiodataID);
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var item = new tblPRCOBiodata();
                        item.COBumiStatus = reader["COBumiStatus"].ReadNullable<bool>();
                        item.EmailNotifyInd = reader["EmailNotifyInd"].ReadNullable<bool>();
                        item.ReligionCd = reader["ReligionCd"].ReadNullableString();
                        item.RaceCd = reader["RaceCd"].ReadNullableString();
                        item.EthnicCd = reader["EthnicCd"].ReadNullableString();
                        item.ArmyPoliceCd = reader["ArmyPoliceCd"].ReadNullableString();
                        item.BloodTypeCd = reader["BloodTypeCd"].ReadNullableString();
                        item.MrtlStatusCd = reader["MrtlStatusCd"].ReadNullableString();
                        item.NatStatusCd = reader["NatStatusCd"].ReadNullableString();
                        item.COHPhoneStatus = reader["COHPhoneStatus"].ReadNullableString();
                        item.COUpdtInd = reader["COUpdtInd"].ReadNullableString();
                        item.EIICd = reader["EIICd"].ReadNullableString();
                        item.COOrigTrtry = reader["COOrigTrtry"].ReadNullableString();
                        item.TitleCd = reader["TitleCd"].ReadNullableString();
                        item.HighestEduLevelCd = reader["HighestEduLevelCd"].ReadNullableString();
                        item.GenderCd = reader["GenderCd"].ReadNullableString();
                        item.COBirthPlaceCd = reader["COBirthPlaceCd"].ReadNullableString();
                        item.COBirthCountryCd = reader["COBirthCountryCd"].ReadNullableString();
                        item.NatCd = reader["NatCd"].ReadNullableString();
                        item.COBirthDt = reader["COBirthDt"].ReadNullable<DateTime>();
                        item.COLastUpdtDt = reader["COLastUpdtDt"].ReadNullable<DateTime>();
                        item.COPhoto = reader["COPhoto"].ReadNullable<System.Byte[]>();
                        item.COBiodataID = (int)reader["COBiodataID"];
                        item.COID = reader["COID"].ReadNullable<int>();
                        item.CONm = reader["CONm"].ReadNullableString();
                        item.COEmail = reader["COEmail"].ReadNullableString();
                        item.COOldID = reader["COOldID"].ReadNullableString();
                        item.COBirthCertNo = reader["COBirthCertNo"].ReadNullableString();
                        item.COHPhoneNo = reader["COHPhoneNo"].ReadNullableString();
                        item.COOffTelNo = reader["COOffTelNo"].ReadNullableString();
                        item.COOffTelNoExtn = reader["COOffTelNoExtn"].ReadNullableString();
                        item.APCrtdBy = reader["APCrtdBy"].ReadNullableString();

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
                var conn = ConfigurationManager.ConnectionStrings["TEST"];
                if (null != conn) return conn.ConnectionString;
                return @"Server=ketupat;Database=DBHRMISLIVE;User Id=hrmis;Password=ghm171209isr;";
            }
        }

    }
}
