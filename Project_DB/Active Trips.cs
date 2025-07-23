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

namespace WindowsFormsApp4
{
    public partial class Active_Trips_Form : Form
    {
        public string UserEmail { get; set; }

        public Active_Trips_Form()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True");

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            User_Functions DeleteForm = new User_Functions();
            DeleteForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Step 1: Get UserID from UserClass using UserEmail
                string getUserIDQuery = "SELECT UserID FROM UserClass WHERE Email = @UserEmail";
                SqlCommand getUserIDCmd = new SqlCommand(getUserIDQuery, conn);
                getUserIDCmd.Parameters.AddWithValue("@UserEmail", UserEmail); // assuming UserEmail is declared outside

                conn.Open();
                object result = getUserIDCmd.ExecuteScalar();
                conn.Close();

                if (result != null)
                {
                    int userID = Convert.ToInt32(result);

                    // Step 2: Get all hotels where ProviderID matches the extracted UserID
                    string getHotelsQuery = "SELECT * FROM Trip WHERE OperatorID = @OpID";
                    SqlCommand getHotelsCmd = new SqlCommand(getHotelsQuery, conn);
                    getHotelsCmd.Parameters.AddWithValue("@OpID", userID);

                    SqlDataAdapter adapter = new SqlDataAdapter(getHotelsCmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    Active_Trips_Grid.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Active_Trips_Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
