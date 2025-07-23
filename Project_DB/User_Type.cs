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
    public partial class User_Type : Form
    {
        public User_Type()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            
            if (rb_admin.Checked)
            {
                Admin_Log_In form_admin = new Admin_Log_In();
                form_admin.ShowDialog();
            }

            if (rb_op.Checked)
            {
                Operator_LogIn form_Operator =new Operator_LogIn();
                form_Operator.ShowDialog();
            }

            if (rb_user.Checked)
            {
                User_Log_In form_user = new User_Log_In();
                form_user.ShowDialog();
            }

            if (rb_provider.Checked)
            {
                Provider_LogIn form_provider = new Provider_LogIn();
                form_provider.ShowDialog();
            }

          //  this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit(); // This stops the entire project
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
