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
    public partial class Information_Change : Form
    {
        public string UserEmail { get; set; }
        public Information_Change()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Email_Change form_mail = new Email_Change();
            form_mail.UE = UserEmail;
            form_mail.ShowDialog();

            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Name_Change form_name = new Name_Change();
            form_name.UE = UserEmail;
            form_name.ShowDialog();

            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Password_Change form_pass = new Password_Change();
            form_pass.UE = UserEmail;
            form_pass.ShowDialog();

            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            User_Type form_pass = new User_Type();
            form_pass.ShowDialog();
        }
    }
}
