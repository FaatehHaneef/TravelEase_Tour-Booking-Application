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

namespace Delete_Entity
{
    public partial class Delete_Form : Form
    {
        public Delete_Form()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int userId;

            // Step 1: Validate input
            if (!int.TryParse(idbox.Text.Trim(), out userId))
            {
                MessageBox.Show("Please enter a valid numeric User ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                conn.Open();

                // Step 3: Check if UserID exists
                string checkQuery = "SELECT COUNT(*) FROM UserClass WHERE UserID = @UserID";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@UserID", userId);

                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    MessageBox.Show("User ID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Step 4: Delete the user
                string deleteQuery = "DELETE FROM UserClass WHERE UserID = @UserID";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@UserID", userId);

                deleteCmd.ExecuteNonQuery();

                MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    

    private void Delete_ID_Text_Box_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
