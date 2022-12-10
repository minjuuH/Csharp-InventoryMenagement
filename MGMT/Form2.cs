using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace n1
{
    public partial class Form2 : Form
    {
        DBClass dbclass = new DBClass();
        DataTable itemTable;
        string[] columns = new string[] { "In_price", "Out_price", "Allcount", "Category", "Note" };

        public Form2()
        {
            InitializeComponent();
        }

        public void setTable(DataTable table)
        {
            foreach (string s in columns)
                table.Columns.Remove(s);
        }

        public void setDB()
        {
            itemTable = dbclass.LoadDT("select * from item_info");
            setTable(itemTable);

            dataGridView1.DataSource = itemTable;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            setDB();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // 검색하면 해당 내용 그리드 뷰에 띄움
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                //MessageBox.Show("검색할 내용을 입력하세요.", "검색 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dataGridView1.DataSource = itemTable;
            }
            else if (dataGridView1.Visible)
            {
                string searchtext = "%" + textBox1.Text + "%";
                DataTable searchTable = dbclass.LoadDT(String.Format("SELECT * FROM item_info Where Code LIKE '{0}' OR Item LIKE '{1}'", searchtext, searchtext));
                setTable(searchTable);

                dataGridView1.DataSource = searchTable;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Owner = this;

            //form2에서 전송한 데이터 출력
            if (form3.ShowDialog() == DialogResult.OK)
                setDB();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult = DialogResult.OK;     //다이얼로그 결과

            Form1 form1 = (Form1)Owner;
            form1.choicecode = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            form1.choiceitem = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            form1.wholeitemTable = dbclass.LoadDT("select * from item_info");

            this.Close();
        }
    }
}
