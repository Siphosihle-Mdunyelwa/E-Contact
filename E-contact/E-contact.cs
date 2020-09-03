using E_contact.E_contact_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_contact
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ContactClass c = new ContactClass();

        private void Form1_Load(object sender, EventArgs e)
        { 
            // Load Data on Data Grid View
            DataTable dt = c.Select();
            dataGridViewContactList.DataSource = dt;
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            // Get the value from the input fields
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ContactNo = textBoxContactNumber.Text;
            c.Address = textBoxAddress.Text;
            c.Gender = comboBoxGender.Text;

            // Inserting Data into Database using Method Created
            bool success = c.Insert(c);
            if(success == true)
            {
                // Successfully inserted
                MessageBox.Show("New Contact Successfully Inserted");

                // Call the Clear Methods Here
                Clear();

                // Focus on First Name Textbox
                textBoxFirstName.Focus();
            }

            else
            {
                // Failed to Add Contact
                MessageBox.Show("Failed to Add New Contact. Try Again.");
            }

            // Load Data on Data Grid View
            DataTable dt = c.Select();
            dataGridViewContactList.DataSource = dt;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Method to Clear Fields
        public void Clear()
        {
            textBoxContactID.Text = "";
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxContactNumber.Text = "";
            textBoxAddress.Text = "";
            comboBoxGender.Text = "";
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            // Get Data From TextBoxes
            c.ContactID = int.Parse(textBoxContactID.Text);
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ContactNo = textBoxContactNumber.Text;
            c.Address = textBoxAddress.Text;
            c.Gender = comboBoxGender.Text;

            // Update Data in Database
            bool success = c.Update(c);
            if(success == true)
            {
                // Updated Successfully
                MessageBox.Show("Contact has been Successfully Updated.");

                // Load Data on Data Grid View
                DataTable dt = c.Select();
                dataGridViewContactList.DataSource = dt;

                // Call Clear Method
                Clear();

                // Focus on First Name Textbox
                textBoxFirstName.Focus();
            }

            else
            {
                // Failed to Update
                MessageBox.Show("Failed to Update Contact. Try Again.");
            }
        }

        private void DataGridViewContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Get Data From Data Grid View and Load it to the TextBoxes Respectively

            // Identify the Row on Which Mouse is Clicked ON
            int rowIndex = e.RowIndex;
            textBoxContactID.Text = dataGridViewContactList.Rows[rowIndex].Cells[0].Value.ToString();
            textBoxFirstName.Text = dataGridViewContactList.Rows[rowIndex].Cells[1].Value.ToString();
            textBoxLastName.Text = dataGridViewContactList.Rows[rowIndex].Cells[2].Value.ToString();
            textBoxContactNumber.Text = dataGridViewContactList.Rows[rowIndex].Cells[3].Value.ToString();
            textBoxAddress.Text = dataGridViewContactList.Rows[rowIndex].Cells[4].Value.ToString();
            comboBoxGender.Text = dataGridViewContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            // Call Clear Method Here
            Clear();

            // Focus on First Name Textbox
            textBoxFirstName.Focus();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            // Get ContactID From Application
            c.ContactID = Convert.ToInt32(textBoxContactID.Text);
            bool success = c.Delete(c);
            if(success == true)
            {
                // Successfully Deleted
                MessageBox.Show("Contact Successfully Deleted.");

                // Refresh Data Grid View

                // Load Data on Data Grid View
                DataTable dt = c.Select();
                dataGridViewContactList.DataSource = dt;

                // Call Clear Method Here
                Clear();
            }

            else
            {
                // Failed to Delete
                MessageBox.Show("Failed to Delete Contact. Try Again.");
            }
        }

        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            // Get Value From TextBox
            string keyword = textBoxSearch.Text;

            SqlConnection conn = new SqlConnection(myconnstr);

            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%'", conn);

            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridViewContactList.DataSource = dt;
        }
    }
}
