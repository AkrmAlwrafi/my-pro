using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace institute_system
{
    public partial class loginfrm : Form
    {
        public loginfrm()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string username = txtpassword.Text;
            string password = txtusername.Text;

        }

        private void loginfrm_Load(object sender, EventArgs e)
        {
            txtusername.Text = "ادخل اسم المستخدم";
            txtusername.ForeColor = Color.Gray;
            txtpassword.Text = "ادخل كلمه المرور";
            txtusername.ForeColor = Color.Gray;


        }

        private void txtusername_Enter(object sender, EventArgs e)
        {
            if(txtusername.Text=="ادخل اسم المستخدم")
            {
                txtusername.Text = "";
                txtusername.ForeColor = Color.Black;
            }
        }

        private void txtusername_Leave(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtusername.Text))
            {
                txtusername.Text = "ادخل اسم المستخدم";
                txtusername.ForeColor = Color.Gray;
            }
        }

        private void txtpassword_Enter(object sender, EventArgs e)
        {
            if (txtpassword.Text == "ادخل كلمه المرور")
            {
                txtpassword.Text = "";
                txtpassword.ForeColor = Color.Black;
                txtpassword.PasswordChar = '*';
            }
        }

        private void txtpassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtpassword.Text))
            {
                txtpassword.Text = "";
                txtpassword.ForeColor = Color.Gray;
                txtpassword.PasswordChar = '\0';

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void showpass_CheckedChanged(object sender, EventArgs e)
        {
            if(showpass.Checked)
            {
                txtpassword.PasswordChar = '\0';

            }
            else
            {
                if(txtpassword.Text!= "ادخل كلمه المرور")
                    txtpassword.PasswordChar = '*';

            }
        }
    }
}
