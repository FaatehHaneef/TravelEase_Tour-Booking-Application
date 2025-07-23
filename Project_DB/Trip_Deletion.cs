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

namespace Project_DB
{
    public partial class Trip_Deletion : Form
    {
        public Trip_Deletion()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");

        private void button2_Click(object sender, EventArgs e)
        {
            string tripTitle = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(tripTitle))
            {
                MessageBox.Show("Please enter a trip title.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                conn.Open();

                // DELETE dependent records in correct order

                string deleteReviewsQuery = "DELETE FROM Review WHERE TripID = (SELECT TripID FROM Trip WHERE Title = @title)";
                using (SqlCommand deleteReviewsCmd = new SqlCommand(deleteReviewsQuery, conn))
                {
                    deleteReviewsCmd.Parameters.AddWithValue("@title", tripTitle);
                    deleteReviewsCmd.ExecuteNonQuery();
                }

                string TripInclusionsQuery = "DELETE FROM TripInclusions WHERE TripID = (SELECT TripID FROM Trip WHERE Title = @title)";
                using (SqlCommand TripInclusionsCmd = new SqlCommand(TripInclusionsQuery, conn))
                {
                    TripInclusionsCmd.Parameters.AddWithValue("@title", tripTitle);
                    TripInclusionsCmd.ExecuteNonQuery();
                }

                string ItineraryQuery = "DELETE FROM Itinerary WHERE TripID = (SELECT TripID FROM Trip WHERE Title = @title)";
                using (SqlCommand ItineraryCmd = new SqlCommand(ItineraryQuery, conn))
                {
                    ItineraryCmd.Parameters.AddWithValue("@title", tripTitle);
                    ItineraryCmd.ExecuteNonQuery();
                }

                string TripCategoryMappingQuery = "DELETE FROM TripCategoryMapping WHERE TripID = (SELECT TripID FROM Trip WHERE Title = @title)";
                using (SqlCommand TripCategoryMappingCmd = new SqlCommand(TripCategoryMappingQuery, conn))
                {
                    TripCategoryMappingCmd.Parameters.AddWithValue("@title", tripTitle);
                    TripCategoryMappingCmd.ExecuteNonQuery();
                }

                string BookingQuery = "DELETE FROM Booking WHERE TripID = (SELECT TripID FROM Trip WHERE Title = @title)";
                using (SqlCommand BookingCmd = new SqlCommand(BookingQuery, conn))
                {
                    BookingCmd.Parameters.AddWithValue("@title", tripTitle);
                    BookingCmd.ExecuteNonQuery();
                }

                string AbandonedBookingQuery = "DELETE FROM AbandonedBooking WHERE TripID = (SELECT TripID FROM Trip WHERE Title = @title)";
                using (SqlCommand AbandonedBookingCmd = new SqlCommand(AbandonedBookingQuery, conn))
                {
                    AbandonedBookingCmd.Parameters.AddWithValue("@title", tripTitle);
                    AbandonedBookingCmd.ExecuteNonQuery();
                }

                string WishlistQuery = "DELETE FROM Wishlist WHERE TripID = (SELECT TripID FROM Trip WHERE Title = @title)";
                using (SqlCommand WishlistCmd = new SqlCommand(WishlistQuery, conn))
                {
                    WishlistCmd.Parameters.AddWithValue("@title", tripTitle);
                    WishlistCmd.ExecuteNonQuery();
                }

                // DELETE main Trip record
                string deleteTripQuery = "DELETE FROM Trip WHERE Title = @title";
                using (SqlCommand deleteTripCmd = new SqlCommand(deleteTripQuery, conn))
                {
                    deleteTripCmd.Parameters.AddWithValue("@title", tripTitle);
                    int rowsAffected = deleteTripCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Trip and all related data deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No matching trip found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while deleting trip:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
