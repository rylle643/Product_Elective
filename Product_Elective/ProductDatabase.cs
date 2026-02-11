using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACOTIN_POS_APPLICATION
{
    internal class ProductDatabase
    {
        public String product_connectionString = null;
        public SqlConnection product_sql_connection;
        public SqlCommand product_sql_command;
        public DataSet product_sql_dataset;
        public SqlDataAdapter product_sql_dataadapter;
        public string product_sql = null;

        public void product_connString()
        {
            product_sql_connection = new SqlConnection();
            product_connectionString = "Data Source=192.168.1.9,1433;Initial Catalog=Products;User ID=Rylle_PC;Password=0000;";
            product_sql_connection = new SqlConnection(product_connectionString);
            product_sql_connection.ConnectionString = product_connectionString;
            product_sql_connection.Open();
        }

        public void product_cmd()
        {
            product_sql_command = new SqlCommand(product_sql, product_sql_connection);
            product_sql_command.CommandType = CommandType.Text;
        }

        public void product_sqladapterSelect()
        {
            product_sql_dataadapter = new SqlDataAdapter();
            product_sql_dataadapter.SelectCommand = product_sql_command;
            product_sql_command.ExecuteNonQuery();
        }

        public void product_sqladapterInsert()
        {
            product_sql_dataadapter = new SqlDataAdapter();
            product_sql_dataadapter.InsertCommand = product_sql_command;
            product_sql_command.ExecuteNonQuery();
        }

        public void product_sqladapterDelete()
        {
            product_sql_dataadapter = new SqlDataAdapter();
            product_sql_dataadapter.DeleteCommand = product_sql_command;
            product_sql_command.ExecuteNonQuery();
        }

        public void product_sqladapterUpdate()
        {
            product_sql_dataadapter = new SqlDataAdapter();
            product_sql_dataadapter.UpdateCommand = product_sql_command;
            product_sql_command.ExecuteNonQuery();
        }

        public void product_sqldatasetSELECT()
        {
            product_sql_dataset = new DataSet();
            product_sql_dataadapter.Fill(product_sql_dataset, "productTbl");
        }
    }
}