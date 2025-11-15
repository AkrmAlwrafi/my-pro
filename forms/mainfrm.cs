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
    public partial class mainfrm : Form
    {
        public mainfrm()
        {
            InitializeComponent();
            UpdateStatusStrip();
            Statistics();
            CheekRole();
        }

        private void systeminfo_Click(object sender, EventArgs e)
        {

        }
        private void UpdateStatusStrip()
        {
            userinfo.Text = $"المستخدم: {Global.CurrentUsername} ({Global.CurrentUserRole})";
        }




        private void closebtn_Click(object sender, EventArgs e)
        {
            loginfrm f = new loginfrm();
            f.Show();
            this.Close();

        }

        private void المتدربونToolStripMenuItem_Click(object sender, EventArgs e)
        {
            traineesfrm f = new traineesfrm();
            f.ShowDialog();
        }

        private void المدربونToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trainersfrm f = new trainersfrm();
            f.ShowDialog();
        }

        private void الدوراتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            coursfrm f = new coursfrm();
            f.ShowDialog();
        }

        private void التسجيلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enrofrm f = new enrofrm();
            f.ShowDialog();
        }

        private void النتائجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resfrm f = new resfrm();
            f.ShowDialog();
        }

        private void الشهاداتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cerfrm f = new cerfrm();
            f.ShowDialog();
        }

        private void المستخدمينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userfrm f = new userfrm();
            f.ShowDialog();
        }

        private void Statistics()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM Trainees";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    ltrees.Text = ((int)cmd.ExecuteScalar()).ToString();

                    query = "SELECT COUNT(*) FROM Trainers";
                    cmd = new SqlCommand(query, conn);
                    ltrers.Text = ((int)cmd.ExecuteScalar()).ToString();

                    query = "SELECT COUNT(*) FROM Courses WHERE Enddate >= GETDATE()";
                    cmd = new SqlCommand(query, conn);
                    lcor.Text = ((int)cmd.ExecuteScalar()).ToString();

                    query = "SELECT COUNT(*) FROM Certificates";
                    cmd = new SqlCommand(query, conn);
                    lcer.Text = ((int)cmd.ExecuteScalar()).ToString();

                    query = "SELECT COUNT(*) FROM Enrollments WHERE Status = 'Active' ";
                    cmd = new SqlCommand(query, conn);
                    lenr.Text = ((int)cmd.ExecuteScalar()).ToString();


                    query = "SELECT COUNT(*) FROM Results";
                    cmd = new SqlCommand(query, conn);
                    lres.Text = ((int)cmd.ExecuteScalar()).ToString();

                    query = "SELECT COUNT(*) FROM Users WHERE IsActive = 1";
                    cmd = new SqlCommand(query, conn);
                    lusr.Text = ((int)cmd.ExecuteScalar()).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                lres.Text = "0";
                ltrees.Text = "0";
                ltrers.Text = "0";
                lenr.Text = "0";
                lusr.Text = "0";
                lcor.Text = "0";
                lcer.Text = "0";

            }
        }

        private void CheekRole()
        {
            if (Global.CurrentUserRole == "employee")
            {
                foreach (ToolStripItem item in menuitem.Items)
                {
                    if (item.Text.Contains("المستخدمين"))
                    { 
                        item.Visible = false;
                        groupBox7.Visible = false;
                        break;
                    }
                }
            }
        }


    }
}
