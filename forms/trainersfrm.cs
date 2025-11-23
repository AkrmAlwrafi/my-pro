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
    public partial class trainersfrm : Form
    {
        private int CurrentTrainerID = 0;
        private string currentPhotoPath = "";
        public trainersfrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
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
                    string query = "SELECT TrainerID,FullName,Gender,BirthDate,Phone,Email,Address,RegistrationDate,PhotoPath,ExperienceYears,Specialty FROM Trainees ORDER BY TraineeID DESC";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvtrainers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا" + ex.Message);
            }
        }

        private void trainersfrm_Load(object sender, EventArgs e)
        {
            getTrainData();
            disbtns();
            setupcompo();
        }

        private void setupcompo()
        {
            combofilter.Items.Add("الكل");
            combofilter.Items.Add("ذكر");
            combofilter.Items.Add("انثى");
            combofilter.Items.Add("تم تسجيله قريبا");
            combofilter.SelectedIndex = 0;

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
            btndelete.Enabled = true;
            btnadd.Enabled = true;
            dtprgster.Enabled = false;
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
            dtprgster.Enabled = true;
        }


        private void cleartxts()
        {
            CurrentTrainerID = 0;
            txtname.Text = "";
            txtphn.Text = "";
            txtmail.Text = "";
            txtaddrs.Text = "";
            radfemale.Checked = false;
            radmale.Checked = false;
            dtpbrith.Value = DateTime.Now;
            dtprgster.Value = DateTime.Now;
            pictsinees.Image = null;
            currentPhotoPath = "";
            yex.Text = "";
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
            if (yex.Text == "")
            {
                MessageBox.Show("الرجاء ادخال سنوات الخبره   ");
                txtmail.Focus();
                return false;
            }


            return true;
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            cleartxts();
            enblbtns();
            txtname.Focus();
        }
    }
}
