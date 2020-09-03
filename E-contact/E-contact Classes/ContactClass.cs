using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_contact.E_contact_Classes
{
    class ContactClass
    {
        // Getter and Setter Properties
        // Acts as Data Carrier in Application
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        // Selecting Data from Database
        public DataTable Select()
        {
            // Step 1: Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                // Step 2: Write SQL Query
                string sql = "SELECT * FROM tbl_contact";

                // Creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Creating SQL DataAdapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();
                adapter.Fill(dt);
            }

            catch(Exception ex)
            {

            }

            finally
            {
                conn.Close();
            }

            return dt;
        }

        // Inserting Data into Database
        public bool Insert (ContactClass c)
        {
            // Creating a Default Return Type and Setting its Value to False
            bool isSuccess = false;

            // Step 1: Connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                // Step 2: Create an SQL Query to Insert Data
                string sql = "INSERT INTO tbl_contact (FirstName, LastName, ContactNo, Address, Gender) VALUES (@FirstName, @LastName, @ContactNo, @Address, @Gender)";

                // Creating SQL Command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Create Parameters to Add Data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                // Connection Open Here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                // If the Query runs Successfully then the Value of Rows will be Greater than Zero else its Value will be 0
                if(rows > 0)
                {
                    isSuccess = true;
                }

                else
                {
                    isSuccess = false;
                }
            }

            catch(Exception ex)
            {

            }

            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        // Method to Update Data in Database from Application
        public bool Update(ContactClass c)
        {
            // Create a Default Return Type and Set its Default Value to False
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                // SQL to Update Data in Database
                string sql = "UPDATE tbl_contact SET FirstName = @FirstName, LastName = @LastName, ContactNo = @ContactNo, Address = @Address, Gender = @Gender WHERE ContactID = @ContactID";

                // Creating SQL Command
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Create Parameters to Add Value
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                // Open Database Connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                // If the Query runs Successfully then the Value of Rows will be Greater than Zero else its Value will be 0
                if(rows > 0)
                {
                    isSuccess = true;
                }

                else
                {
                    isSuccess = false;
                }
            }

            catch (Exception ex)
            {

            }

            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        // Method to Delete Data from Database
        public bool Delete(ContactClass c)
        {
            // Create a Defualt Return Type and Set its Value to False
            bool isSuccess = false;

            // Create SQL Connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                // SQL to Delete Data
                string sql = "DELETE FROM tbl_contact WHERE ContactID = @ContactID";

                // Creating SQL Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                //Open Connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                // If the Query runs Successfully then Value of Rows will be Greater than Zero else its Value will be 0
                if(rows > 0)
                {
                    isSuccess = true;
                }

                else
                {
                    isSuccess = false;
                }
            }

            catch (Exception ex)
            {

            }

            finally
            {
                // Close Connection
                conn.Close();
            }

            return isSuccess;
        }
    }
}
