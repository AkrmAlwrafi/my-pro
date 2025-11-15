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
    public partial class flashfrm : Form
    {
        public flashfrm()
        {
            InitializeComponent();
        }

        private void flashfrm_Load(object sender, EventArgs e)
        {

        }
            int x = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {

            x++;

            progressBar1.Value = x * 20;

            if (progressBar1.Value >= 100)
            {
                timer1.Stop(); 

                loginfrm login = new loginfrm();
                login.Show();
                this.Hide();


            }


        }
    }
}
