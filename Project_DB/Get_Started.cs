using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DB
{
    public partial class Get_Started : Form
    {
        public Get_Started()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            User_Type form = new User_Type();
            form.ShowDialog();

           // this.Close();
        }

        private void Get_Started_Load(object sender, EventArgs e)
        {

          //  this.reportViewer1.RefreshReport();
        }
    }
}
