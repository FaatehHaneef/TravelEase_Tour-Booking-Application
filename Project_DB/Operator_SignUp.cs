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
    public partial class Operator_SignUp : Form
    {
        public Operator_SignUp()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Operator_LogIn form_LogIn = new Operator_LogIn();
            form_LogIn.ShowDialog();

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Get input from textboxes
                string email = textBox1.Text.Trim();
                string password = textBox2.Text;
                string name = textBox3.Text.Trim();

                // Validate input
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Please fill in all fields.", "Registration Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Generate random values for additional required fields
                Random random = new Random();
                // int userId = random.Next(100000, 999999); // Generate a random UserID
                string licenseInfo = "TO-" + random.Next(10000, 99999).ToString();
                string responseTime = random.Next(1, 24) + " hours";
                string description = "Professional tour operator offering unique travel experiences since " +
                                    random.Next(2000, 2023).ToString() + ".";

                    conn.Open();

                    // Start a transaction to ensure all inserts succeed or fail together
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        int userId;
                        using (SqlCommand getMaxIdCmd = new SqlCommand("SELECT ISNULL(MAX(UserID), 0) + 1 FROM UserClass", conn, transaction))
                        {
                            userId = (int)getMaxIdCmd.ExecuteScalar();
                        }
                        try
                        {
                            // 1. Insert into UserClass
                            string userQuery = @"INSERT INTO UserClass (UserID, Name, Email, Password, Role, RegistrationDate, Status) 
                                        VALUES (@UserID, @Name, @Email, @Password, @Role, @RegistrationDate, @Status)";

                            using (SqlCommand userCmd = new SqlCommand(userQuery, conn, transaction))
                            {
                                userCmd.Parameters.AddWithValue("@UserID", userId);
                                userCmd.Parameters.AddWithValue("@Name", name);
                                userCmd.Parameters.AddWithValue("@Email", email);
                                userCmd.Parameters.AddWithValue("@Password", password);
                                userCmd.Parameters.AddWithValue("@Role", "TourOperator");
                                userCmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Now.Date);
                                userCmd.Parameters.AddWithValue("@Status", "Active");

                                userCmd.ExecuteNonQuery();
                            }

                            // 2. Insert into ServiceProvider
                            string providerQuery = @"INSERT INTO ServiceProvider (ProviderID, Type, Name, ContactInfo, Description) 
                                          VALUES (@ProviderID, @Type, @Name, @ContactInfo, @Description)";

                            using (SqlCommand providerCmd = new SqlCommand(providerQuery, conn, transaction))
                            {
                                providerCmd.Parameters.AddWithValue("@ProviderID", userId);  // Using same ID as UserID
                                providerCmd.Parameters.AddWithValue("@Type", "TourOperator");
                                providerCmd.Parameters.AddWithValue("@Name", name);
                                providerCmd.Parameters.AddWithValue("@ContactInfo", email);
                                providerCmd.Parameters.AddWithValue("@Description", description);

                                providerCmd.ExecuteNonQuery();
                            }

                            // 3. Insert into TourOperatorProfile
                            string profileQuery = @"INSERT INTO TourOperatorProfile (UserID, CompanyName, LicenseInfo, Description, ResponseTime) 
                                         VALUES (@UserID, @CompanyName, @LicenseInfo, @Description, @ResponseTime)";

                            using (SqlCommand profileCmd = new SqlCommand(profileQuery, conn, transaction))
                            {
                                profileCmd.Parameters.AddWithValue("@UserID", userId);
                                profileCmd.Parameters.AddWithValue("@CompanyName", name); // Using name as company name
                                profileCmd.Parameters.AddWithValue("@LicenseInfo", licenseInfo);
                                profileCmd.Parameters.AddWithValue("@Description", description);
                                profileCmd.Parameters.AddWithValue("@ResponseTime", responseTime);

                                profileCmd.ExecuteNonQuery();
                            }

                            // Commit the transaction if all inserts succeed
                            transaction.Commit();

                            MessageBox.Show("Registration successful!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            SetCurrentUserCredentials(email, password);


                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                        // Navigate to operator functions form
                        this.Hide();
                            Operator_Functions form_function = new Operator_Functions();
                        form_function.UserEmail = email;
                        form_function.ShowDialog();
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            // Roll back the transaction if any insert fails
                            transaction.Rollback();
                            MessageBox.Show("Registration failed: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Method to set current user credentials
        private void SetCurrentUserCredentials(string email, string password)
        {
            // Option 1: Using static class
            Program.CurrentUserEmail = email;
            Program.CurrentUserPassword = password;

            // Option 2: Using singleton (more robust)
            UserSession.GetInstance().Email = email;
            UserSession.GetInstance().Password = password;
        }

        // Static Program class (Option 1)
        public static class Program
        {
            public static string CurrentUserEmail { get; set; }
            public static string CurrentUserPassword { get; set; }
        }

        // Singleton UserSession class (Option 2 - Recommended)
        public sealed class UserSession
        {
            private static UserSession _instance;
            private static readonly object _lock = new object();

            public string Email { get; set; }
            public string Password { get; set; }
            public int UserID { get; set; }
            public string Name { get; set; }
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
                Email = null;
                Password = null;
                UserID = 0;
                Name = null;
                Role = null;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
