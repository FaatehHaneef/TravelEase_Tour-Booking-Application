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

namespace My_Reviews
{
    public partial class My_Reviews_Form : Form
    {
        public My_Reviews_Form()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");


        private void button1_Click(object sender, EventArgs e)
        {
            int userId;

            // Step 1: Parse UserID from revbox
            if (!int.TryParse(revbox.Text.Trim(), out userId))
            {
                MessageBox.Show("Please enter a valid numeric User ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

                try
                {
                    conn.Open();

                    // Step 2: Check if UserID exists in UserClass
                    string checkUserQuery = "SELECT COUNT(*) FROM UserClass WHERE UserID = @UserID";
                    SqlCommand checkCmd = new SqlCommand(checkUserQuery, conn);
                    checkCmd.Parameters.AddWithValue("@UserID", userId);

                    int count = (int)checkCmd.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("User ID not found in UserClass table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Step 3: Get all reviews for that UserID
                    string reviewQuery = "SELECT * FROM Review WHERE UserID = @UserID";
                    SqlDataAdapter adapter = new SqlDataAdapter(reviewQuery, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@UserID", userId);

                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    // Step 4: Display the data
                    dataGridView1.DataSource = table;
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }
           
        }
    }
}
