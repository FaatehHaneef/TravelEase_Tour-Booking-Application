using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forms_db_proj;

namespace Project_DB
{
    public partial class review_choice_provider : Form
    {
        public review_choice_provider()
        {
            InitializeComponent();
        }

        public string Hd { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            MyReviewsProv form_my_reviews = new MyReviewsProv();
            form_my_reviews.ShowDialog();

            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            ReviewsRecProv form_reviews_received = new ReviewsRecProv();
            form_reviews_received.ShowDialog();

            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
