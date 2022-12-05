using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;   //db 사용을 위한 namespace


namespace DBtest
{
    internal class Program
    {
        //데이터베이스 출력
        public static void LoadList(string connStr)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string query = "select * from item_info";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader r = cmd.ExecuteReader();

            string[] fields = new string[8];

            while (r.Read())
            {
                for(int i=0; i<8; i++)
                {
                    fields[i] = r[i].ToString();
                    Console.Write($" {fields[i]}");
                }
                Console.WriteLine();
            }

            conn.Close();
        }

        //데이터 입력할 인덱스 번호
        static int NewNumber(string connStr)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string query = "select max(Number) from warehousing";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader r = cmd.ExecuteReader();
            r.Read();

            int number = 1;
            if (r[0].ToString().Length > 0)
                number = int.Parse(r[0].ToString())+1;

            r.Close();
            return number;
        }

        //전체 재고
        static int AllCount(string connStr, int code)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string query = "select * from warehousing";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader r = cmd.ExecuteReader();

            int count = 0;

            while (r.Read())
            {
                if (int.Parse(r[1].ToString())== code && r[4].ToString() == "1")
                    count += int.Parse(r[7].ToString());

                else if (int.Parse(r[1].ToString()) == code && r[4].ToString() == "0")
                    count -= int.Parse(r[7].ToString());
            }

            r.Close() ;
            Console.WriteLine($"상품코드={code} 전체수량={count}");
            return count;
        }

        static void Main(string[] args)
        {
            string connStr = "Server=localhost;Uid=root;Database=inventory-mgmt;port=3306;pwd=minju#db00";

            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();

            string query;
            //query = "Insert into item_info (Code, Item, In_price, Out_price)" +
            //    String.Format("values (11115, \"햇반\", 1500, 2000)");

            //전체재고 수정
            int count = AllCount(connStr, 11115);
            query = String.Format("update item_info set Allcount={0} where Code=11115", count);

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            LoadList(connStr);
            AllCount(connStr, 11111);
            AllCount(connStr, 11110);
            AllCount(connStr, 11115);
        }
    }
}
