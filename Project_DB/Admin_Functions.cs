using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Delete_Entity;
using Forms_db_proj;
using My_Reviews;
using Operator_Review;

namespace Project_DB
{
    public partial class Admin_Functions : Form
    {
        public Admin_Functions()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Delete_Form DeleteForm = new Delete_Form();
            DeleteForm.ShowDialog();


            this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            User_Type DeleteForm = new User_Type();
            DeleteForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            request_manage requestmanage = new request_manage();
            requestmanage.ShowDialog();

            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            My_Reviews_Form manage_reviews = new My_Reviews_Form();
            manage_reviews.ShowDialog();

            this.Show();
        }
    }
}
