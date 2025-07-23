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
    public partial class User_Functions : Form
    {
        public string UserEmail { get; set; } 

        public User_Functions()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Information_Change form_information_change = new Information_Change();
            form_information_change.UserEmail = UserEmail;
            form_information_change.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChooseTrip form_chooseTrip = new ChooseTrip();
            form_chooseTrip.ShowDialog();

            //this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            User_Type form_my_booking = new User_Type();
            form_my_booking.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            MyBookings form_my_booking = new MyBookings();
            form_my_booking.UE = UserEmail;
            form_my_booking.ShowDialog();

           
        }

        private void button4_Click(object sender, EventArgs e)
        {
          

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            MyReviewsCust form_review_given = new MyReviewsCust();
            form_review_given.UE = UserEmail;
            form_review_given.ShowDialog();

        }
    }
}
