using Dbsys.AppData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dbsys.Forms
{
    public partial class FrmAddMenu : Form
    {
        public bool hasChange = false;
        private String IMG_PATH = AppDomain.CurrentDomain.BaseDirectory + "\\MenuImage";

        // Event declaration
        public event EventHandler ItemAdded;

        public FrmAddMenu()
        {
            InitializeComponent();

            if (!Directory.Exists(IMG_PATH))
                Directory.CreateDirectory(IMG_PATH);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();

            String path = ofd.FileName;

            label1.Text = path;
            pictureBox1.Image = new Bitmap(path);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                String oldpath = label1.Text;
                String newFile = $"Img_{DateTime.Now.ToString("yyyy-M-d_H-m-ss")}.jpg";
                String newFilepath = Path.Combine(IMG_PATH, newFile);
                System.IO.File.Copy(oldpath, newFilepath);

                using (var db = new DBSYSEntities())
                {
                    var newMenu = new AppData.Menu();
                    newMenu.MenuId = Convert.ToInt32(txtMenuId.Text);
                    newMenu.MenuName = txtMenuName.Text;
                    newMenu.MenuPrice = Convert.ToDecimal(txtPrice.Text);
                    newMenu.MenuImg = newFile;
                    newMenu.MenuCategory = cbCategory.SelectedItem.ToString(); // Assign the selected category

                    db.Menu.Add(newMenu);
                    db.SaveChanges();

                    hasChange = true;

                    // Raise the event to notify FrmMenu about the new item
                    ItemAdded?.Invoke(this, EventArgs.Empty);
                }
             
                MessageBox.Show("Uploaded!");
                
                FrmMenu M = new FrmMenu();
                M.Show();
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
            FrmMenu M = new FrmMenu();
            M.Show();
            this.Hide();
        }

        private void FrmAddMenu_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-K06JGE5P\\SQLEXPRESS;Initial Catalog=DBSYS;Integrated Security=True");
            con.Open();

            SqlCommand cmdMenu = new SqlCommand("SELECT * FROM Menu", con);
            SqlDataAdapter daMenu = new SqlDataAdapter(cmdMenu);
            DataTable dtMenu = new DataTable();
            daMenu.Fill(dtMenu);
            GetMax.GetData.GetMaxItem();
            txtMenuId.Text = GetMax.GlobalDeclaration.ItemCode.ToString();
            //dataGridViewPRICES.DataSource = dtMenu;
        }
    }
}