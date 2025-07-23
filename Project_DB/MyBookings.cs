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
using Operator_Review;
using Project_DB;

namespace Forms_db_proj
{
    public partial class MyBookings : Form
    {
        public string UE { get; set; }

        public MyBookings()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");

        //private void ShowBtn_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                // Step 1: Get UserID from UserClass where Email = UE
                string userIdQuery = "SELECT UserID FROM UserClass WHERE Email = @Email";
                SqlCommand getUserIdCmd = new SqlCommand(userIdQuery, conn);
                getUserIdCmd.Parameters.AddWithValue("@Email", UE);

                object userIdResult = getUserIdCmd.ExecuteScalar();

                if (userIdResult == null)
                {
                    MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int userId = Convert.ToInt32(userIdResult);

                // Step 2: Get reviews from Review table
                string reviewQuery = "SELECT * FROM Booking WHERE UserID = @UserID";
                SqlCommand getReviewsCmd = new SqlCommand(reviewQuery, conn);
                getReviewsCmd.Parameters.AddWithValue("@UserID", userId);

                SqlDataAdapter adapter = new SqlDataAdapter(getReviewsCmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("You have not made any bookings yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null; // Optionally clear the grid
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            User_Functions form_review = new User_Functions(); // Corrected class name
            form_review.ShowDialog();
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();

            Review_Form form_review = new Review_Form();
            form_review.ShowDialog();

            this.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
