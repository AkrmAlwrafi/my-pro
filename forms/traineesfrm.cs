using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace institute_system
{
    public partial class traineesfrm : Form
    {
        private int CurrentTraineeID = 0;
        private string currentPhotoPath = "";
        public traineesfrm()
        {
            InitializeComponent();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void getTrainData()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT TraineeID,FullName,Gender,BirthDate,Phone,Email,Address,RegistrationDate,PhotoPath FROM Trainees ORDER BY TraineeID DESC";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvtrainees.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا"+ex.Message);
            }
        }

        private void traineesfrm_Load(object sender, EventArgs e)
        {
            getTrainData();
          


        }

        private void disbtns()
        {
            txtname.Enabled = false;
            txtphn.Enabled = false;
            txtmail.Enabled = false;
            txtaddrs.Enabled = false;
            radfemale.Enabled = false;
            radmale.Enabled = false;
            btnaddphto.Enabled = false;
            dtpbrith.Enabled = false;
            btnsave.Enabled = false;
            btnedit.Enabled = false;
            btncancel.Enabled = false;
            btndelete.Enabled = false;
            btnadd.Enabled = true;
        }

        private void enblbtns()
        {
            txtname.Enabled = true;
            txtphn.Enabled = true;
            txtmail.Enabled = true;
            txtaddrs.Enabled = true;
            radfemale.Enabled = true;
            radmale.Enabled = true;
            btnaddphto.Enabled = true;
            dtpbrith.Enabled = true;
            btnsave.Enabled = true;
            btnedit.Enabled = false;
            btncancel.Enabled = true;
            btndelete.Enabled = false;
            btnadd.Enabled = false;
        }


        private void cleartxts()
        {
            CurrentTraineeID = 0;
            txtname.Text = "";
            txtphn.Text = "";
            txtmail.Text = "";
            txtaddrs.Text = "";
            radfemale.Checked= false;
            radmale.Checked= false;
            dtpbrith.Value=DateTime.Now;
            dtprgster.Value=DateTime.Now;
            pictsinees.Image = null;
            currentPhotoPath = "";
        }

        private bool cheek_isnull()
        {
            if (txtname.Text == "")
            {
                MessageBox.Show("الرجاء ادخال الاسم كامل ");
                txtname.Focus();
                return false;
            }

            if (!radfemale.Checked && !radmale.Checked)
            {
                MessageBox.Show("يرجى اختيار الجنس  ");
                return false;
            }

            if (txtphn.Text == "")
            {
                MessageBox.Show("الرجاء ادخال رقم الهاتف  ");
                txtphn.Focus();
                return false;
            }

            if (txtaddrs.Text == "")
            {
                MessageBox.Show("الرجاء ادخال  العنوان   ");
                txtaddrs.Focus();
                return false;
            }
            if (txtmail.Text == "")
            {
                MessageBox.Show("الرجاء ادخال رقم الهاتف  ");
                txtmail.Focus();
                return false;
            }
            return true;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            cleartxts();
            enblbtns();
            txtname.Focus();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (!cheek_isnull())
                return;
            try
            {
                using(SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();



                    string query=@"INSERT INTO Trainees (FullName , Gender , BirthDate , Phone , Email , Address , PhotoPath , RegistrationDate) "+
                      "VALUES (@FullName , @Gender , @BirthDate , @Phone , @Email , @Address, @PhotoPath ,  GETDATE())";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FullName", txtname.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gender", radfemale.Checked ? "male" : "female");
                    cmd.Parameters.AddWithValue("@BirthDate", dtpbrith.Value);
                    cmd.Parameters.AddWithValue("@Phone", txtphn.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtaddrs.Text.Trim());
                    cmd.Parameters.AddWithValue("@PhotoPath", currentPhotoPath);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم الاضافه بنجاح");
                    getTrainData();
                    disbtns();
                    



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا"+ex.Message);
            }
        }
    }
}
