using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using System.Xml.Linq;

namespace project
{
    public partial class ReturnBook : Form
    {
        private CustomStack<(int id, string bookName, string returnDate)> returnStack = new CustomStack<(int id, string bookName, string returnDate)>();

        public ReturnBook()
        {
            InitializeComponent();
        }

        private void BookREturn()
        {
            txtEnterEnrollment.Clear();
        }
        private void ReturnBook_Load(object sender, EventArgs e)
        {
            BookREturn();
        }


        private void SSearch()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-13O7JS7;Initial Catalog=project_library;Integrated Security=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            // Fetch all issued books for the student where the return date is NULL
            cmd.CommandText = "SELECT * FROM IRBook WHERE std_enroll = @enrollmentId AND book_return_date IS NULL";
            cmd.Parameters.AddWithValue("@enrollmentId", txtEnterEnrollment.Text.Trim());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            // Check if there are any results
            if (ds.Tables[0].Rows.Count != 0)
            {
                // Bind data to DataGridView
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                // Display a message if no books found for the given student
                MessageBox.Show("No pending returns for this student", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void btnSearchStudent_Click(object sender, EventArgs e)
        {
            SSearch();
        }


        private void Exit()
        {
            if (MessageBox.Show("Are you sure ", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();

            }
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        string bname;
        string bdate;
        Int64 rowid;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            panel2.Visible = true;

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                rowid = Int64.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                bname = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                bdate = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();

            }
            txtBookName.Text = bname;
            txtBookIssueDate.Text = bdate;
        }


        private void Return()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-13O7JS7;Initial Catalog=project_library;Integrated Security=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            con.Open();
            cmd.CommandText = "UPDATE IRBook SET book_return_date = @returnDate WHERE std_enroll = @enroll AND id = @rowId";
            cmd.Parameters.AddWithValue("@returnDate", dateTimePicker1.Text.Trim());
            cmd.Parameters.AddWithValue("@enroll", txtEnterEnrollment.Text.Trim());
            cmd.Parameters.AddWithValue("@rowId", rowid);

            // Add the current book return details to the stack
            returnStack.Push((id: (int)rowid, bookName: bname, returnDate: dateTimePicker1.Text.Trim()));

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Return Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Refresh the DataGridView by removing the returned book from it
            RemoveReturnedBookFromGrid();

            ReturnBook_Load(this, null); // Reload the data grid
        }

        private void RemoveReturnedBookFromGrid()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["id"].Value != null && row.Cells["id"].Value.ToString() == rowid.ToString())
                {
                    dataGridView1.Rows.Remove(row); // Remove the row from the grid
                    break;
                }
            }
        }


        //undo
        private void UndoLastReturn()
        {
            if (returnStack.Count == 0)
            {
                MessageBox.Show("No return to undo.", "Undo Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var lastReturn = returnStack.Pop(); // Use your custom Pop() method

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-13O7JS7;Initial Catalog=project_library;Integrated Security=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            con.Open();
            cmd.CommandText = "UPDATE IRBook SET book_return_date = NULL WHERE id = @rowId";
            cmd.Parameters.AddWithValue("@rowId", lastReturn.id);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show($"Undo Successful! Book '{lastReturn.bookName}' is marked as not returned.", "Undo Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Re-fetch and refresh the data grid
            AddBackToGrid(lastReturn.id);
        }



        private void AddBackToGrid(int bookId)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-13O7JS7;Initial Catalog=project_library;Integrated Security=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            // Fetch the book details for the undone return
            cmd.CommandText = "SELECT * FROM IRBook WHERE id = @rowId";
            cmd.Parameters.AddWithValue("@rowId", bookId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            // After fetching the data, rebind it to the DataGridView
            dataGridView1.DataSource = ds.Tables[0]; // Rebind the dataset
        }





        private void btnReturn_Click(object sender, EventArgs e)
        {
            Return();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            UndoLastReturn();
        }
    }

}
