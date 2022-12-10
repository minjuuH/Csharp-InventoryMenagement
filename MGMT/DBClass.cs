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

        //데이터베이스에 항목 추가
        public void insertDB(string query)
        {
            MySqlConnection conn = new MySqlConnection(strCon);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //입출고데이터베이스에 사용할 인덱스 번호 추출
        public int NewNumber(string connStr)
        {
            MySqlConnection conn = new MySqlConnection(strCon);
            conn.Open();
            string query = "select max(Number) from warehousing";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader r = cmd.ExecuteReader();
            r.Read();

            int number = 1;
            if (r[0].ToString().Length > 0)
                number = int.Parse(r[0].ToString()) + 1;

            r.Close();
            return number;
        }
    }
}
