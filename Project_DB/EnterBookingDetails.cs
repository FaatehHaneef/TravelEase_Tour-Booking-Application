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
using Project_DB;

namespace Forms_db_proj
{
    public partial class EnterBookingDetails : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");

        public EnterBookingDetails()
        {
            InitializeComponent();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            // Validate ComboBoxes
            if (mealbox.SelectedItem == null || transbox.SelectedItem == null || guidebox.SelectedItem == null)
            {
                MessageBox.Show("Meal, Transport, and Guide fields cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate Radio Buttons
            if (!creditbtn.Checked && !debitbtn.Checked && !cashbtn.Checked)
            {
                MessageBox.Show("Please select a payment method.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate NumericUpDown
            int peopleCount = (int)peoplebox.Value;
            if (peopleCount <= 0)
            {
                MessageBox.Show("Please enter the number of people greater than 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Read ComboBox values
            bool meal = mealbox.SelectedItem.ToString() == "Yes";
            bool transport = transbox.SelectedItem.ToString() == "Yes";
            bool guide = guidebox.SelectedItem.ToString() == "Yes";

            // Read selected payment method
            string paymentMethod = creditbtn.Checked ? "Credit Card" :
                                   debitbtn.Checked ? "Debit Card" : "Cash";

            string paymentStatus = (paymentMethod == "Cash") ? "Pending" : "Paid";

            try
            {
                conn.Open();

                // Initialize IDs
                int newBookingID = 1, newHotelBookingID = 1, newInclusionID = 1, newPaymentID = 1;
                int userID = 0, tripID = 0;

                // Fetch max IDs including UserID and TripID
                SqlCommand cmdGetMaxIDs = new SqlCommand(@"
            SELECT 
                ISNULL((SELECT MAX(BookingID) FROM Booking), 0) + 1,
                ISNULL((SELECT MAX(HotelBookingID) FROM HotelBooking), 0) + 1,
                ISNULL((SELECT MAX(InclusionID) FROM TripInclusions), 0) + 1,
                ISNULL((SELECT MAX(PaymentID) FROM Payment), 0) + 1,
                ISNULL((SELECT MAX(UserID) FROM UserClass), 0),
                ISNULL((SELECT MAX(TripID) FROM Trip), 0)", conn);

                // After fetching max IDs
               int  hotelID = 0;
                SqlCommand cmdHotelID = new SqlCommand("SELECT MAX(HotelID) FROM Hotel", conn);
                cmdHotelID.Parameters.AddWithValue("@TripID", tripID);
                object hotelResult = cmdHotelID.ExecuteScalar();

                if (hotelResult == null || hotelResult == DBNull.Value)
                {
                    MessageBox.Show("No hotel found for the selected trip.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Close();
                    return;
                }

                hotelID = Convert.ToInt32(hotelResult);


                SqlDataReader reader = cmdGetMaxIDs.ExecuteReader();
                if (reader.Read())
                {
                    newBookingID = reader.GetInt32(0);
                    newHotelBookingID = reader.GetInt32(1);
                    newInclusionID = reader.GetInt32(2);
                    newPaymentID = reader.GetInt32(3);
                    userID = reader.GetInt32(4);
                    tripID = reader.GetInt32(5);
                }
                reader.Close();

                // Get cost per person from Trip table for selected tripID
                SqlCommand cmdCost = new SqlCommand("SELECT PricePerPerson FROM Trip WHERE TripID = @TripID", conn);
                cmdCost.Parameters.AddWithValue("@TripID", tripID);
                object result = cmdCost.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                {
                    MessageBox.Show("Could not retrieve price per person for the selected trip. Please check TripID and ensure the price is set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Close();
                    return;
                }

                decimal costPerPerson = Convert.ToDecimal(result);
                decimal totalCost = costPerPerson * peopleCount;
                DateTime today = DateTime.Today;
                DateTime checkIn = today.AddDays(10);
                DateTime checkOut = today.AddDays(20);

                // Insert into Booking table
                SqlCommand cmdBooking = new SqlCommand(@"
            INSERT INTO Booking (BookingID, UserID, TripID, BookingDate, Status, NoOfPeople, TotalCost)
            VALUES (@BookingID, @UserID, @TripID, @BookingDate, @Status, @NoOfPeople, @TotalCost)", conn);

                cmdBooking.Parameters.AddWithValue("@BookingID", newBookingID);
                cmdBooking.Parameters.AddWithValue("@UserID", userID);
                cmdBooking.Parameters.AddWithValue("@TripID", tripID);
                cmdBooking.Parameters.AddWithValue("@BookingDate", today);
                cmdBooking.Parameters.AddWithValue("@Status", "Active");
                cmdBooking.Parameters.AddWithValue("@NoOfPeople", peopleCount);
                cmdBooking.Parameters.AddWithValue("@TotalCost", totalCost);
                cmdBooking.ExecuteNonQuery();

                // Insert into HotelBooking table
                SqlCommand cmdHotelBooking = new SqlCommand(@"
            INSERT INTO HotelBooking (HotelBookingID, BookingID, HotelID, CheckInDate, CheckOutDate, Status)
            VALUES (@HotelBookingID, @BookingID, @HotelID, @CheckInDate, @CheckOutDate, @Status)", conn);

                cmdHotelBooking.Parameters.AddWithValue("@HotelBookingID", newHotelBookingID);
                cmdHotelBooking.Parameters.AddWithValue("@BookingID", newBookingID);
                cmdHotelBooking.Parameters.AddWithValue("@HotelID", hotelID);
                cmdHotelBooking.Parameters.AddWithValue("@CheckInDate", checkIn);
                cmdHotelBooking.Parameters.AddWithValue("@CheckOutDate", checkOut);
                cmdHotelBooking.Parameters.AddWithValue("@Status", "Active");
                cmdHotelBooking.ExecuteNonQuery();

                // Insert into TripInclusions table
                SqlCommand cmdInclusions = new SqlCommand(@"
            INSERT INTO TripInclusions (InclusionID, TripID, MealIncluded, TransportIncluded, GuideIncluded, AccommodationIncluded)
            VALUES (@InclusionID, @TripID, @Meal, @Transport, @Guide, @Accommodation)", conn);

                cmdInclusions.Parameters.AddWithValue("@InclusionID", newInclusionID);
                cmdInclusions.Parameters.AddWithValue("@TripID", tripID);
                cmdInclusions.Parameters.AddWithValue("@Meal", meal);
                cmdInclusions.Parameters.AddWithValue("@Transport", transport);
                cmdInclusions.Parameters.AddWithValue("@Guide", guide);
                cmdInclusions.Parameters.AddWithValue("@Accommodation", true); // assumed always true
                cmdInclusions.ExecuteNonQuery();

                // Insert into Payment table
                SqlCommand cmdPayment = new SqlCommand(@"
            INSERT INTO Payment (PaymentID, BookingID, PaymentDate, Amount, PaymentMethod, Status)
            VALUES (@PaymentID, @BookingID, @PaymentDate, @Amount, @PaymentMethod, @Status)", conn);

                cmdPayment.Parameters.AddWithValue("@PaymentID", newPaymentID);
                cmdPayment.Parameters.AddWithValue("@BookingID", newBookingID);
                cmdPayment.Parameters.AddWithValue("@PaymentDate", today);
                cmdPayment.Parameters.AddWithValue("@Amount", totalCost);
                cmdPayment.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                cmdPayment.Parameters.AddWithValue("@Status", paymentStatus);
                cmdPayment.ExecuteNonQuery();

                conn.Close();

                // Show confirmation form
                this.Hide();
                ConfTrip CB = new ConfTrip();
                CB.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            this.Show();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void EnterBookingDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
