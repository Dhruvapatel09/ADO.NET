using ADO.NET_Practical.Data.Contract;
using ADO.NET_Practical.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ADO.NET_Practical.Data.Implementation
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly IConfiguration _configuration;

        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("mydb")))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }

            foreach (DataRow row in dt.Rows)
            {
                Employee employee = new Employee
                {
                    EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                    EmployeeName = row["EmployeeName"].ToString(),
                    EmployeeDesignation = row["EmployeeDesignation"].ToString(),
                    EmployeeDOB = Convert.ToDateTime(row["EmployeeDOB"])
                };
                employees.Add(employee);
            }

            return employees;
        }
        public Employee GetEmployeeById(int employeeId)
        {
            Employee employee = null;
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("mydb")))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employee WHERE EmployeeId = @EmployeeId", con);
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    employee = new Employee
                    {
                        EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                        EmployeeName = row["EmployeeName"].ToString(),
                        EmployeeDesignation = row["EmployeeDesignation"].ToString(),
                        EmployeeDOB = Convert.ToDateTime(row["EmployeeDOB"])
                    };
                }
            }

            return employee;
        }
        public bool AddEmployee(Employee employee)
        {
            var result = false;
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("mydb")))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Employee (EmployeeName, EmployeeDesignation, EmployeeDOB) VALUES (@EmployeeName, @EmployeeDesignation, @EmployeeDOB)", con);
                cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@EmployeeDesignation", employee.EmployeeDesignation);
                cmd.Parameters.AddWithValue("@EmployeeDOB", employee.EmployeeDOB);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                result = true;
            }
            return result;
        }
        public bool UpdateEmployee(Employee employee)
        {
            var result = false;
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("mydb")))
            {
                // First, check if the employee exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Employee WHERE EmployeeId = @EmployeeId", con);
                checkCmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);

                con.Open();
                int employeeExists = (int)checkCmd.ExecuteScalar(); // Returns the count of employees with the given ID

                if (employeeExists > 0)
                {
                    // If the employee exists, proceed with the update
                    SqlCommand updateCmd = new SqlCommand("UPDATE Employee SET EmployeeName = @EmployeeName, EmployeeDesignation = @EmployeeDesignation, EmployeeDOB = @EmployeeDOB WHERE EmployeeId = @EmployeeId", con);
                    updateCmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    updateCmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    updateCmd.Parameters.AddWithValue("@EmployeeDesignation", employee.EmployeeDesignation);
                    updateCmd.Parameters.AddWithValue("@EmployeeDOB", employee.EmployeeDOB);

                    updateCmd.ExecuteNonQuery(); // Execute the update command
                    result = true; // Update was successful
                }
              

                con.Close();
            }

            return result;
        }


        public bool DeleteEmployee(int employeeId)
        {
            var result = false;
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("mydb")))
            {
                // First, check if the employee exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Employee WHERE EmployeeId = @EmployeeId", con);
                checkCmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                con.Open();
                int employeeExists = (int)checkCmd.ExecuteScalar(); // Check if employee exists

                if (employeeExists > 0)
                {
                    // Employee exists, proceed with deletion
                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM Employee WHERE EmployeeId = @EmployeeId", con);
                    deleteCmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    deleteCmd.ExecuteNonQuery();
                    result = true; // Deletion successful
                }
              

                con.Close();
            }

            return result;
        }

    }
}
