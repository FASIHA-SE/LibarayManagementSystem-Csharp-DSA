using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using System.Xml.Linq;

namespace project
{
    public partial class ViewStudent : Form
    {
        private Student_IUD studentList = new Student_IUD();

        public ViewStudent()
        {
            InitializeComponent();
        }

        private void StudentView()
        {
            panel2.Visible = false;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-13O7JS7;Initial Catalog=project_library;Integrated Security=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "SELECT * FROM NewStudents ORDER BY stdid DESC"; // Fetch the records from the database
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet DS = new DataSet();
            DA.Fill(DS);

            // Convert DataTable to List<Student>
            List<Student> students = new List<Student>();
            foreach (DataRow row in DS.Tables[0].Rows)
            {
                Student student = new Student()
                {
                    StudentId = Convert.ToInt32(row["stdid"]),
                    Name = row["Sname"].ToString(),
                    Enrollment = row["enroll"].ToString(),
                    Department = row["dep"].ToString(),
                    Semester = row["sem"].ToString(),
                    Contact = row["Contact"].ToString(),
                    Email = row["email"].ToString()
                };
                students.Add(student);
            }

            // Apply insertion sort on the List<Student>
            InsertionSort(students);

            // Bind the sorted list to the DataGridView
            dataGridView1.DataSource = students;
        }

        // Insertion Sort implementation
        private void InsertionSort(List<Student> students)
        {
            for (int i = 1; i < students.Count; i++)
            {
                Student current = students[i];
                int j = i - 1;

                // Move elements of students[0..i-1], that are smaller than current, to one position ahead
                while (j >= 0 && students[j].StudentId < current.StudentId)  // Change the comparison to '<' for descending order
                {
                    students[j + 1] = students[j];
                    j = j - 1;
                }

                students[j + 1] = current;
            }
        }




        private void ViewStudent_Load(object sender, EventArgs e)
        {
            InitializeStudentList();
            StudentView();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        //Encapsulation
        private void Delete()
        {
            if (MessageBox.Show("Data will be Deleted. Confirm?", "Confirmation Dialog", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                // Delete from the linked list
                bool deletedFromList = studentList.DeleteStudent((int)rowid);

                if (deletedFromList)
                {
                    // Also delete from the database
                    SqlConnection con = new SqlConnection("Data Source=DESKTOP-13O7JS7;Initial Catalog=project_library;Integrated Security=True;");
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText = "DELETE FROM NewStudents WHERE stdid=" + rowid;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Student record deleted successfully.");
                }
                else
                {
                    MessageBox.Show("Student not found in the linked list.");
                }
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }


        //Encapsulation

        private void Update()
        {
            if (MessageBox.Show("Data will be Updated. Confirm?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // Update in the linked list
                bool updatedInList = studentList.UpdateStudent((int)rowid, txtSname.Text, txtSEno.Text, txtSdept.Text, txtSem.Text, txtContact.Text, txtEmail.Text);

                if (updatedInList)
                {
                    // Also update in the database
                    string ConnectionString = "Data Source=DESKTOP-13O7JS7;Initial Catalog=project_library;Integrated Security=True;";
                    SqlConnection con = new SqlConnection(ConnectionString);
                    con.Open();

                    string Query = "UPDATE NewStudents SET Sname = '" + txtSname.Text + "', enroll = '" + txtSEno.Text + "', dep = '" + txtSdept.Text + "', sem = '" + txtSem.Text + "', Contact = '" + txtContact.Text + "', email = '" + txtEmail.Text + "' WHERE stdid = " + rowid;
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Student record updated successfully.");
                }
                else
                {
                    MessageBox.Show("Student not found in the linked list.");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void InitializeStudentList()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-13O7JS7;Initial Catalog=project_library;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM NewStudents ORDER BY stdid DESC";
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet DS = new DataSet();
            DA.Fill(DS);

            foreach (DataRow row in DS.Tables[0].Rows)
            {
                int studentId = Convert.ToInt32(row["stdid"]);
                string sname = row["Sname"].ToString();
                string enroll = row["enroll"].ToString();
                string dep = row["dep"].ToString();
                string sem = row["sem"].ToString();
                string contact = row["Contact"].ToString();
                string email = row["email"].ToString();

                studentList.AddStudent(studentId, sname, enroll, dep, sem, contact, email);
            }
        }
        private void txtPDate_ValueChanged(object sender, EventArgs e)
        {
        }
        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
        }
        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
        }
        private void txtPublication_TextChanged(object sender, EventArgs e)
        {
        }
        private void txtAuthor_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtBName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        //Encapsulation
        private void Search()
        {
            // Establish a connection to the database
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-13O7JS7;Initial Catalog=project_library;Integrated Security=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            // Fetch all records initially, for linear search
            cmd.CommandText = "SELECT * FROM NewStudents ORDER BY stdid DESC";
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet DS = new DataSet();
            DA.Fill(DS);
            DataTable allStudents = DS.Tables[0];

            // Check if the search text is not empty
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                // Prepare a new DataTable to hold search results
                DataTable searchResults = allStudents.Clone(); // Clone the structure
                foreach (DataRow row in allStudents.Rows)
                {
                    string enroll = row["enroll"].ToString();
                    // Linear search for matching enrollment numbers
                    if (enroll.Contains(txtSearch.Text))
                    {
                        searchResults.ImportRow(row);
                    }
                }
                // Bind the search results to DataGridView
                dataGridView1.DataSource = searchResults;
            }
            else
            {
                // If the search is empty, show all records
                dataGridView1.DataSource = allStudents;
            }
        }



        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            Search();

        }


       

        int book_id;
        Int64 rowid;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                book_id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            panel2.Visible = true;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-13O7JS7;Initial Catalog=project_library;Integrated Security=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select* from NewStudents where stdid="+book_id+"";
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet DS = new DataSet();
            DA.Fill(DS);

            rowid = Int64.Parse(DS.Tables[0].Rows[0][0].ToString());

            txtSname.Text = DS.Tables[0].Rows[0][1].ToString();
            txtSEno.Text = DS.Tables[0].Rows[0][2].ToString();
            txtSdept.Text = DS.Tables[0].Rows[0][3].ToString();
            txtSem.Text = DS.Tables[0].Rows[0][4].ToString();
            txtContact.Text = DS.Tables[0].Rows[0][5].ToString();
            txtEmail.Text = DS.Tables[0].Rows[0][6].ToString();
        }

        //Encapsulation
        private void Refresh()
        {
            txtSearch.Clear();
            panel2.Visible = false;
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();

        }


    }
}
