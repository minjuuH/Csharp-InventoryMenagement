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

            DataTable inoutTable = dbclass.LoadDT();
            DataTable allTable;
            inoutTable.Columns.RemoveAt(0);

            foreach (DataRow dr in inoutTable.Rows)
            {
                if (dr["In_out"].ToString() == "1")
                    dr["In_out"] = "입고";
                else if (dr["In_out"].ToString() == "0")
                    dr["In_out"] = "출고";
            }

            //dataGridView1.Columns.Clear();    //그리드뷰 columns을 다 비워주는 코드
            dataGridView1.DataSource = inoutTable;

            allTable = inoutTable.Copy();
            allTable.Columns.Remove("Date");
            allTable.Columns.Remove("In_out");
            allTable.Columns.Remove("Count");
            allTable.Columns.Remove("Defect");
            dataGridView2.DataSource = allTable;
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
                MessageBox.Show("검색할 내용을 입력하세요.", "검색 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
