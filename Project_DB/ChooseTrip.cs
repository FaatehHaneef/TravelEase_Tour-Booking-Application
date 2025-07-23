using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Project_DB;

namespace Forms_db_proj
{
    public partial class ChooseTrip : Form
    {
        public ChooseTrip()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            User_Functions form_userfun = new User_Functions();
            form_userfun.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");
        string eyeD;
        //private void TripsBtn_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        conn.Open();

        //        string query = "SELECT * FROM registrations";

        //        SqlDataAdapter da = new SqlDataAdapter(query, conn);

        //        DataTable dt = new DataTable();

        //        da.Fill(dt);

        //        dataGridView1.DataSource = dt;

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

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            User_Functions form_my_booking = new User_Functions();
            form_my_booking.ShowDialog();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            string enteredTripID = bookbox.Text; // Get the value entered in bookbox

            // Ensure that the input is not empty and is a valid number
            if (string.IsNullOrWhiteSpace(enteredTripID) || !int.TryParse(enteredTripID, out int tripID))
            {
                MessageBox.Show("Please enter a valid TripID.");
                return;
            }

            try
            {
                conn.Open(); // Open the connection

                string query = "SELECT 1 FROM Trip WHERE TripID = @TripID"; // Query to check if the entered TripID exists in Trip table

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add the entered TripID as parameter
                    cmd.Parameters.AddWithValue("@TripID", tripID);

                    // Execute the query and check if a matching TripID exists
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        this.Hide();
                        ChooseAccom form_chooseacc = new ChooseAccom();
                        form_chooseacc.tripID = eyeD;
                        form_chooseacc.ShowDialog();
                    }
                    else
                    {
                        // If the TripID does not exist, show an error message
                        MessageBox.Show("Invalid choice", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close(); // Close the connection after the operation
            }
        }



        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Validate at least one filter is selected
            if (string.IsNullOrEmpty(destbox.Text) || string.IsNullOrEmpty(typebox.Text))
            {
                MessageBox.Show("Please select filters.");
                return;
            }
            try
            {
                conn.Open();
                string query = "SELECT * FROM Trip WHERE StartDate >= @StartDate AND PricePerPerson BETWEEN @MinPrice AND @MaxPrice AND Capacity >= @GroupSize";

                // Add conditions only if not "All"
                if (destbox.Text != "All locations")
                {
                    query += " AND Destination = @Destination";
                }
                if (typebox.Text != "All Categories")
                {
                    query += " AND TripType = @TripType";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StartDate", datebox.Value.Date);
                    cmd.Parameters.AddWithValue("@MinPrice", minbox.Value);
                    cmd.Parameters.AddWithValue("@MaxPrice", maxbox.Value);
                    cmd.Parameters.AddWithValue("@GroupSize", groupbox.Value);

                    if (destbox.Text != "All locations")
                    {
                        cmd.Parameters.AddWithValue("@Destination", destbox.Text);
                    }
                    if (typebox.Text != "All Categories")
                    {
                        cmd.Parameters.AddWithValue("@TripType", typebox.Text);
                    }

                    eyeD = destbox.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Check if any rows are returned
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No results found.");
                    }

                    dataGridView1.DataSource = dt;
                }
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
