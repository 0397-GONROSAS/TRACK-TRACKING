using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockTracking.DAL.DTO;
using StockTracking.BLL;

namespace StockTracking
{
    public partial class FrmUsers : Form
    {
        EmployeeBLL bll = new EmployeeBLL();
        public FrmUsers()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        EmployeeDTO dto = new EmployeeDTO();
        public EmployeeDetailDTO detail = new EmployeeDetailDTO();
        public bool isUpdate = false;
        private void FrmUsers_Load(object sender, EventArgs e)
        {
            if(!isUpdate)
            {
                dto = bll.Select();
                cmbPermission.DataSource = dto.permission;
                cmbPermission.DisplayMember = "PermissionType";
                cmbPermission.ValueMember = "ID";
                cmbPermission.SelectedIndex = -1;
            }
            else
            {
                txtName.Text = detail.Name;
                txtSurname.Text = detail.Surname;
                txtUser.Text = detail.UserNumer.ToString();
                txtPassword.Text = detail.Password;

                txtName.ReadOnly = true;
                txtPassword.ReadOnly = true;
                txtSurname.ReadOnly = true;
                txtUser.ReadOnly = true;

                dto = bll.Select();
                cmbPermission.DataSource = dto.permission;
                cmbPermission.DisplayMember = "PermissionType";
                cmbPermission.ValueMember = "ID";
                cmbPermission.SelectedIndex = detail.Permission-1;
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!isUpdate)
            {
                if (txtName.Text.Trim() == "")
                    MessageBox.Show("You miss the User Name");

                else if (txtPassword.Text.Trim() == "")
                    MessageBox.Show("You miss the Password");

                else if (txtUser.Text.Trim() == "")
                    MessageBox.Show("You miss the User Number");

                else if (txtSurname.Text.Trim() == "")
                    MessageBox.Show("You miss the Surname");

                else if (cmbPermission.SelectedIndex == -1)
                    MessageBox.Show("You miss the Permission");
                else
                {
                    EmployeeDetailDTO employee = new EmployeeDetailDTO();
                    employee.Name = txtName.Text;
                    employee.UserNumer = Convert.ToInt32(txtUser.Text);
                    employee.Surname = txtSurname.Text;
                    employee.Password = txtPassword.Text;
                    employee.Permission = Convert.ToInt32(cmbPermission.SelectedValue);
                    if (bll.Insert(employee))
                    {
                        MessageBox.Show("User was added");
                        CleanAll();
                    }

                }
            }
            else
            {
                if (cmbPermission.SelectedIndex == detail.Permission-1)
                    MessageBox.Show("You must change the permission type");
                else
                {
                    detail.Permission = Convert.ToInt32(cmbPermission.SelectedValue);
                    if(bll.Update(detail))
                    {
                        MessageBox.Show("User was updated");
                        this.Close();
                    }
                }
            }

        }

        private void CleanAll()
        {
            txtName.Clear();
            txtPassword.Clear();
            txtUser.Clear();
            txtSurname.Clear();
            cmbPermission.SelectedIndex = -1;
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = isGeneral.isNumber(e);
        }
    }
}
