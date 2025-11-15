using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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
            string username = txtusername.Text;
            string password = txtpassword.Text;

          

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("يرجى إدخال اسم المستخدم وكلمة المرور");
                return;
            }

            if (CheckUserInDatabase(username, password))
            {
                MessageBox.Show($"تم الدخول بنجاح {Global.CurrentUserRole}");
                this.Hide();
                mainfrm mainfrm= new mainfrm();
                mainfrm.Show();
            }
            else
            {
                MessageBox.Show("اسم المستخدم أو كلمة المرور غير صحيحة");
                txtpassword.Text = "";
            }


        }

        private void loginfrm_Load(object sender, EventArgs e)
        {
            txtusername.Text = "ادخل اسم المستخدم";
            txtusername.ForeColor = Color.Gray;
            txtpassword.Text = "ادخل كلمه المرور";
            txtpassword.ForeColor = Color.Gray;


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
                txtpassword.ForeColor = Color.Black;
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
                    txtpassword.PasswordChar = '*';

            }
        }


        private bool CheckUserInDatabase(string username , string password)
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT UserID, Username, Role FROM Users WHERE Username = @Username AND Password = @Password AND IsActive = 1";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Global.CurrentUserID = reader.GetInt32(0);
                        Global.CurrentUsername = reader.GetString(1);
                        Global.CurrentUserRole = reader.GetString(2);

                        reader.Close();
                        return true;
                    }

                    reader.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                return false;
            }

        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();    
        }
    }
}
