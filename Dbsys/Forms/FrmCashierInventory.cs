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
    public partial class FrmCashierInventory : Form
    {
        public FrmCashierInventory()
        {
            InitializeComponent();
        }

        private void FrmCashierInventory_Load(object sender, EventArgs e)
        {
            lblUserId.Text = UserLogged.GetInstance().UserAccount.userId.ToString();
            lblUseraName.Text = UserLogged.GetInstance().UserAccount.userName.ToString();

            int userId = int.Parse(lblUserId.Text);

            SqlConnection con = new SqlConnection("Data Source=LAPTOP-K06JGE5P\\SQLEXPRESS;Initial Catalog=DBSYS;Integrated Security=True");
            con.Open();

        
            SqlCommand cmdOrderDetails = new SqlCommand("SELECT * FROM OrderDetails WHERE userId = '" + userId + "'", con);
            SqlDataAdapter daOrderDetails = new SqlDataAdapter(cmdOrderDetails);
            DataTable dtOrderDetails = new DataTable();
            daOrderDetails.Fill(dtOrderDetails);
            GetMax.GetData.GetMaxOrderNumber();
            lblOrderNumber.Text = GetMax.GlobalDeclaration.OrderNumber.ToString();
            dataGridViewOrderDetails.DataSource = dtOrderDetails;




            SqlCommand cmdPurchase = new SqlCommand("SELECT * FROM Purchase WHERE userId = '" + userId + "' AND CustomerName = '" + txtCustomerName.Text + "'", con);
            SqlDataAdapter daPurchase = new SqlDataAdapter(cmdPurchase);
            DataTable dtPurchase = new DataTable();
            daPurchase.Fill(dtPurchase);
            GetMax.GetData.GetNumItem();
            lblItemNo.Text = GetMax.GlobalDeclaration.ItemNo.ToString();
            dataGridViewPurchase.DataSource = dtPurchase;

            dataGridViewOrderDetails.Refresh();
            dataGridViewPurchase.Refresh();
        }

        private void dataGridViewOrderDetails_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewOrderDetails.SelectedRows.Count > 0)
            {
                string customerName = dataGridViewOrderDetails.SelectedRows[0].Cells["CustomerName"].Value.ToString();

                SqlConnection con = new SqlConnection("Data Source=LAPTOP-K06JGE5P\\SQLEXPRESS;Initial Catalog=DBSYS;Integrated Security=True");
                con.Open();

                SqlCommand cmdOrderDetails = new SqlCommand("SELECT * FROM Purchase WHERE CustomerName = '" + customerName + "'", con);
                SqlDataAdapter daOrderDetails = new SqlDataAdapter(cmdOrderDetails);
                DataTable dtOrderDetails = new DataTable();
                daOrderDetails.Fill(dtOrderDetails);
                dataGridViewPurchase.DataSource = dtOrderDetails;

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-K06JGE5P\\SQLEXPRESS;Initial Catalog=DBSYS;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM OrderDetails WHERE userId=@userId", con);
            cmd.Parameters.AddWithValue("@userId", int.Parse(txtSearch.Text));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridViewOrderDetails.DataSource = dt;
        }
    }
}
