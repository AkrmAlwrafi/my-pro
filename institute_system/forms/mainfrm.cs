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
    public partial class mainfrm : Form
    {
        public mainfrm()
        {
            InitializeComponent();
            UpdateStatusStrip();
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
    }
}
