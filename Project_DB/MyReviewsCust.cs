using Project_DB;
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

namespace Forms_db_proj
{
    public partial class MyReviewsCust : Form
    {
        public string UE { get; set; }

        public MyReviewsCust()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            User_Functions formhehe = new User_Functions();
            formhehe.ShowDialog();
        }


        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string Email = UE;

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
                string reviewQuery = "SELECT * FROM Review WHERE UserID = @UserID";
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
                    MessageBox.Show("You have not given any reviews yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
