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
    public partial class Password_Change : Form
    {
        public string UE { get; set; }

        public Password_Change()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");

        private void button2_Click(object sender, EventArgs e)
        {
            string oldPassword = oldbox.Text.Trim();
            string newPassword = newbox.Text.Trim();
            string confirmPassword = confbox.Text.Trim();

            // 1. Check for empty fields
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conn.Open();

                // 2. Get current password from DB
                string checkQuery = "SELECT Password FROM UserClass WHERE Email = @Email";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@Email", UE);

                object result = checkCmd.ExecuteScalar();

                if (result == null)
                {
                    MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string currentPassword = result.ToString();

                // 3. Compare old password
                if (oldPassword != currentPassword)
                {
                    MessageBox.Show("Incorrect old password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 4. Check if old and new password are same
                if (oldPassword == newPassword)
                {
                    MessageBox.Show("New password cannot be the same as the old password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 5. Check if new password and confirm password match
                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("New password and confirmation do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 6. Update password
                string updateQuery = "UPDATE UserClass SET Password = @NewPassword WHERE Email = @Email";
                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@NewPassword", newPassword);
                updateCmd.Parameters.AddWithValue("@Email", UE);

                int rowsAffected = updateCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Password updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Optional: close the form after success
                }
                else
                {
                    MessageBox.Show("Password update failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
