using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockTracking.DAL.DAO;
using StockTracking.DAL;
using StockTracking.DAL.DTO;
using StockTracking.BLL;

namespace StockTracking
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = isGeneral.isNumber(e);
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("Enter the User number");
            else if (txtPass.Text.Trim() == "")
                MessageBox.Show("Enter the Password number");
            else
            {
                List<EmployeeDetailDTO> list = new EmployeeBLL().Get(Convert.ToInt32(txtUserNo.Text), txtPass.Text);
                if (list.Count == 0)
                {
                    MessageBox.Show("The user or the password are wrong");
                }
                else
                {
                    EmployeeDetailDTO emp = new EmployeeDetailDTO();
                    emp = list.First();
                    isUser.Permissison = emp.Permission;
                    FrmMain frm = new FrmMain();
                    this.Hide();
                    frm.ShowDialog();
                }
            }
        }
    }
}
