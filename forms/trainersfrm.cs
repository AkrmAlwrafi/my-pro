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
        private void getSpecialty()
        {

            try
            {
                Specialty.Items.Clear();

                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT DISTINCT Specialty FROM Trainers WHERE Specialty IS NOT NULL AND Specialty != ''";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Specialty.Items.Add(reader["Specialty"].ToString());
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
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
                    string query = "SELECT TrainerID,FullName,Gender,BirthDate,Phone,Email,Address,RegistrationDate,PhotoPath,ExperienceYears,Specialty FROM Trainers ORDER BY TrainerID DESC";
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
            getSpecialty();

            
            getTrainData();
            disbtns();
            setupcompo();

        }

        private void setupcompo()
        {
            filtercompo.Items.Add("الكل");
            filtercompo.Items.Add("ذكر");
            filtercompo.Items.Add("انثى");
            filtercompo.Items.Add("تم تسجيله قريبا");
            filtercompo.SelectedIndex = 0;

        }
        private void disbtns()
        {
            nametxt.Enabled = false;
            phntxt.Enabled = false;
            maltxt.Enabled = false;
            adrstxt.Enabled = false;
            femalrd.Enabled = false;
            malerd.Enabled = false;
            photoptn.Enabled = false;
            brthdy.Enabled = false;
            savebtn.Enabled = false;
            editbtn.Enabled = false;
            cancelbtn.Enabled = false;
            delbtn.Enabled = true;
            addbtn.Enabled = true;
            regsday.Enabled = false;
        }

        private void enblbtns()
        {
            nametxt.Enabled = true;
            phntxt.Enabled = true;
            maltxt.Enabled = true;
            adrstxt.Enabled = true;
            femalrd.Enabled = true;
            malerd.Enabled = true;
            photoptn.Enabled = true;
            brthdy.Enabled = true;
            savebtn.Enabled = true;
            editbtn.Enabled = false;
            cancelbtn.Enabled = true;
            delbtn.Enabled = false;
            addbtn.Enabled = false;
            regsday.Enabled = true;
        }


        private void cleartxts()
        {
            CurrentTrainerID = 0;
            nametxt.Text = "";
            phntxt.Text = "";
            maltxt.Text = "";
            femalrd.Checked = false;
            malerd.Checked = false;
            brthdy.Value = DateTime.Now;
            regsday.Value = DateTime.Now;
            picturbox.Image = null;
            currentPhotoPath = "";
            yex.Text = "";
            adrstxt.Text = "";

        }
        private bool cheek_isnull()
        {
            if (nametxt.Text == "")
            {
                MessageBox.Show("الرجاء ادخال الاسم كامل ");
                nametxt.Focus();
                return false;
            }

            if (!femalrd.Checked && !malerd.Checked)
            {
                MessageBox.Show("يرجى اختيار الجنس  ");
                return false;
            }

            if (phntxt.Text == "")
            {
                MessageBox.Show("الرجاء ادخال رقم الهاتف  ");
                phntxt.Focus();
                return false;
            }

            if (adrstxt.Text == "")
            {
                MessageBox.Show("الرجاء ادخال  العنوان   ");
                adrstxt.Focus();
                return false;
            }
            if (maltxt.Text == "")
            {
                MessageBox.Show("الرجاء ادخال رقم الهاتف  ");
                maltxt.Focus();
                return false;
            }
            if (yex.Text == "")
            {
                MessageBox.Show("الرجاء ادخال سنوات الخبره   ");
                yex.Focus();
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
            nametxt.Focus();
        }

        private void Specialty_Leave(object sender, EventArgs e)
        {
            string newSpecialty = Specialty.Text.Trim();

            if (!string.IsNullOrEmpty(newSpecialty))
            {
                if (!Specialty.Items.Contains(newSpecialty))
                {
                    Specialty.Items.Add(newSpecialty);
                }

            }
        }

        private void savebtn_Click(object sender, EventArgs e)
        {

            if (!cheek_isnull())
                return;
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();


                    string query = @"INSERT INTO Trainers (FullName,Gender,BirthDate,Phone,Email,Address, PhotoPath ,RegistrationDate,ExperienceYears,Specialty) " +
                      "VALUES (@FullName , @Gender , @BirthDate , @Phone , @Email , @Address, @PhotoPath ,  GETDATE() , @ExperienceYears , @Specialty)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FullName", nametxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gender", femalrd.Checked ? "male" : "female");
                    cmd.Parameters.AddWithValue("@BirthDate", brthdy.Value);
                    cmd.Parameters.AddWithValue("@Phone", phntxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", maltxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", adrstxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@PhotoPath", currentPhotoPath);
                    cmd.Parameters.AddWithValue("@RegistrationDate", regsday.Value);
                    cmd.Parameters.AddWithValue("@ExperienceYears", yex.Text);
                    cmd.Parameters.AddWithValue("@Specialty", Specialty.Text);




                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم الاضافه بنجاح");
                    getTrainData();
                    disbtns();




                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا" + ex.Message);
            }
        }

        private void delbtn_Click(object sender, EventArgs e)
        {
            if (CurrentTrainerID > 0)
            {
                if (MessageBox.Show("تاكيد الحذف", " هل انت متاكد من حذف المتدرب ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = DatabaseHelper.GetConnection())
                        {
                            conn.Open();
                            string query = "DELETE FROM Trainers WHERE TrainerID = @TrainerID";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@TrainerID", CurrentTrainerID);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("تم الحذف بنجاح");
                            cleartxts();
                            getTrainData();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("خطا" + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("يرجى اختيار متدرب للحذف ");
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            if (CurrentTrainerID == 0)
            {
                cleartxts();
            }
            else
            {
                getTrainData();
            }
            disbtns();
        }

        private void photoptn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "الصور|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    picturbox.Image = Image.FromFile(openFile.FileName);
                    currentPhotoPath = openFile.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطا في تحميل الصوره " + ex.Message);
                }
            }
        }

        private void dgvtrainers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvtrainers.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvtrainers.SelectedRows[0];
                CurrentTrainerID = Convert.ToInt32(row.Cells["TrainerID"].Value);
                getTrainerinfo(CurrentTrainerID);

                editbtn.Enabled = true;
                delbtn.Enabled = true;


            }
        }
        private void getTrainerinfo(int trainerID)
        {

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Trainers WHERE TrainerID = @TrainerID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TrainerID", CurrentTrainerID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        nametxt.Text = reader["FullName"].ToString();
                        if (reader["Gender"].ToString() == "Male")
                            malerd.Checked = true;
                        else
                            femalrd.Checked = true;

                        if (reader["BirthDate"] != DBNull.Value)
                            brthdy.Value = Convert.ToDateTime(reader["BirthDate"]);

                        phntxt.Text = reader["Phone"].ToString();
                        maltxt.Text = reader["Email"].ToString();
                        adrstxt.Text = reader["Address"].ToString();
                        yex.Text = reader["ExperienceYears"].ToString();
                        Specialty.Text = reader["Specialty"].ToString();

                        if (reader["RegistrationDate"] != DBNull.Value)
                            regsday.Value = Convert.ToDateTime(reader["RegistrationDate"]);


                        currentPhotoPath = reader["PhotoPath"].ToString();
                        if (!string.IsNullOrEmpty(currentPhotoPath) && File.Exists(currentPhotoPath))
                        {
                            picturbox.Image = Image.FromFile(currentPhotoPath);
                        }
                        else
                        {
                            picturbox.Image = null;
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ " + ex.Message);

            }
        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            if (CurrentTrainerID > 0)
            {


                enblbtns();
                nametxt.Focus();



            }
            else
            {
                MessageBox.Show("اختر المدرب الي تريد تعديله ");
            }
        }

        private void uploadbtn_Click(object sender, EventArgs e)
        {
            if (!cheek_isnull()) return;

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE Trainers SET 
                           FullName = @FullName, Gender = @Gender, BirthDate = @BirthDate,
                           Phone = @Phone, Email = @Email, Address = @Address, PhotoPath = @PhotoPath , RegistrationDate=@RegistrationDate, ExperienceYears=@ExperienceYears ,Specialty =@Specialty
                           WHERE TrainerID = @TrainerID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TrainerID", CurrentTrainerID);
                    cmd.Parameters.AddWithValue("@FullName", nametxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gender", femalrd.Checked ? "male" : "female");
                    cmd.Parameters.AddWithValue("@BirthDate", brthdy.Value);
                    cmd.Parameters.AddWithValue("@Phone", phntxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", maltxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", adrstxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@PhotoPath", currentPhotoPath);
                    cmd.Parameters.AddWithValue("@RegistrationDate", regsday.Value);
                    cmd.Parameters.AddWithValue("@ExperienceYears", yex.Text);
                    cmd.Parameters.AddWithValue("@Specialty", Specialty.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("تم تعديل بيانات المدرب بنجاح");
                    getTrainData();
                    disbtns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في التعديل: " + ex.Message);
            }
        }

        private void serchbtn_Click(object sender, EventArgs e)
        {
            SearchTrainers();

        }
        private void SearchTrainers()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT TrainerID,FullName,Gender,BirthDate,Phone,Email,Address,RegistrationDate,PhotoPath,ExperienceYears,Specialty 
                           FROM Trainers 
                           WHERE 1=1";

                    List<SqlParameter> parameters = new List<SqlParameter>();

                    if (!string.IsNullOrWhiteSpace(serchtxt.Text))
                    {
                        query += " AND FullName LIKE @Search";
                        parameters.Add(new SqlParameter("@Search", "%" + serchtxt.Text.Trim() + "%"));
                    }

                    if (filtercompo.SelectedIndex > 0)
                    {
                        if (filtercompo.SelectedIndex == 1)
                        {
                            query += " AND Gender = 'Male'";
                        }
                        else if (filtercompo.SelectedIndex == 2)
                        {
                            query += " AND Gender = 'Female'";
                        }
                        else if (filtercompo.SelectedIndex == 3)
                        {
                            query += " AND RegistrationDate >= @RecentDate";
                            parameters.Add(new SqlParameter("@RecentDate", DateTime.Now.AddDays(-7)));
                        }
                    }

                    query += " ORDER BY TrainerID DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                    foreach (var param in parameters)
                    {
                        adapter.SelectCommand.Parameters.Add(param);
                    }

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvtrainers.DataSource = dt;
                    dgvtrainers.Columns["TrainerID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في البحث: " + ex.Message);
            }
        }

        private void phntxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < 48 || e.KeyChar > 57)
                e.Handled = true;
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void nametxt_TextChanged(object sender, EventArgs e)
        {

           
        }

        private void nametxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 65 || e.KeyChar > 90) &&
               (e.KeyChar < 97 || e.KeyChar > 122) &&
               (e.KeyChar < 1570 || e.KeyChar > 1610) &&
               e.KeyChar != 32 &&
               e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void yex_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar < 48 || e.KeyChar > 57)
                e.Handled = true;
            if (e.KeyChar == 8)
                e.Handled = false;
        }
    }
}