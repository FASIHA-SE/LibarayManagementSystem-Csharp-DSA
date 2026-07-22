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


namespace project
{
    public partial class CompleteBDetail : Form
    {
        private Queue<int> bookQueue; // Queue to hold book IDs for processing
        public CompleteBDetail()
        {
            InitializeComponent();
            bookQueue = new Queue<int>(); // Initialize the queue
        }

        private void DetailsBook()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-13O7JS7;Initial Catalog=project_library;Integrated Security=True;";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            // Query for books not returned
            cmd.CommandText = "select * from IRBook where book_return_date is null";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            // Clear the queue before adding new items to avoid adding already processed books
            bookQueue.Clear();

            // Add books not returned to queue
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int bookId = Convert.ToInt32(row["id"]);
                bookQueue.Enqueue(bookId); // Enqueue book ID
            }

            // Query for books that have been returned
            cmd.CommandText = "select * from IRBook where book_return_date is not null";
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            dataGridView2.DataSource = ds1.Tables[0];

            // Update the queue count label
            UpdateQueueLabel();
        }


        private void ProcessQueue()
        {
            if (bookQueue.Count > 0)
            {
                int bookId = bookQueue.Dequeue(); // Process the next book in the queue

                // Perform operations for the book, e.g., log details or update the database
                MessageBox.Show($"Processing Book ID: {bookId}", "Queue Processing");

                // Update the book's return date in the database to mark it as returned
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=DESKTOP-13O7JS7;Initial Catalog=project_library;Integrated Security=True;";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE IRBook SET book_return_date = @ReturnDate WHERE id = @BookId";
                cmd.Parameters.AddWithValue("@ReturnDate", DateTime.Now.ToString("yyyy-MM-dd")); // Set return date as current date
                cmd.Parameters.AddWithValue("@BookId", bookId);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show($"Book ID {bookId} returned successfully.", "Book Return");

                    // Refresh the data grids to show the updated status
                    DetailsBook(); // This will reload the books' data
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating book status: " + ex.Message, "Error");
                }

                // Update the queue count label to reflect the new size of the queue
                UpdateQueueLabel();
            }
            else
            {
                MessageBox.Show("No books in the queue to process.", "Queue Empty");
            }
        }

        private void UpdateQueueLabel()
        {
            // Update the label with the current size of the queue
            lblQueueCount.Text = "Queue Count: " + bookQueue.Count.ToString();
        }




        private void CompleteBDetail_Load(object sender, EventArgs e)
        {
            DetailsBook();
            UpdateQueueLabel(); // Initialize the label with the current queue count on form load
        }


        //private void UpdateQueueLabel()
        //{
        //    lblQueueCount.Text = $"Queue Count: {bookQueue.Count}";
        //}

        private void btnProcessNext_Click(object sender, EventArgs e)
        {
            // Process the next book in the queue when a button is clicked
            ProcessQueue();

        }
    }
}
