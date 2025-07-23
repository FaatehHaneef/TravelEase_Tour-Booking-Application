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
    public partial class Acc_Delection : Form
    {
        public Acc_Delection()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");

        private void button2_Click(object sender, EventArgs e)
        {
            // Check if the namebox is empty
            if (string.IsNullOrWhiteSpace(namebox.Text))
            {
                MessageBox.Show("Please enter the hotel name to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                conn.Open();

                SqlCommand deleteHotel = new SqlCommand("DELETE FROM Hotel WHERE HotelName = @HotelName", conn);
                deleteHotel.Parameters.AddWithValue("@HotelName", namebox.Text.Trim());

                int rowsAffected = deleteHotel.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Hotel deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No hotel found with the given name.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
