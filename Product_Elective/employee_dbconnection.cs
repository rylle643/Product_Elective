using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Product_Elective
{
    public class employee_dbconnection
    {
        private SqlConnection employee_conn;
        private SqlCommand employee_command;
        private SqlDataAdapter employee_adapter;
        public DataSet employee_sql_dataset;
        public string employee_sql;

        public void employee_connString()
        {
            try
            {
                employee_conn = new SqlConnection(
                    "Data Source=192.168.1.10,1433;Initial Catalog=Products;User ID=Rylle_PC;Password=0000;"
                );
                employee_conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection error: " + ex.Message);
            }
        }

        public void employee_cmd()
        {
            try
            {
                employee_command = new SqlCommand(employee_sql, employee_conn);
                employee_command.CommandType = CommandType.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Command error: " + ex.Message);
            }
        }

        public void employee_sqladapterSelect()
        {
            try
            {
                employee_adapter = new SqlDataAdapter(employee_command);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Adapter error: " + ex.Message);
            }
        }

        public void employee_sqldatasetSELECT()
        {
            try
            {
                employee_sql_dataset = new DataSet();
                employee_adapter.Fill(employee_sql_dataset, "employeeTbl");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dataset error: " + ex.Message);
            }
        }

        public void employee_sqladapterInsert()
        {
            try
            {
                employee_adapter = new SqlDataAdapter();
                employee_adapter.InsertCommand = new SqlCommand(employee_sql, employee_conn);
                employee_adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert error: " + ex.Message);
            }
        }

        public void employee_sqladapterUpdate()
        {
            try
            {
                employee_adapter = new SqlDataAdapter();
                employee_adapter.UpdateCommand = new SqlCommand(employee_sql, employee_conn);
                employee_adapter.UpdateCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update error: " + ex.Message);
            }
        }

        public void employee_sqladapterDelete()
        {
            try
            {
                employee_adapter = new SqlDataAdapter();
                employee_adapter.DeleteCommand = new SqlCommand(employee_sql, employee_conn);
                employee_adapter.DeleteCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete error: " + ex.Message);
            }
        }
    }
}