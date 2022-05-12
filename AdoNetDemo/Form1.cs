using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoNetDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ProductDal _productDal = new ProductDal();
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _productDal.Add(new Product
            {
                Name = txtName.Text,
                UnitPrice = Convert.ToDecimal(txtUnitPrice.Text),
                StockAmount = Convert.ToInt32(txtStockAmount.Text)
            });

            LoadProducts();
            MessageBox.Show("Product added");
        }

        private void LoadProducts()
        {
            dgwProducts.DataSource = _productDal.GetAll();
        }

        private void dgwProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            updateName.Text = dgwProducts.CurrentRow.Cells[1].Value.ToString();
            updateUnitPrice.Text = dgwProducts.CurrentRow.Cells[2].Value.ToString();
            updateStockAmount.Text = dgwProducts.CurrentRow.Cells[3].Value.ToString();
            updateStockAmount.Text = dgwProducts.CurrentRow.Cells[3].Value.ToString();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            Product product = new Product
            {
                Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value),
                Name = updateName.Text,
                UnitPrice = Convert.ToDecimal(updateUnitPrice.Text),
                StockAmount = Convert.ToInt32(updateStockAmount.Text)
            };
            _productDal.Update(product);
            LoadProducts();
            MessageBox.Show("Updated");
    }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value);
            _productDal.Delete(id);
            LoadProducts();
            MessageBox.Show("Deleted");
        }
    }
}
