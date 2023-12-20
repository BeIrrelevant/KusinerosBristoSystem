using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dbsys.GetMax
{
    internal class GetData
    {

            public static void GetMaxItem()
            {
                try
                {
                    string connectionString = "Data Source=LAPTOP-K06JGE5P\\SQLEXPRESS;Initial Catalog=DBSYS;Integrated Security=True";
                    string sql = "SELECT MAX(MenuId) FROM Menu";

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand(sql, con);
                        object result = cmd.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            GlobalDeclaration.ItemCode = Convert.ToInt32(result) + 1;
                        }
                        else
                        {
                            GlobalDeclaration.ItemCode = 101;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error ----> GET MAX ID " + ex.Message);
                }
            }

            public static void GetMaxOrderNumber()
            {
                try
                {
                    string connectionString = "Data Source=LAPTOP-K06JGE5P\\SQLEXPRESS;Initial Catalog=DBSYS;Integrated Security=True";
                    string sql = "SELECT MAX(OrderNumber) FROM Orderdetails";

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand(sql, con);
                        object result = cmd.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            GlobalDeclaration.OrderNumber = Convert.ToInt32(result) + 1;
                        }
                        else
                        {
                            GlobalDeclaration.OrderNumber = 10001;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error ----> GET MAX ORNo " + ex.Message);
                }
            }

            public static void GetNumItem()
            {
                try
                {
                    string connectionString = "Data Source=LAPTOP-K06JGE5P\\SQLEXPRESS;Initial Catalog=DBSYS;Integrated Security=True";
                    string sql = "SELECT MAX(ItemNo) FROM Purchase";

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand(sql, con);
                        object result = cmd.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            GlobalDeclaration.ItemNo = Convert.ToInt32(result) + 1;
                        }
                        else
                        {
                            GlobalDeclaration.ItemNo = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error ----> GET MAX ID " + ex.Message);
                }
            }
        }
    }

