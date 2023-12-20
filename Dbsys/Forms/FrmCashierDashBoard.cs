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
    public partial class FrmCashierDashBoard : Form
    {
        public FrmCashierDashBoard()
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

        private void btnPOS_Click(object sender, EventArgs e)
        {
            AddControls(new FrmCashierPOS());
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            AddControls(new FrmCashierInventory());
        }

        private void FrmCashierDashBoard_Load(object sender, EventArgs e)
        {
            lblUserId.Text = UserLogged.GetInstance().UserAccount.userId.ToString();
            lblUseraName.Text = UserLogged.GetInstance().UserAccount.userName.ToString();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            FrmLogin log = new FrmLogin();
            log.Show();
            this.Hide();

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            AddControls(new FrmHome());
        }
    }
}
