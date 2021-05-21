using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockTracking
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmCategoryList frm = new FrmCategoryList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            FrmSalesList frm = new FrmSalesList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;

        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            FrmProductList frm = new FrmProductList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            FrmCustomerList frm = new FrmCustomerList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            FrmAddStock frm = new FrmAddStock();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FrmDeleted frm = new FrmDeleted();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            FrmUsersList frm = new FrmUsersList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if(isUser.Permissison == 1)
            {
                btnCategory.Hide();
                btnCustomer.Hide();
                btnProduct.Hide();
                btnAddStock.Hide();
                btnDelete.Hide();
                btnUsers.Hide();
                btnSales.Location = new Point(30, 28);
                btnLogOut.Location = new Point(230, 28);
                btnExit.Location = new Point(420, 28);
            }
            else if (isUser.Permissison == 2)
            {
                btnSales.Hide();
                btnCustomer.Hide();
                btnDelete.Hide();
                btnUsers.Hide();

                btnAddStock.Location = new Point(30, 28);
                btnCategory.Location = new Point(420, 28);
                btnExit.Location = new Point(120, 250);
                btnLogOut.Location = new Point(320,250);
            }
            else if (isUser.Permissison == 3)
            {
                btnSales.Hide();
                btnCategory.Hide();
                btnAddStock.Hide();
                btnDelete.Hide();
                btnUsers.Hide();

                btnProduct.Location = new Point(120, 28);
                btnCustomer.Location = new Point(320, 28);
                btnExit.Location = new Point(120, 250);
                btnLogOut.Location = new Point(320, 250);
            }
            else if (isUser.Permissison == 4)
            {
                btnCategory.Hide();
                btnCustomer.Hide();
                btnProduct.Hide();
                btnAddStock.Hide();
                btnDelete.Hide();
                btnSales.Hide();

                btnUsers.Location = new Point(320, 28);
                btnExit.Location = new Point(120, 28);
                btnLogOut.Location = new Point(320, 28);
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            FrmLogin frm = new FrmLogin();
            this.Hide();
            frm.ShowDialog();
        }
    }
}
