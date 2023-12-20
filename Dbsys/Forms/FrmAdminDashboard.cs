using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dbsys.Forms
{
    public partial class FrmAdminDashboard : Form
    {
        public FrmAdminDashboard()
        {
            InitializeComponent();
        }
        public void AddControls(Form f)
        {
            MainPanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            MainPanel.Controls.Add(f);
            f.Show();

        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            AddControls(new FrmHome());
        }

        private void FrmAdminDashboard_Load(object sender, EventArgs e)
        {
            lblUserName.Text = UserLogged.GetInstance().UserAccount.userName;
  
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            AddControls(new FrmMenu());
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            FrmPOS POS = new FrmPOS();
            POS.Show();
            this.Hide();
            
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            AddControls(new FrmReports());
        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            AddControls(new UserEntry());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FrmLogin LO = new FrmLogin();
            LO.Show();
            this.Hide();
        }
    }
}
