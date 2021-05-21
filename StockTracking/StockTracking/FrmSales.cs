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
    public partial class FrmSales : Form
    {
        public FrmSales()
        {
            InitializeComponent();
        }

        public SalesDTO dto = new SalesDTO();
        bool combofull = false;
        public SalesDetailDTO detail = new SalesDetailDTO();
        public bool isUpdate = false;
        private void FrmSales_Load(object sender, EventArgs e)
        {
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;
            if(!isUpdate)
            {
                GridProduct.DataSource = dto.Products;
                GridProduct.Columns[0].HeaderText = "Product Name";
                GridProduct.Columns[1].HeaderText = "Category Name";
                GridProduct.Columns[2].HeaderText = "Stock Amount";
                GridProduct.Columns[3].HeaderText = "Price";
                GridProduct.Columns[4].Visible = false;
                GridProduct.Columns[5].Visible = false;

                gridCustomer.DataSource = dto.Customers;
                gridCustomer.Columns[0].Visible = false;
                gridCustomer.Columns[1].HeaderText = "Customer Name";

                if (dto.Categories.Count > 0)
                    combofull = true;
            }
            else
            {
                panel1.Hide();
                txtCustomerName.Text = detail.CustomerName;
                txtProductName.Text = detail.ProductName;
                txtPrice.Text = detail.Price.ToString();
                txtSalesAmount.Text = detail.SalesAmount.ToString();
                ProductDetailDTO product = dto.Products.First(x => x.ProductID == detail.ProductID);
                detail.StockAmount = product.StockAmount;
                txtProductStock.Text = detail.StockAmount.ToString();

            }
        }

        private void txtSalesAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(combofull)
            {
                List<ProductDetailDTO> list = dto.Products;
                list = list.Where(x => x.CategoryID == Convert.ToInt32(cmbCategory.SelectedValue)).ToList();
                GridProduct.DataSource = list;
                if(list.Count==0)
                {
                    txtPrice.Clear();
                    txtProductName.Clear();
                    txtSalesAmount.Clear();
                }
            }
        }

        private void textCustomerNameSearch_TextChanged(object sender, EventArgs e)
        {
            List<CustomerDetailDTO> list = dto.Customers;
            list = list.Where(x => x.CustomerName.Contains(txtCustomerSearch.Text)).ToList();
            gridCustomer.DataSource = list;
            if (list.Count == 0)
            {
                txtCustomerName.Clear();
            }
        }
            //SalesDetailDTO detail = new SalesDetailDTO();
        private void GridProduct_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.ProductName = GridProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
            detail.StockAmount = Convert.ToInt32(GridProduct.Rows[e.RowIndex].Cells[2].Value);
            detail.Price = Convert.ToInt32(GridProduct.Rows[e.RowIndex].Cells[3].Value);
            detail.ProductID = Convert.ToInt32(GridProduct.Rows[e.RowIndex].Cells[4].Value);
            detail.CategoryID = Convert.ToInt32(GridProduct.Rows[e.RowIndex].Cells[5].Value);

            txtProductName.Text = detail.ProductName;
            txtPrice.Text = detail.Price.ToString();
            txtProductStock.Text = detail.StockAmount.ToString();
        }

        private void gridCustomer_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.CustomerName = gridCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.CustomerID = Convert.ToInt32(gridCustomer.Rows[e.RowIndex].Cells[0].Value);
            txtCustomerName.Text = detail.CustomerName;
        }
        SalesBLL bll = new SalesBLL();
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (detail.ProductID == 0)
                MessageBox.Show("Please select a product");
            else
            {
                if(!isUpdate)
                {
                    if (detail.CustomerID == 0)
                        MessageBox.Show("Please select a customer ID");
                    else if (detail.CustomerID == 0)
                        MessageBox.Show("Select a customer from the table");
                    else if (detail.StockAmount < Convert.ToInt32(txtProductStock.Text))
                        MessageBox.Show("Stock is not enough");
                    else
                    {
                        detail.SalesAmount = Convert.ToInt32(txtSalesAmount.Text);
                        detail.SalesDate = DateTime.Today;

                        if (bll.Insert(detail))
                        {
                            MessageBox.Show("Sles was added");
                            bll = new SalesBLL();
                            dto = bll.Select();
                            GridProduct.DataSource = dto.Products;
                            combofull = false;
                            cmbCategory.DataSource = dto.Categories;
                            if (dto.Products.Count > 0)
                                combofull = true;
                            txtSalesAmount.Clear();

                        }
                    }

                }
                else
                {
                    if (detail.SalesAmount == Convert.ToInt32(txtSalesAmount.Text))
                        MessageBox.Show("There is not changes");
                    else
                    {
                        int temp = detail.StockAmount + detail.SalesAmount;
                        if (temp < Convert.ToInt32(txtSalesAmount.Text))
                            MessageBox.Show("You don have enoigh stock");
                        else
                        {
                            detail.SalesAmount = Convert.ToInt32(txtSalesAmount.Text);
                            detail.StockAmount = temp - detail.SalesAmount;
                            if(bll.Update(detail))
                            {
                                MessageBox.Show("Sales was updated");
                                this.Close();
                            }
                        }
                    }
                }
            }
        }
    }
}
