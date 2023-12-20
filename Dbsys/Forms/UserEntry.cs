using Dbsys.AppData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dbsys.Forms
{
    public partial class UserEntry : Form
    {
        public string username = String.Empty;
        DBSYSEntities db;

        UserRepository userRepo;
        int? userSelectedId = null;



        public UserEntry()
        {
            InitializeComponent();
            db = new DBSYSEntities();
            userRepo = new UserRepository(); // Instantiate the UserRepository
            dgv_main.DataSource = userRepo.UserAccounts();
            dgv_main.DataSource = null;
        }

        private void loadUser()
        {
            dgv_main.DataSource = userRepo.UserAccounts();
        }

        private void UserEntry_Load(object sender, EventArgs e)
        {
            loadCbBoxRole();
            userRepo = new UserRepository();
            loadUser();
            
        }

        public void loadCbBoxRole()
        {
            var roles = db.Role.ToList();

            cbBoxRole.ValueMember = "roleId";
            cbBoxRole.DisplayMember = "roleName";
            cbBoxRole.DataSource = roles;
        }

        private void btnRegistion_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtUsername.Text))
                {
                    errorProviderCustom.SetError(txtUsername, "Empty field");
                    return;
                }
                if (String.IsNullOrEmpty(txtPassword.Text))
                {
                    errorProviderCustom.Clear();
                    errorProviderCustom.SetError(txtPassword, "Empty field");
                    return;
                }

                UserAccount nUserAccount = new UserAccount();
                nUserAccount.userName = txtUsername.Text;
                nUserAccount.userPassword = txtPassword.Text;
                nUserAccount.roleId = (int)cbBoxRole.SelectedValue; // Directly cast to int
                nUserAccount.userStatus = "Active";

                username = txtUsername.Text;

                db.UserAccount.Add(nUserAccount);
                db.SaveChanges();

                txtPassword.Clear();
                txtUsername.Clear();
                UserEntry_Load(sender, e);
                dgv_main.Refresh();
                clearfields();
                MessageBox.Show("Registered!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }

        private void dgv_main_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                userSelectedId = (int)dgv_main.Rows[e.RowIndex].Cells[0].Value;
                txtUsername.Text = dgv_main.Rows[e.RowIndex].Cells[1].Value as string;
                txtPassword.Text = dgv_main.Rows[e.RowIndex].Cells[2].Value as string;

                // Retrieve the user account from the repository
                var userAccount = userRepo.GetUserById(userSelectedId);

                // Set the selected item in the ComboBox to the role of the selected user
                if (userAccount != null)
                {
                    cbBoxRole.SelectedValue = userAccount.roleId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text;
                string pass = txtPassword.Text;

                string strOutputMsg = "";

                if (String.IsNullOrEmpty(username))
                {
                    errorProviderCustom.SetError(txtUsername, "Empty Field!");
                    return;
                }
                if (String.IsNullOrEmpty(pass))
                {
                    errorProviderCustom.SetError(txtPassword, "Empty Field!");
                    return;
                }

                // Retrieve the user account from the repository
                var userAccount = userRepo.GetUserById(userSelectedId);

                // Update the properties of the user account with new values
                userAccount.userName = username;
                userAccount.userPassword = pass;
                userAccount.roleId = (int)cbBoxRole.SelectedValue;

                // Call the UpdateUser method in the repository
                ErrorCode retValue = userRepo.UpdateUser(userSelectedId, userAccount, ref strOutputMsg);

                if (retValue == ErrorCode.Success)
                {
                    errorProviderCustom.Clear();
                    MessageBox.Show(strOutputMsg, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UserEntry_Load(sender, e); // Reload the updated user data in the DataGridView
                    userSelectedId = null;
                }
                else
                {
                    MessageBox.Show(strOutputMsg, "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string pass = txtPassword.Text;
            string strOutputMsg = "";

            if (userSelectedId == null)
            {
                MessageBox.Show("No User Selected", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ErrorCode retValue = userRepo.RemoveUser(userSelectedId, ref strOutputMsg);
            if (retValue == ErrorCode.Success)
            {
                errorProviderCustom.Clear();
                MessageBox.Show(strOutputMsg, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadUser();
                userSelectedId = null;
                txtPassword.Clear();
                txtUsername.Clear();
            }
            else
            {
                MessageBox.Show(strOutputMsg, "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void clearfields()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            cbBoxRole.SelectedIndex = 0;
          
        }

        private void ckShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (ckShowPass.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}