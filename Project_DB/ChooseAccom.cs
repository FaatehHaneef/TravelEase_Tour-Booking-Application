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
    public partial class ChooseAccom : Form
    {
        public string tripID { get; set; }

        public ChooseAccom()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");


        //private void TripsBtn_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        conn.Open();

        //        string query = "SELECT * FROM registrations";

        //        SqlDataAdapter da = new SqlDataAdapter(query, conn);

        //        DataTable dt = new DataTable();

        //        da.Fill(dt);

        //        dataGridView2.DataSource = dt;

        //        MessageBox.Show("Data Loaded Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string enteredID = IDBOX.Text.Trim();

            if (string.IsNullOrEmpty(enteredID))
            {
                MessageBox.Show("Hotel ID cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int hotelID;
            if (!int.TryParse(enteredID, out hotelID))
            {
                MessageBox.Show("Please enter a valid numeric Hotel ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Hotel WHERE HotelID = @HotelID AND City = @City";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@HotelID", hotelID);
                    cmd.Parameters.AddWithValue("@City", tripID); // tripID must be accessible here

                    int count = (int)cmd.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("Incorrect Hotel ID for the selected trip.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Valid entry, go to next form
                this.Hide();
                EnterBookingDetails form_booking_detail = new EnterBookingDetails();
                form_booking_detail.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                string query;

                if (tripID == "All locations")
                {
                    query = "SELECT * FROM Hotel";
                }
                else
                {
                    query = "SELECT * FROM Hotel WHERE City = @City";
                }

                SqlCommand cmd = new SqlCommand(query, conn);

                if (tripID != "All locations")
                {
                    cmd.Parameters.AddWithValue("@City", tripID);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No Accommodation available currently");
                }

                dataGridView2.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
