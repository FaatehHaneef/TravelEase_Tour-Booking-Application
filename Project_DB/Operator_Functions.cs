using Forms_db_proj;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp4;

namespace Project_DB
{
    public partial class Operator_Functions : Form
    {
        public Operator_Functions()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");
        public string UserEmail { get; set; }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // First, get the UserID from IUserClass using the UserEmail
                string userIdQuery = "SELECT UserID FROM UserClass WHERE Email = @Email";
                SqlCommand cmdUser = new SqlCommand(userIdQuery, conn);
                cmdUser.Parameters.AddWithValue("@Email", UserEmail);

                conn.Open();
                object userIdResult = cmdUser.ExecuteScalar();
                conn.Close();

                if (userIdResult != null)
                {
                    int userId = Convert.ToInt32(userIdResult);

                    // Then, get the HotelID from Hotel using the UserID
                    string hotelIdQuery = "SELECT UserID FROM TourOperatorProfile WHERE UserID = @UserID";
                    SqlCommand cmdHotel = new SqlCommand(hotelIdQuery, conn);
                    cmdHotel.Parameters.AddWithValue("@UserID", userId);

                    conn.Open();
                    object hotelIdResult = cmdHotel.ExecuteScalar();
                    conn.Close();

                    if (hotelIdResult != null)
                    {
                        int hotelId = Convert.ToInt32(hotelIdResult);

                        // Pass HotelID to the next form
                        this.Hide();
                        MyReviewsOp form_review_choice = new MyReviewsOp();
                        form_review_choice.Hd = hotelId;
                        form_review_choice.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        MessageBox.Show("No hotel found for this user.");
                    }
                }
                else
                {
                    MessageBox.Show("User not found with this email.");
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Trip_Addition form_addtrip = new Trip_Addition();
            form_addtrip.ShowDialog();

            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Trip_Deletion form_delecttrip = new Trip_Deletion();
            form_delecttrip.ShowDialog();

            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Information_Change form_information_change = new Information_Change();
            form_information_change.UserEmail = UserEmail;
            form_information_change.ShowDialog();

            this.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Active_Trips_Form form_active_trip = new Active_Trips_Form();
            form_active_trip.UserEmail = UserEmail;
            form_active_trip.ShowDialog();

            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            User_Type form_active_trip = new User_Type();
            form_active_trip.ShowDialog();
        }
    }
}
