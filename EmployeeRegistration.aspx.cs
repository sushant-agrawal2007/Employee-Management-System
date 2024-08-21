using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace ITP11AM
{
    public partial class EmployeeRegistration : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            
            string name = txtName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string position = txtPosition.Text;
            string department = txtDepartment.Text;

            
            string connectionString = ConfigurationManager.ConnectionStrings["EmployeeDBConnectionString"].ConnectionString;

        
            string query = "INSERT INTO Employees (FullName, Email, Phone, Position, Department) VALUES (@FullName, @Email, @Phone, @Position, @Department)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FullName", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Position", position);
                    command.Parameters.AddWithValue("@Department", department);

                    try
                    {
                     
                        connection.Open();
                        command.ExecuteNonQuery();
                        
                        Response.Write("<script>alert('Employee registered successfully!');</script>");
                    }
                    catch (Exception ex)
                    {
                     
                        Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
                    }
                }
            }
        }
    }
}
