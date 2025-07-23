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
    public partial class Operator_LogIn : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");

        public Operator_LogIn()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Operator_SignUp form_SignUp = new Operator_SignUp();
            form_SignUp.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();
            string password = textBox2.Text;

            // Validate input
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both Email and password.", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
              
                    conn.Open();

                    // Retrieve full user details instead of just checking count
                    string query = @"
                        SELECT UserID, Name, Email, Role, Status 
                        FROM UserClass 
                        WHERE Password = @Password AND Email = @Email";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Check user status
                                string status = reader.GetString(reader.GetOrdinal("Status"));
                                if (status.ToLower() != "active")
                                {
                                    MessageBox.Show("Your account is not active. Please contact support.", "Account Inactive",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                // Set user session
                                UserSession session = UserSession.GetInstance();
                                session.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                                session.Name = reader.GetString(reader.GetOrdinal("Name"));
                                session.Email = reader.GetString(reader.GetOrdinal("Email"));
                                session.Role = reader.GetString(reader.GetOrdinal("Role"));
                                session.Password = password; // Be cautious with storing passwords

                            if (conn.State == ConnectionState.Open)
                                conn.Close();

                            // Valid credentials - proceed to next form
                            this.Hide();
                                Operator_Functions form_function = new Operator_Functions();
                            form_function.UserEmail = email;
                            form_function.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                // Invalid credentials
                                MessageBox.Show("Invalid Email or password. Please try again.", "Login Failed",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                // Handle database or other errors
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    // Ensure this is in the same namespace or in a separate file
    public sealed class UserSession
    {
        private static UserSession _instance;
        private static readonly object _lock = new object();

        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        private UserSession() { }

        public static UserSession GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new UserSession();
                    }
                }
            }
            return _instance;
        }

        public void Clear()
        {
            UserID = 0;
            Name = null;
            Email = null;
            Password = null;
            Role = null;
        }
    }
}
