using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project_DB;

namespace Forms_db_proj
{
    public partial class ConfTrip : Form
    {
        public ConfTrip()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            User_Functions form_userfun = new User_Functions();
            form_userfun.ShowDialog();
        }
    }
}
