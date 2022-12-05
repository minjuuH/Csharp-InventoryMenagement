using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace n1
{
    public partial class Form1 : Form
    {
        DBClass dbclass = new DBClass();

        //데이터테이블 멤버
        DataTable wholeinoutTable;   //입출고 내역
        DataTable wholeitemTable;     //전체재고

        public Form1()
        {
            InitializeComponent();
        }

        // 실행했을 때
        private void Form1_Load(object sender, EventArgs e) 
        {
            this.button2.BackColor = Color.Silver;
            this.button3.BackColor = Color.White;
            this.dataGridView1.Visible = true;
            this.dataGridView2.Visible = false;

            wholeinoutTable = dbclass.LoadDT("select * from warehousing");
            wholeitemTable = dbclass.LoadDT("select * from item_info");
            wholeinoutTable.Columns.RemoveAt(0);

            //foreach (DataRow dr in inoutTable.Rows)
            //{
            //    if (dr["In_out"].ToString() == "1")
            //        dr["In_out"] = "입고";
            //    else if (dr["In_out"].ToString() == "0")
            //        dr["In_out"] = "출고";
            //}

            //dataGridView1.Columns.Clear();    //그리드뷰 columns을 다 비워주는 코드

            //입출고 데이터
            dataGridView1.DataSource = wholeinoutTable;

            //전체 재고 데이터            
            dataGridView2.DataSource = wholeitemTable;
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
            this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                //MessageBox.Show("검색할 내용을 입력하세요.", "검색 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dataGridView1.DataSource = wholeinoutTable;
                dataGridView2.DataSource = wholeitemTable;
            }
            else if(dataGridView1.Visible)
            {
                string searchtext = "%" + textBox9.Text + "%";
                DataTable inoutTable = dbclass.LoadDT(String.Format("SELECT * FROM warehousing Where Code LIKE '{0}' OR Item LIKE '{1}'", searchtext, searchtext));
                inoutTable.Columns.RemoveAt(0);
                dataGridView1.DataSource = inoutTable;
            }
            else if (dataGridView2.Visible)
            {
                string searchtext = "%" + textBox9.Text + "%";
                DataTable itemTable = dbclass.LoadDT(String.Format("SELECT * FROM item_info Where Code LIKE '{0}' OR Item LIKE '{1}'", searchtext, searchtext));
                dataGridView2.DataSource = itemTable;
            }
        }
    }
}
