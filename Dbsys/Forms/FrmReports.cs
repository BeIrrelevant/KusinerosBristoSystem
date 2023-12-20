using Dbsys.AppData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dbsys.Forms
{
    public partial class FrmReports : Form
    {
        private SqlConnection connection;


        public FrmReports()
        {
            InitializeComponent();
            connection = new SqlConnection("Data Source=LAPTOP-K06JGE5P\\SQLEXPRESS;Initial Catalog=DBSYS;Integrated Security=True");
        }

        private void FrmReports_Load(object sender, EventArgs e)
        {
            LoadSalesReport();
            CalculateOverallSales();
        }

        private void LoadSalesReport()
        {
            try
            {
                connection.Open();

                string query = "SELECT OrderDate, SUM(TotalBill) AS Sales FROM OrderDetails GROUP BY OrderDate";
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dtSalesReport = new DataTable();
                adapter.Fill(dtSalesReport);

                reportSalesDataGridView.DataSource = dtSalesReport;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sales report: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void CalculateOverallSales()
        {
            try
            {
                connection.Open();

                string query = "SELECT SUM(TotalBill) AS OverallSales FROM OrderDetails";
                SqlCommand cmd = new SqlCommand(query, connection);

                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    decimal overallSales = Convert.ToDecimal(result);
                    overallSalesLabel.Text = $"{overallSales.ToString("C")}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating overall sales: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

    }
}