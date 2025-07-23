using Delete_Entity;
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
    public partial class Admin_Log_In : Form
    {
        public Admin_Log_In()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            // Get email and password from textboxes
            string email = textBox1.Text.Trim();
            string password = textBox2.Text;

            // Validate input
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if admin exists in database
            if (ValidateAdmin(email, password))
            {
                // Login successful - proceed to admin functions
                this.Hide();
                Admin_Functions form = new Admin_Functions();
                form.ShowDialog();
                this.Close();
            }
            else
            {
                // Login failed
                MessageBox.Show("Invalid email or password", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Clear password field for security
                textBox2.Clear();
                textBox2.Focus();
            }
        }

        private bool ValidateAdmin(string email, string password)
        {
            try
            {
               
                    conn.Open();

                    // SQL query to check if admin exists with provided email and password
                    string query = "SELECT COUNT(1) FROM Admin WHERE Email=@Email AND Password=@Password";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        // Execute the query and get the count
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        // If count > 0, admin exists
                        return count > 0;
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (conn.State == ConnectionState.Open)
                    conn.Close();

                return false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            User_Type DeleteForm = new User_Type();
            DeleteForm.ShowDialog();
        }
    }
}
