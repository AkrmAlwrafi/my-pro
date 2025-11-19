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
                MessageBox.Show("خطا" + ex.Message);
            }
        }

        private void traineesfrm_Load(object sender, EventArgs e)
        {
            getTrainData();
            disbtns();
            getTrainData();
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
            dtprgster.Enabled= true;
        }


        private void cleartxts()
        {
            CurrentTraineeID = 0;
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
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();



                    string query = @"INSERT INTO Trainees (FullName , Gender , BirthDate , Phone , Email , Address , PhotoPath , RegistrationDate) " +
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
                MessageBox.Show("خطا" + ex.Message);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (CurrentTraineeID > 0)
            {
                if (MessageBox.Show("تاكيد الحذف", " هل انت متاكد من حذف المتدرب ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = DatabaseHelper.GetConnection())
                        {
                            conn.Open();
                            string query = "DELETE FROM Trainees WHERE TraineeID = @TraineeID";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@TraineeID", CurrentTraineeID);
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

        private void btncancel_Click(object sender, EventArgs e)
        {
            if (CurrentTraineeID == 0)
            {
                cleartxts();
            }
            else
            {
                getTrainData();
            }
            disbtns();
        }

        private void btnaddphto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "الصور|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictsinees.Image = Image.FromFile(openFile.FileName);
                    currentPhotoPath = openFile.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطا في تحميل الصوره " + ex.Message);
                }
            }
        }

        private void dgvtrainees_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvtrainees.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvtrainees.SelectedRows[0];
                CurrentTraineeID = Convert.ToInt32(row.Cells["TraineeID"].Value);
                getTraineeinfo(CurrentTraineeID);

                btnedit.Enabled = true;
                btndelete.Enabled = true;


            }
        }

        private void getTraineeinfo(int traineeID)
        {

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Trainees WHERE TraineeID = @TraineeID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TraineeID", CurrentTraineeID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtname.Text = reader["FullName"].ToString();
                        if (reader["Gender"].ToString() == "Male")
                            radmale.Checked = true;
                        else
                            radfemale.Checked = true;

                        if (reader["BirthDate"] != DBNull.Value)
                            dtpbrith.Value = Convert.ToDateTime(reader["BirthDate"]);

                        txtphn.Text = reader["Phone"].ToString();
                        txtmail.Text = reader["Email"].ToString();
                        txtaddrs.Text = reader["Address"].ToString();

                        if (reader["RegistrationDate"] != DBNull.Value)
                            dtprgster.Value = Convert.ToDateTime(reader["RegistrationDate"]);


                        currentPhotoPath = reader["PhotoPath"].ToString();
                        if (!string.IsNullOrEmpty(currentPhotoPath) && File.Exists(currentPhotoPath))
                        {
                            pictsinees.Image = Image.FromFile(currentPhotoPath);
                        }
                        else
                        {
                            pictsinees.Image = null;
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

        private void btnedit_Click(object sender, EventArgs e)
        {
            if(CurrentTraineeID > 0)
            {


            enblbtns();
            txtname.Focus(); 
            
            
            
            }
            else
            {
                MessageBox.Show("اختر المتدرب الي تريد تعديله ");
            }
        }

        private void upldbtn_Click(object sender, EventArgs e)
        {
         
                if (!cheek_isnull()) return;

                try
                {
                    using (SqlConnection conn = DatabaseHelper.GetConnection())
                    {
                        conn.Open();
                        string query = @"UPDATE Trainees SET 
                           FullName = @FullName, Gender = @Gender, BirthDate = @BirthDate,
                           Phone = @Phone, Email = @Email, Address = @Address, PhotoPath = @PhotoPath
                           WHERE TraineeID = @TraineeID";

                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@TraineeID", CurrentTraineeID);
                        cmd.Parameters.AddWithValue("@FullName", txtname.Text.Trim());
                        cmd.Parameters.AddWithValue("@Gender", radmale.Checked ? "Male" : "Female");
                        cmd.Parameters.AddWithValue("@BirthDate", dtpbrith.Value);
                        cmd.Parameters.AddWithValue("@Phone", txtphn.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Address", txtaddrs.Text.Trim());
                        cmd.Parameters.AddWithValue("@PhotoPath", currentPhotoPath);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("تم تعديل بيانات المتدرب بنجاح");
                        getTrainData();
                        disbtns();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطأ في التعديل: " + ex.Message);
                }
            
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            SearchTrainees();
        }

       

        private void SearchTrainees()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT TraineeID, FullName, Gender, BirthDate, Phone, Email, Address, RegistrationDate, PhotoPath 
                           FROM Trainees 
                           WHERE 1=1";

                    List<SqlParameter> parameters = new List<SqlParameter>();

                    if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                    {
                        query += " AND FullName LIKE @Search";
                        parameters.Add(new SqlParameter("@Search", "%" + txtSearch.Text.Trim() + "%"));
                    }

                    if (combofilter.SelectedIndex > 0)
                    {
                        if (combofilter.SelectedIndex == 1)
                        {
                            query += " AND Gender = 'Male'";
                        }
                        else if (combofilter.SelectedIndex == 2) 
                        {
                            query += " AND Gender = 'Female'";
                        }
                        else if (combofilter.SelectedIndex == 3) 
                        {
                            query += " AND RegistrationDate >= @RecentDate";
                            parameters.Add(new SqlParameter("@RecentDate", DateTime.Now.AddDays(-7)));
                        }
                    }

                    query += " ORDER BY TraineeID DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                    foreach (var param in parameters)
                    {
                        adapter.SelectCommand.Parameters.Add(param);
                    }

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvtrainees.DataSource = dt;
                    dgvtrainees.Columns["TraineeID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في البحث: " + ex.Message);
            }
        }

        private void txtmail_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void txtphn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '+' && txtphn.Text.Length > 0)
            {
                e.Handled = true;
            }

        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (char.IsDigit(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }

        }
    }
}

