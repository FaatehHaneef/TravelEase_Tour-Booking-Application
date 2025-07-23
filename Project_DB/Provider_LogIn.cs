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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Project_DB
{
    public partial class Provider_LogIn : Form
    {
        public Provider_LogIn()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Provider_SignUp form_SignUp = new Provider_SignUp();
            form_SignUp.ShowDialog();

            this.Close();
        }

        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            string email = emailbox.Text.Trim();
            string password = passbox.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.");
                return;
            }

            try
            {
                conn.Open();

                // Step 1: Check if email exists
                string checkEmailQuery = "SELECT UserID, Password FROM UserClass WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(checkEmailQuery, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string dbPassword = reader["Password"].ToString();

                    if (password == dbPassword)
                    {
                        reader.Close();
                        conn.Close();

                        this.Hide();
                        Provider_Functions form_function = new Provider_Functions();
                        form_function.UserEmail = email;
                        form_function.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        reader.Close();
                        MessageBox.Show("Incorrect password.");
                    }
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("Incorrect email.");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            User_Type form_function = new User_Type();
            form_function.ShowDialog();
        }
    }
}
