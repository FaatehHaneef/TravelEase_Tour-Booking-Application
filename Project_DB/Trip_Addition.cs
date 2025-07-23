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
    public partial class Trip_Addition : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");

        public Trip_Addition()
        {
            InitializeComponent();

            // Populate trip type combo box
            comboBox1.Items.AddRange(new string[]
            {
                "Adventure",
                "Cultural",
                "Leisure",
                "Eco-Tourism",
                "Educational",
                "Luxury",
                "Family",
                "Solo"
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Cancel button
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (!ValidateInputs())
                return;

            try
            {
                
                    conn.Open();

                    // First, get the max TripID and create new TripID
                    int newTripId;
                    string maxIdQuery = "SELECT ISNULL(MAX(TripID), 0) + 1 FROM Trip";
                    using (SqlCommand maxIdCmd = new SqlCommand(maxIdQuery, conn))
                    {
                        newTripId = Convert.ToInt32(maxIdCmd.ExecuteScalar());
                    }

                    // Get current user's ID by email from UserSession
                    UserSession session = UserSession.GetInstance();
                    int operatorId;

                    // Retrieve OperatorID using the email from UserSession
                    string getUserIdQuery = "SELECT UserID FROM UserClass WHERE Email = @Email";
                    using (SqlCommand getUserIdCmd = new SqlCommand(getUserIdQuery, conn))
                    {
                        getUserIdCmd.Parameters.AddWithValue("@Email", session.Email);

                        object result = getUserIdCmd.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("Could not find user associated with this email.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        operatorId = Convert.ToInt32(result);
                    }

                    // Calculate trip duration
                    int duration = (int)Math.Ceiling((dateTimePicker2.Value - dateTimePicker1.Value).TotalDays);

                    // Prepare insert query
                    string insertQuery = @"
                        INSERT INTO Trip (
                            TripID, OperatorID, Title, Description, Destination, 
                            StartDate, EndDate, TripType, Capacity, Duration, 
                            PricePerPerson, SustainabilityScore, AccessibilityOptions
                        ) VALUES (
                            @TripID, @OperatorID, @Title, @Description, @Destination, 
                            @StartDate, @EndDate, @TripType, @Capacity, @Duration, 
                            @PricePerPerson, @SustainabilityScore, @AccessibilityOptions
                        )";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        // Add parameters
                        cmd.Parameters.AddWithValue("@TripID", newTripId);
                        cmd.Parameters.AddWithValue("@OperatorID", operatorId);
                        cmd.Parameters.AddWithValue("@Title", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@Description", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@Destination", textBox3.Text.Trim());
                        cmd.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                        cmd.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value.Date);
                        cmd.Parameters.AddWithValue("@TripType", comboBox1.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Capacity", int.Parse(textBox4.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Duration", duration);
                        cmd.Parameters.AddWithValue("@PricePerPerson", decimal.Parse(textBox5.Text.Trim()));

                        // Optional: Add some default values for non-required fields
                        cmd.Parameters.AddWithValue("@SustainabilityScore", 5); // Default mid-range score
                        cmd.Parameters.AddWithValue("@AccessibilityOptions", "Standard");

                        // Execute the insert
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Trip added successfully!\nTrip ID: {newTripId}", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                        this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add trip.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding trip: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            // Validate Title
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter a trip title.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate Description
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a trip description.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate Destination
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Please enter a destination.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate Dates
            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                MessageBox.Show("End date must be after start date.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate Trip Type
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a trip type.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate Capacity
            if (!int.TryParse(textBox4.Text.Trim(), out int capacity) || capacity <= 0)
            {
                MessageBox.Show("Please enter a valid capacity (positive number).", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate Price
            if (!decimal.TryParse(textBox5.Text.Trim(), out decimal price) || price < 0)
            {
                MessageBox.Show("Please enter a valid price (non-negative number).", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
