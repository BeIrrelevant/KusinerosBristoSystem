﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dbsys.AppData;
using Dbsys.Forms;

namespace Dbsys
{
    
    public partial class FrmLogin : Form
    {
        UserRepository userRepo;
        public FrmLogin()
        {
            InitializeComponent();
            //
            userRepo = new UserRepository();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtUsername.Text))
            {
                errorProviderCustom.SetError(txtUsername, "Empty Field!");
                return;
            }
            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                errorProviderCustom.SetError(txtPassword, "Empty Field!");
                return;
            }

            var userLogged = userRepo.GetUserByUsername(txtUsername.Text);

            if (userLogged != null)
            {
                if (userLogged.userPassword.Equals(txtPassword.Text))
                {

                    UserLogged.GetInstance().UserAccount = userLogged;

                    switch ((Role)userLogged.roleId)
                    {
                       
                        case Role.Admin:
                            new FrmAdminDashboard().Show();
                            this.Hide();
                            break;
                        case Role.Cashier:
                            new FrmCashierDashBoard().Show();
                            this.Hide();
                            break;
                        case Role.Customer:
                           new FrmCustomerDashboard().Show();
                            this.Hide();
                            break;
                        default:
                            MessageBox.Show("User has no role!");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Password");
                }
            }
            else
            {
                MessageBox.Show("Username not found");
            }

        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {

        }

        private void linkLabelRigester_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            FrmRegister frm = new FrmRegister();
            frm.ShowDialog();

            txtUsername.Text = frm.username;

           
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
        }
    }
}
