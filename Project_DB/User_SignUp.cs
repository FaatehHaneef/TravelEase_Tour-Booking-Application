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
    public partial class User_SignUp : Form
    {
        public User_SignUp()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            User_Log_In form_Log_In = new User_Log_In();
            form_Log_In.ShowDialog();

            this.Close();
        }
        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            string name = namebox.Text.Trim();
            string email = emailbox.Text.Trim();
            string password = passbox.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Field cannot be empty");
                return;
            }

            if (!email.Contains("@") || !email.EndsWith("gmail.com") || email.IndexOf("@") == 0)
            {
                MessageBox.Show("Incorrect email, try again.");
                return;
            }

            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open(); // ✅ Ensure connection is open

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    // Get next available UserID
                    cmd.CommandText = "SELECT ISNULL(MAX(UserID), 0) + 1 FROM UserClass";
                    int nextUserId = (int)cmd.ExecuteScalar();

                    // Insert user data
                    cmd.CommandText = @"INSERT INTO UserClass 
                                (UserID, Name, Email, Password, Role, RegistrationDate, Status) 
                                VALUES 
                                (@UserID, @Name, @Email, @Password, @Role, GETDATE(), @Status)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserID", nextUserId);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Role", "Traveler");
                    cmd.Parameters.AddWithValue("@Status", "Active");

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Registration successful!");

                this.Hide();
                User_Functions form_function = new User_Functions();
                form_function.UserEmail = email;
                form_function.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close(); // Always close after done
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void l_email_Click(object sender, EventArgs e)
        {

        }
    }
}
