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

namespace project
{
    public partial class ViewBook : Form
    {
        string BookName;
        string BookAuthor;
        string BookPublication;
        string BookPrice;
        string BookQuantity;

        private LinkedList_IUD bookList;

        //encapsulation
        public ViewBook()
        {
            InitializeComponent();
            bookList = new LinkedList_IUD();  // Initialize the linked list
            panel2.Visible = false;
            LoadBooks();
            
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void ViewBook_Load(object sender, EventArgs e)
        {

            LoadBooks();  // Load books into the linked list and update the grid
            ViewBook V = new ViewBook();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        int book_id;
        Int64 rowid;


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                book_id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtBName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();  // Set the book name in txtBName
            }
            panel2.Visible = true;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-13O7JS7;Initial Catalog=project_library;Integrated Security=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select* from NewBook where book_id="+ book_id+"";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            rowid = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());


            txtBName.Text = ds.Tables[0].Rows[0][1].ToString();
            txtAuthor.Text = ds.Tables[0].Rows[0][2].ToString();
            txtPublication.Text = ds.Tables[0].Rows[0][3].ToString();
            txtPrice.Text = ds.Tables[0].Rows[0][4].ToString();
            txtQuantity.Text = ds.Tables[0].Rows[0][5].ToString();
        }

        //Encapsulation
        //searching
        private void TBookName()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-13O7JS7;Initial Catalog=project_library;Integrated Security=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            // Fetch all books to filter manually
            cmd.CommandText = "SELECT * FROM NewBook ORDER BY book_id DESC";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable allBooks = ds.Tables[0];

            // Linear search logic
            if (!string.IsNullOrEmpty(txtBookName.Text))
            {
                DataTable filteredBooks = allBooks.Clone(); // Create a structure to hold filtered rows
                foreach (DataRow row in allBooks.Rows)
                {
                    string bookName = row["bName"].ToString();
                    if (bookName.IndexOf(txtBookName.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        filteredBooks.ImportRow(row);
                    }
                }

                // Bind filtered results to the DataGridView
                dataGridView1.DataSource = filteredBooks;
            }
            else
            {
                // Bind all books if no search term
                dataGridView1.DataSource = allBooks;
            }
        }



        private void LoadBooks()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-13O7JS7;Initial Catalog=project_library;Integrated Security=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            // Fetch all books without ordering
            cmd.CommandText = "SELECT * FROM NewBook";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            // Clear the current list before loading new data
            bookList.Clear(); // Clear the list before inserting new data

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Book book = new Book(
                    Convert.ToInt32(row["book_id"]),  // book_id is now included in the Book object
                    row["bName"].ToString(),
                    row["bAuthor"].ToString(),
                    row["bPublish"].ToString(),
                    row["bDate"].ToString(),
                    row["bPrice"].ToString(),
                    row["bQuantity"].ToString()
                );
                bookList.Insert(book);  // Insert book into the linked list
            }

            // Sort books using a sorting algorithm
            SortBooksByBookId();  // This is where the sorting happens

            // Bind the books to the DataGridView after loading and sorting
            dataGridView1.DataSource = bookList.GetBooks();  // Ensure GetBooks() returns a valid list or collection
        }

        // Sorting algorithm (e.g., Insertion Sort) to sort the linked list based on book_id
        private void SortBooksByBookId()
        {
            // Convert the linked list to a list for easier sorting (or sort directly on the linked list)
            List<Book> books = bookList.GetBooks().ToList();

            // Implement a sorting algorithm (e.g., Insertion Sort)
            for (int i = 1; i < books.Count; i++)
            {
                Book current = books[i];
                int j = i - 1;

                // Move elements of books[0..i-1] that are greater than current to one position ahead
                while (j >= 0 && books[j].BookID < current.BookID)
                {
                    books[j + 1] = books[j];
                    j = j - 1;
                }
                books[j + 1] = current;
            }

            // After sorting, update the linked list
            bookList.Clear();  // Clear the list before inserting the sorted books
            foreach (Book book in books)
            {
                bookList.Insert(book);  // Insert sorted books back into the linked list
            }
        }




        private void txtBookName_TextChanged(object sender, EventArgs e)
        {
            TBookName();
        }

        //encapsulation
        private void Refresh()
        {
            txtBookName.Clear();
            panel2.Visible = false;
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }


        private DialogResult ShowConfirmationDialog(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(message, caption, buttons, icon);
        }


        //encapsulation
        private void Update()
        {
            // Create the updated Book object
            Book updatedBook = new Book(
                book_id, // Use the book_id for identification
                txtBName.Text,
                txtAuthor.Text,
                txtPublication.Text,
                txtPrice.Text,
                txtQuantity.Text,
                txtPDate.Text
            );

            // Update the book in the linked list
            if (bookList.Update(book_id, updatedBook))
            {
                // Now update the book in the database
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-13O7JS7;Initial Catalog=project_library;Integrated Security=True;";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "UPDATE NewBook SET bName = @bName, bAuthor = @bAuthor, bPublish = @bPublish, bPrice = @bPrice, bQuantity = @bQuantity, bDate = @bDate WHERE book_id = @book_id";

                cmd.Parameters.AddWithValue("@bName", txtBName.Text);
                cmd.Parameters.AddWithValue("@bAuthor", txtAuthor.Text);
                cmd.Parameters.AddWithValue("@bPublish", txtPublication.Text);
                cmd.Parameters.AddWithValue("@bPrice", txtPrice.Text);
                cmd.Parameters.AddWithValue("@bQuantity", txtQuantity.Text);
                cmd.Parameters.AddWithValue("@bDate", txtPDate.Text);
                cmd.Parameters.AddWithValue("@book_id", book_id);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Book updated successfully in the database!");
                    LoadBooks(); // Reload books after update
                    dataGridView1.DataSource = bookList.GetBooks(); // Update the grid with new data
                }
                else
                {
                    MessageBox.Show("Failed to update the book in the database.");
                }
            }
            else
            {
                MessageBox.Show("Book not found.");
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ShowConfirmationDialog("Data will be updated. Confirm?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Update();
            }
        }

        //encapsulation

        private void Delete()
        {
            // Delete the book from the linked list
            if (bookList.Delete(book_id))  // Use book_id for deletion
            {
                // Now delete the book from the database
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-13O7JS7;Initial Catalog=project_library;Integrated Security=True;";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "DELETE FROM NewBook WHERE book_id = @book_id";
                cmd.Parameters.AddWithValue("@book_id", book_id);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Book deleted successfully from the database!");
                    LoadBooks(); // Reload books after deletion
                    dataGridView1.DataSource = bookList.GetBooks(); // Update the grid with new data
                }
                else
                {
                    MessageBox.Show("Failed to delete the book from the database.");
                }
            }
            else
            {
                MessageBox.Show("Book not found in the linked list.");
            }
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be Delete.confirm?", "Confirmation Dialog", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {

                Delete();
            }
        }

        private void Cancel()
        {
            panel2.Visible = false;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
    }
}
