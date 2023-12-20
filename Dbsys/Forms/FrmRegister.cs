using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dbsys.AppData;

namespace Dbsys.Forms
{
    public partial class FrmRegister : Form
    {
        public string username = String.Empty;
        DBSYSEntities db;

        public FrmRegister()
        {
            InitializeComponent();
            db = new DBSYSEntities();
        }

        private void Frm_Register_Load(object sender, EventArgs e)
        {
            SetDefaultRole();
        }

        private void SetDefaultRole()
        {
            Dbsys.AppData.Role defaultRole = db.Role.FirstOrDefault(r => r.roleName == "Customer" && r.roleId == 3);

            if (defaultRole != null)
            {
                lblRoleCustomer.Text = defaultRole.roleName;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtUsername.Text))
                {
                    errorProvider1.SetError(txtUsername, "Empty field");
                    return;
                }
                if (String.IsNullOrEmpty(txtPassword.Text))
                {
                    errorProvider1.Clear();
                    errorProvider1.SetError(txtPassword, "Empty field");
                    return;
                }
                if (String.IsNullOrEmpty(txtRepassword.Text))
                {
                    errorProvider1.Clear();
                    errorProvider1.SetError(txtPassword, "Empty field");
                    return;
                }

                if (!txtPassword.Text.Equals(txtRepassword.Text))
                {
                    errorProvider1.Clear();
                    errorProvider1.SetError(txtRepassword, "Password does not match");
                    return;
                }

                Dbsys.AppData.Role nRole = db.Role.FirstOrDefault(r => r.roleName == "Customer" && r.roleId == 3);
                if (nRole == null)
                {
                    MessageBox.Show("Default role not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                UserAccount nUserAccount = new UserAccount();
                nUserAccount.userName = txtUsername.Text;
                nUserAccount.userPassword = txtPassword.Text;
                nUserAccount.roleId = nRole.roleId;
                nUserAccount.userStatus = "Active";

                username = txtUsername.Text;

                db.UserAccount.Add(nUserAccount);
                db.SaveChanges();

                txtPassword.Clear();
                txtRepassword.Clear();
                txtUsername.Clear();

                MessageBox.Show("Sign Up successfully", "Sign up", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                FrmLogin log = new FrmLogin();
                log.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ckBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {

            if (ckBoxShowPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }

            if (ckBoxShowPassword.Checked)
            {
                txtRepassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtRepassword.UseSystemPasswordChar = true;
            }
        }
    }
}