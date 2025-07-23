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
    public partial class review_choice_op : Form
    {
        public review_choice_op()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            MyReviewsOp form_reviews_given = new MyReviewsOp();
            form_reviews_given.ShowDialog();

            this.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            ReviewsRecOp form_reviews_received = new ReviewsRecOp();
            form_reviews_received.ShowDialog();

            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
