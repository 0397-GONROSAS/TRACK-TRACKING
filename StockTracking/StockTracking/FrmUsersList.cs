using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockTracking.BLL;
using StockTracking.DAL.DTO;

namespace StockTracking
{
    public partial class FrmUsersList : Form
    {
        public FrmUsersList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmUsers frm = new FrmUsers();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }
        EmployeeBLL bll = new EmployeeBLL();
        EmployeeDTO dto = new EmployeeDTO();

        private void FrmUsersList_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            cmbPermission.DataSource = dto.permission;
            cmbPermission.DisplayMember = "PermissionType";
            cmbPermission.ValueMember = "ID";
            cmbPermission.SelectedIndex = -1;

            dataGridView1.DataSource = dto.Employees;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "User Number";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].HeaderText = "Permission";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CleanAll();
            dataGridView1.DataSource = dto.Employees;
        }

        private void CleanAll()
        {
            txtName.Clear();
            txtUser.Clear();
            txtSurname.Clear();
            cmbPermission.SelectedIndex = -1;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<EmployeeDetailDTO> list = dto.Employees;
            if (txtName.Text.Trim() == "" && txtSurname.Text.Trim() == "" && txtUser.Text.Trim() == "" && cmbPermission.SelectedIndex == -1)
                MessageBox.Show("Please introduce any field to search");
            else
            {
                if (txtName.Text.Trim() != "")
                    list = list.Where(x => x.Name.Contains(txtName.Text)).ToList();
                if (txtSurname.Text.Trim() != "")
                    list = list.Where(x => x.Surname.Contains(txtSurname.Text)).ToList();
                if (txtUser.Text.Trim() != "")
                    list = list.Where(x => x.UserNumer == Convert.ToInt32(txtUser.Text)).ToList();
                if (cmbPermission.SelectedIndex != -1)
                    list = list.Where(x => x.Permission == Convert.ToInt32(cmbPermission.SelectedValue)).ToList();
            }
            dataGridView1.DataSource = list;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.UserNumer == 0)
                MessageBox.Show("Select a empote from the table");
            else
            {
                FrmUsers frm = new FrmUsers();
                frm.detail = detail;
                frm.isUpdate = true;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                bll = new EmployeeBLL();
                dto = bll.Select();
                dataGridView1.DataSource = dto.Employees;
                CleanAll();
            }
        }
        EmployeeDetailDTO detail = new EmployeeDetailDTO();
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = new EmployeeDetailDTO();
            detail.ID= Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            detail.UserNumer = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            detail.Name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            detail.Surname = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            detail.Password = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            detail.Permission = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
        }
    }
}
