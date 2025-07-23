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
using Forms_db_proj;

namespace Project_DB
{
    public partial class Acc_Addition : Form
    {
        public Acc_Addition()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");


        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            // Input Validation
            if (string.IsNullOrWhiteSpace(namebox.Text) ||
                string.IsNullOrWhiteSpace(addrbox.Text) ||
                string.IsNullOrWhiteSpace(citybox.Text) ||
                string.IsNullOrWhiteSpace(pricebox.Text) ||
                string.IsNullOrWhiteSpace(roombox.Text) ||
                string.IsNullOrWhiteSpace(ratingbox.Text))
            {
                MessageBox.Show("All fields must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reject_addition ra = new reject_addition();
                ra.ShowDialog();
                this.Show();
                return;
            }

            try
            {
                conn.Open();

                // Get next HotelID
                SqlCommand getHotelID = new SqlCommand("SELECT ISNULL(MAX(HotelID), 0) + 1 FROM Hotel", conn);
                int newHotelID = Convert.ToInt32(getHotelID.ExecuteScalar());

                // Get latest ProviderID where Role = 'HotelProvider'
                SqlCommand getProviderID = new SqlCommand("SELECT MAX(UserID) FROM UserClass WHERE Role = 'HotelProvider'", conn);
                object providerResult = getProviderID.ExecuteScalar();

                if (providerResult == null || providerResult == DBNull.Value)
                {
                    MessageBox.Show("No hotel provider found in the system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reject_addition ra = new reject_addition();
                    ra.ShowDialog();
                    this.Show();
                    return;
                }

                int providerID = Convert.ToInt32(providerResult);

                // Prepare Insert Command
                SqlCommand insertHotel = new SqlCommand(@"
            INSERT INTO Hotel (HotelID, ProviderID, HotelName, Address, City, PricePerNight, AvailableRooms, Rating)
            VALUES (@HotelID, @ProviderID, @HotelName, @Address, @City, @PricePerNight, @AvailableRooms, @Rating)", conn);

                insertHotel.Parameters.AddWithValue("@HotelID", newHotelID);
                insertHotel.Parameters.AddWithValue("@ProviderID", providerID);
                insertHotel.Parameters.AddWithValue("@HotelName", namebox.Text);
                insertHotel.Parameters.AddWithValue("@Address", addrbox.Text);
                insertHotel.Parameters.AddWithValue("@City", citybox.Text);
                insertHotel.Parameters.AddWithValue("@PricePerNight", Convert.ToDecimal(pricebox.Text));
                insertHotel.Parameters.AddWithValue("@AvailableRooms", Convert.ToInt32(roombox.Text));
                insertHotel.Parameters.AddWithValue("@Rating", Convert.ToDouble(ratingbox.Text));

                insertHotel.ExecuteNonQuery();

                conn.Close();

                // Show success confirmation
                ConfAccAdd form_success = new ConfAccAdd();
                form_success.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while inserting hotel: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reject_addition ra = new reject_addition();
                ra.ShowDialog();
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            this.Show();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
