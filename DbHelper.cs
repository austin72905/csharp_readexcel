using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace readExcel
{
    public class DbHelper
    {
        //連接資料庫
        public static string conStr = new DbConnectStr().conStr;
        public static string conStrOld = new DbConnectStr().conStrOld;

        //查詢資料庫
        public static DataSet QueryNewDB(string SQLString, params SqlParameter[] cmdParms)
        {
            using (var connection = new SqlConnection(conStr))
            {
                var cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (var da = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        connection.Close();
                    }
                    return ds;
                }
            }
        }

        //查詢舊資料庫
        public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            using (var connection = new SqlConnection(conStrOld))
            {
                var cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (var da = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        connection.Close();
                    }
                    return ds;
                }
            }
        }

        //幫你整理參數
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        //修改新庫資料
        public async static Task asyncExecuteSql(int newDB, string SQLString, params SqlParameter[] cmdParms)
        {
            using (var connection = new SqlConnection(conStr))
            {
                using (var cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        await cmd.ExecuteScalarAsync();
                        //cmd.Parameters.Clear();
                    }
                    catch (SqlException e)
                    {
                        //connection.Close();
                        throw e;
                    }
                }
            }
        }


        //取得新庫的資料表
        public DataTable GetPaySet()
        {
            SqlParameter[] parameters = { };

            DataTable dt = DbHelper.QueryNewDB("SELECT ThirdInfo.PayName," +
                "PayChannel.Name AS 'PayChannel'," +
                "PayWay.Name AS 'PayWay'" +
                "FROM ThirdPay" +
                " FULL JOIN ThirdInfo ON ThirdPay.ThirdInfoID = ThirdInfo.ID" +
                " FULL JOIN PayChannel ON ThirdPay.PayChannelID = PayChannel.ID" +
                " FULL JOIN PayWay ON ThirdPay.PayWayID = PayWay.ID " +
                "where PayChannel.Name is not null", parameters).Tables[0];

            return dt;
        }

        //newDb all setting
        public DataTable GetPaySetting()
        {

            SqlParameter[] parameters = { };

            DataTable dt = DbHelper.QueryNewDB("SELECT ThirdInfo.PayName," +
                "PayChannel.Name AS 'PayChannel'," +
                "PayWay.Name AS 'PayWay'," +
                "ThirdInfo.RedirectURL," +
                "ThirdInfo.WithdrawURL," +
                "ThirdInfo.Remark," +
                "ThirdInfo.WithdrawRemark," +
                "ThirdInfo.IsEnabled," +
                "ThirdInfo.WithdrawIsEnabled FROM ThirdPay" +
                " FULL JOIN ThirdInfo ON ThirdPay.ThirdInfoID = ThirdInfo.ID" +
                " FULL JOIN PayChannel ON ThirdPay.PayChannelID = PayChannel.ID" +
                " FULL JOIN PayWay ON ThirdPay.PayWayID = PayWay.ID", parameters).Tables[0];

            return dt;
        }


        //取得舊庫支付方式資料表
        public DataTable GetOldPaySet()
        {
            SqlParameter[] parameters = { };

            DataTable dt = DbHelper.Query("select payName,setting from fastpay_set", parameters).Tables[0];
            return dt;
        }


    }
}
