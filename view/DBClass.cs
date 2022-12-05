using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace n1
{
    internal class DBClass
    {
        string strCon = "Server=localhost;Uid=root;Database=inventory-mgmt;port=3306;pwd=minju#db00";
        string strSql = "select * from warehousing";

        //데이터베이스 출력
        public MySqlDataReader LoadList(string connStr)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string query = strSql;
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader r = cmd.ExecuteReader();

            string[] fields = new string[11];

            while (r.Read())
            {
                for (int i = 0; i < 11; i++)
                {
                    fields[i] = r[i].ToString();
                    Console.Write($" {fields[i]}");
                }
            }

            conn.Close();

            return r;
        }

        //GridView에 전달할 데이터테이블 반환
        public DataTable LoadDT(string strSql)
        {
            //string strSql = 반환할 데이터베이스 테이블 선택
            DataTable dt = null;
            try
            {
                MySqlConnection cnn = new MySqlConnection(strCon);
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                dt = new DataTable();

                cmd.Connection = cnn;
                cmd.CommandText = strSql;
                cmd.CommandType = CommandType.Text;

                da.SelectCommand = cmd;
                da.Fill(dt);

                //dt 컬럼명 확인
                //string[] columns = dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
                //string msg = "";
                //for (int idx = 0; idx < columns.Length; idx++)
                //{
                //    msg += columns[idx].ToString() + " ";
                //}

                //MessageBox.Show(msg);
                
                cnn.Close();

                return dt;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return dt;
            }
        }
    }
}
