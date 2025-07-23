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
using Active_Recommendations;
using Forms_db_proj;

namespace Project_DB
{
    public partial class Provider_Functions : Form
    {
        public string UserEmail { get; set; }

        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");

        public Provider_Functions()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Acc_Delection form_accdel = new Acc_Delection();
            form_accdel.ShowDialog();

            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Information_Change form_information_change = new Information_Change();
            form_information_change.UserEmail = UserEmail;
            form_information_change.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Acc_Addition form_accadd = new Acc_Addition();
            form_accadd.ShowDialog();

            this.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Active_Accomodations_Form form_view_active_acc = new Active_Accomodations_Form();
            form_view_active_acc.UserEmail = UserEmail;
            form_view_active_acc.ShowDialog();

            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Provider_LogIn form_review_choice = new Provider_LogIn();
            form_review_choice.ShowDialog();

        }

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
                    string hotelIdQuery = "SELECT HotelID FROM Hotel WHERE ProviderID = @UserID";
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
                        ReviewsRecProv form_review_choice = new ReviewsRecProv();
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

    }
}
