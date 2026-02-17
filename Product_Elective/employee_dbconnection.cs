using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ACOTIN_POS_APPLICATION
{
    public class employee_dbconnection
    {
        // ─── CONNECTION ───────────────────────────────────────────────────────────
        private SqlConnection employee_conn;
        private SqlCommand employee_command;
        private SqlDataAdapter employee_adapter;
        public DataSet employee_sql_dataset;
        public string employee_sql;

        // ─── CONNECTION STRING — same Products database as productTbl ─────────────
        public void employee_connString()
        {
            try
            {
                employee_conn = new SqlConnection(
                    "Data Source=.;Initial Catalog=Products;Integrated Security=True;"
                );
                employee_conn.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator!");
            }
        }

        // ─── COMMAND ──────────────────────────────────────────────────────────────
        public void employee_cmd()
        {
            try
            {
                employee_command = new SqlCommand(employee_sql, employee_conn);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator!");
            }
        }

        // ─── SELECT ───────────────────────────────────────────────────────────────
        public void employee_sqladapterSelect()
        {
            try
            {
                employee_adapter = new SqlDataAdapter(employee_command);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator!");
            }
        }

        public void employee_sqldatasetSELECT()
        {
            try
            {
                employee_sql_dataset = new DataSet();
                employee_adapter.Fill(employee_sql_dataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator!");
            }
        }

        // ─── INSERT ───────────────────────────────────────────────────────────────
        public void employee_sqladapterInsert()
        {
            try
            {
                employee_adapter = new SqlDataAdapter();
                employee_adapter.InsertCommand = new SqlCommand(employee_sql, employee_conn);
                employee_adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator!");
            }
        }

        // ─── UPDATE ───────────────────────────────────────────────────────────────
        public void employee_sqladapterUpdate()
        {
            try
            {
                employee_adapter = new SqlDataAdapter();
                employee_adapter.UpdateCommand = new SqlCommand(employee_sql, employee_conn);
                employee_adapter.UpdateCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator!");
            }
        }

        // ─── DELETE ───────────────────────────────────────────────────────────────
        public void employee_sqladapterDelete()
        {
            try
            {
                employee_adapter = new SqlDataAdapter();
                employee_adapter.DeleteCommand = new SqlCommand(employee_sql, employee_conn);
                employee_adapter.DeleteCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator!");
            }
        }

        // ─── SELECT ALL (default query) ───────────────────────────────────────────
        public void employee_select()
        {
            employee_sql = "SELECT * FROM employeeTbl";
        }
    }
}