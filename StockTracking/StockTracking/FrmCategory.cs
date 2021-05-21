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
    public partial class FrmCategory : Form
    {
        public FrmCategory()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        CategoryBLL bll = new CategoryBLL();
        public CategoryDetailDTO detail = new CategoryDetailDTO();
        public bool isUpdate = false;
        private void FrmCategory_Load(object sender, EventArgs e)
        {
            if(isUpdate)
            {
                txtCategoryNamw.Text = detail.CategoryName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {   
            if (txtCategoryNamw.Text.Trim() == "")
                MessageBox.Show("Category is empty");
            else
            {
                 if(!isUpdate)
                {
                    CategoryDetailDTO category = new CategoryDetailDTO();
                    category.CategoryName = txtCategoryNamw.Text;
                    if (bll.Insert(category))
                    {
                        MessageBox.Show("Category was added");
                        txtCategoryNamw.Clear();
                    }
                }else
                {
                    if (detail.CategoryName == txtCategoryNamw.Text.Trim())
                        MessageBox.Show("Thre is not change");
                    else
                    {
                        detail.CategoryName = txtCategoryNamw.Text;
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("Category was updated");
                            this.Close();
                        }
                    }

                }
            }
        }
    }
}
