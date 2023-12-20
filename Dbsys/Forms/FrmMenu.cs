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
    public partial class FrmMenu : Form
    {
        private BindingList<AppData.Menu> menuList; 

        public FrmMenu()
        {
            InitializeComponent();
        }

        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            FrmAddMenu addMenu = new FrmAddMenu();
            addMenu.ItemAdded += AddMenu_ItemAdded; 
            addMenu.ShowDialog();
            clearfields();
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-K06JGE5P\\SQLEXPRESS;Initial Catalog=DBSYS;Integrated Security=True");
           con.Open();



            SqlCommand cmd = new SqlCommand("UPDATE Menu SET MenuName=@MenuName, MenuPrice=@MenuPrice, MenuCategory=@MenuCategory, MenuImg=@MenuImg " +
               "WHERE MenuId=@MenuId", con);

            cmd.Parameters.AddWithValue("@MenuId", txtMenuId.Text);
            cmd.Parameters.AddWithValue("@MenuName", txtMenuN.Text);
            cmd.Parameters.AddWithValue("@MenuPrice", txtMenuPrice.Text);
            cmd.Parameters.AddWithValue("@MenuCategory", cbCategory.Text);
            cmd.Parameters.AddWithValue("@MenuImg", lblPath.Text);

            cmd.ExecuteNonQuery();


            MessageBox.Show("Successfully Updated!!!");

            con.Close();
            FrmMenu_Load(sender, e);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewPRICES.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewPRICES.SelectedRows[0];
                int MenuId = Convert.ToInt32(selectedRow.Cells["MenuId"].Value);

                using (var db = new DBSYSEntities())
                {
            
                    var menu = db.Menu.Find(MenuId);
                    if (menu != null)
                    {
                        db.Menu.Remove(menu);
                        db.SaveChanges();
                        dataGridViewPRICES.Rows.Remove(selectedRow);
                        MessageBox.Show("Successfully deleted!");
                    }
                }
            }
            else
            {
                MessageBox.Show("No row selected!");
            }
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            LoadMenuData();

            SqlConnection con = new SqlConnection("Data Source=LAPTOP-K06JGE5P\\SQLEXPRESS;Initial Catalog=DBSYS;Integrated Security=True");
            con.Open();


            SqlCommand cmdMenu = new SqlCommand("SELECT * FROM Menu", con);
            SqlDataAdapter daMenu = new SqlDataAdapter(cmdMenu);
            DataTable dtMenu = new DataTable();
            daMenu.Fill(dtMenu);
            GetMax.GetData.GetMaxItem();
            txtMenuId.Text = GetMax.GlobalDeclaration.ItemCode.ToString();
            dataGridViewPRICES.DataSource = dtMenu;
        }

        private void LoadMenuData()
        {
            try
            {
                using (var db = new DBSYSEntities())
                {
                    menuList = new BindingList<AppData.Menu>(db.Menu.ToList());
                    dataGridViewPRICES.DataSource = menuList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void AddMenu_ItemAdded(object sender, EventArgs e)
        {

           LoadMenuData();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
        
        }

        private void dataGridViewPRICES_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewPRICES.Rows[e.RowIndex];

                txtMenuId.Text = row.Cells["MenuId"].Value.ToString();
                txtMenuN.Text = row.Cells["MenuName"].Value.ToString();
                txtMenuPrice.Text = row.Cells["MenuPrice"].Value.ToString();
                cbCategory.Text = row.Cells["MenuCategory"].Value.ToString();
                lblPath.Text = row.Cells["MenuImg"].Value.ToString();
            }
        }

        private void txtMenuN_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void clearfields()
        {

            txtMenuId.Clear();
            txtMenuN.Clear();
            txtMenuPrice.Clear();
            txtSearch.Clear();
            cbCategory.Items.Clear();
        }
    }
}