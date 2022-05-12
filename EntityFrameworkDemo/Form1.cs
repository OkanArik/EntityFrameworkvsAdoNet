using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ProductDal _productDal=new ProductDal();
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productDal.Add(new Product
            {
                Name = txtAddName.Text,
                UnitPrice = Convert.ToDecimal(txtAddUnitPrice.Text),
                StockAmount = Convert.ToInt32(txtAddStockAmount.Text)
            });
            LoadProducts();
            MessageBox.Show("Added");
        }

        private void LoadProducts()
        {
            dgwProducts.DataSource = _productDal.GetAll();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _productDal.Update(new Product
            {
                Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value),
                Name=txtUpdateName.Text,
                UnitPrice=Convert.ToDecimal(txtUpdateUnitPrice.Text),
                StockAmount=Convert.ToInt32(txtUpdateStockAmount.Text)
            });
            LoadProducts();
            MessageBox.Show("Updated");
        }

        private void dgwProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUpdateName.Text = dgwProducts.CurrentRow.Cells[1].Value.ToString();
            txtUpdateUnitPrice.Text = dgwProducts.CurrentRow.Cells[2].Value.ToString();
            txtUpdateStockAmount.Text = dgwProducts.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            _productDal.Delete(new Product
            {
                Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value)
                
            });
            LoadProducts();
            MessageBox.Show("Deleted");
        }

        private void lblSearch_TextChanged(object sender, EventArgs e)
        {
            SearchProducts(lblSearch.Text);
        }
        private void SearchProducts(string key)
        {
            //var result = _productDal.GetAll().Where(x=> x.Name.ToLower().Contains(key));
            var result = _productDal.GetByName(key);
            dgwProducts.DataSource = result;
        }
    }
}
