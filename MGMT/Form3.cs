using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace n1
{
    public partial class Form3 : Form
    {
        DBClass dbclass = new DBClass();
        

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable itemTable = dbclass.LoadDT("select * from item_info");
            string query;
            if(txtCode.Text.Length == 0||txtItem.Text.Length==0||txtInprice.Text.Length==0||txtOutprice.Text.Length==0)
                MessageBox.Show("상품코드, 제품명, 입고단가, 출고단가는 필수 입력 사항입니다.", "등록 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (itemTable.AsEnumerable().Any(row => txtCode.Text == row.Field<String>("Code")))
                MessageBox.Show("이미 등록된 상품코드입니다.", "등록 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                query = "Insert into item_info (Code, Item, In_price, Out_price, Business, Category)" +
                String.Format("values ({0}, \"{1}\", {2}, {3}, \"{4}\", \"{5}\")", 
                txtCode.Text, txtItem.Text, txtInprice.Text, txtOutprice.Text, txtBusiness.Text, txtCategory.Text);
                
                dbclass.insertDB(query);

                DialogResult = DialogResult.OK;     //다이얼로그 결과

                Form2 form2 = (Form2)Owner;
                Close();
            }
        }
    }
}
