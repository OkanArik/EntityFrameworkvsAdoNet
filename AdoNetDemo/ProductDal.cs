using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetDemo
{
    public class ProductDal
    {
        SqlConnection _sqlConnection = new SqlConnection(@"server = (localdb)\MSSQLLocalDB; initial catalog=ETrade ; integrated security = true");
        public List<Product> GetAll()
        {

            ConnectionControl();
            SqlCommand command = new SqlCommand("Select * from Products", _sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            List<Product> products = new List<Product>();

            while(reader.Read())
            {
                Product product = new Product
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    StockAmount = (int)reader["StockAmount"],
                    UnitPrice =(decimal)reader["UnitPrice"]
                };
                products.Add(product);  
                
            }


            reader.Close();
            _sqlConnection.Close();
            return products;

        }

        public DataTable GetAll2() // DataTable günümüzde kullanılmamaktadır.
        {
            SqlConnection sqlConnection = new SqlConnection(@"server = (localdb)\MSSQLLocalDB; initial catalog=ETrade ; integrated security = true");
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlCommand command = new SqlCommand("Select * from Products",sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            reader.Close();
            sqlConnection.Close();
            return dataTable;
            
        }

        public void Add(Product product)
        {
            ConnectionControl();
            SqlCommand sqlCommand = new SqlCommand
                ("Insert into Products values(@name,@unitPrice,@stockAmount)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@name",product.Name);
            sqlCommand.Parameters.AddWithValue("@unitPrice",product.UnitPrice);
            sqlCommand.Parameters.AddWithValue("@stockAmount",product.StockAmount);
            sqlCommand.ExecuteNonQuery();

            _sqlConnection.Close();
            
        }

        public void Update(Product product)
        {
            ConnectionControl();
            SqlCommand sqlCommand = new SqlCommand
                ("Update Products Set Name=@name , UnitPrice=@unitPrice , StockAmount=@stockAmount where Id=@id", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@name", product.Name);
            sqlCommand.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            sqlCommand.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            sqlCommand.Parameters.AddWithValue("@id", product.Id);
            
            sqlCommand.ExecuteNonQuery();

            _sqlConnection.Close();

        }

        public void Delete(int id)
        {
            ConnectionControl();
            SqlCommand sqlCommand = new SqlCommand
                ("Delete from Products where Id=@id", _sqlConnection);
   
            sqlCommand.Parameters.AddWithValue("@id",id);

            sqlCommand.ExecuteNonQuery();

            _sqlConnection.Close();

        }



        private void ConnectionControl()
        {
            if (_sqlConnection.State == ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }
        }
    }
}
