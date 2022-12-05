using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;   //db 사용을 위한 namespace

namespace n1
{
    public partial class Form1 : Form
    {
        private MySqlConnection connection;
        private MySqlDataAdapter da;
        private DataTable dt;

        DBClass dbclass = new DBClass();

        public Form1()
        {
            InitializeComponent();
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server. Contact administrator");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                    default:
                        MessageBox.Show(ex.Message);
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.button2.BackColor = Color.Silver;
            this.button3.BackColor = Color.White;
            this.dataGridView1.Visible = true;
            this.dataGridView2.Visible = false;

            DataTable inoutTable = dbclass.LoadDT();
            inoutTable.Columns.RemoveAt(0);

            foreach(DataRow dr in inoutTable.Rows)
            {
                if (dr["In_out"].ToString() == "1")
                    dr["In_out"] = "입고";
                else if (dr["In_out"].ToString() == "0")
                    dr["In_out"] = "출고";
            }

            //dataGridView1.Columns.Clear();    //그리드뷰 columns을 다 비워주는 코드
            dataGridView1.DataSource = inoutTable;
        }

        //입출고내역 버튼 클릭시
        private void button2_Click(object sender, EventArgs e)
        {
            this.button2.BackColor = Color.Silver;
            this.button3.BackColor = Color.White;
            this.dataGridView1.Visible = true;
            this.dataGridView2.Visible = false;
        }

        //전체 재고 버튼 클릭시
        private void button3_Click(object sender, EventArgs e)
        {
            this.button3.BackColor = Color.Silver;
            this.button2.BackColor = Color.White;
            this.dataGridView1.Visible = false;
            this.dataGridView2.Visible = true;
        }

        //등록 버튼 
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("등록할 내용을 입력하세요.", "등록 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //검색 버튼
        private void button4_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox9.Text))
            {
                MessageBox.Show("검색할 내용을 입력하세요.", "검색 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataTable changes = ((DataTable)dataGridView1.DataSource).GetChanges();

            if (changes != null)
            {
                MySqlCommandBuilder mcb = new MySqlCommandBuilder(da);
                da.UpdateCommand = mcb.GetUpdateCommand();
                da.Update(changes);
                ((DataTable)dataGridView1.DataSource).AcceptChanges();
            }
        }
    }
}
