using project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BookNode
{
    public Book Data { get; set; }
    public BookNode Next { get; set; }

    public BookNode(Book data)
    {
        Data = data;
        Next = null;
    }
}

public class LinkedList_IUD
{
    private BookNode head;

    public LinkedList_IUD()
    {
        head = null;
    }

    // Insert a book at the end
    public void Insert(Book book)
    {
        BookNode newNode = new BookNode(book);

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            BookNode current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
    }

    // Update a book by book_id (instead of book name)
    public bool Update(int bookId, Book updatedBook)
    {
        BookNode current = head;

        while (current != null)
        {
            if (current.Data.BookID == bookId) // Match by book_id
            {
                current.Data.Name = updatedBook.Name;
                current.Data.Author = updatedBook.Author;
                current.Data.Publication = updatedBook.Publication;
                current.Data.Price = updatedBook.Price;
                current.Data.Quantity = updatedBook.Quantity;
                current.Data.PurchaseDate = updatedBook.PurchaseDate;
                return true;
            }
            current = current.Next;
        }
        return false; // Book not found by book_id
    }

    // Delete a book by book_id (instead of book name)
    public bool Delete(int bookId)
    {
        if (head == null) return false;

        // If the head node is the book to be deleted
        if (head.Data.BookID == bookId)
        {
            head = head.Next;
            return true;
        }

        BookNode current = head;
        while (current.Next != null)
        {
            if (current.Next.Data.BookID == bookId) // Match by book_id
            {
                current.Next = current.Next.Next;
                return true;
            }
            current = current.Next;
        }

        return false; // Book not found by book_id
    }


    // Display all books (For testing or debugging purposes)
    public void DisplayBooks()
    {
        BookNode current = head;
        while (current != null)
        {
            Console.WriteLine($"Book Name: {current.Data.Name}, Author: {current.Data.Author}");
            current = current.Next;
        }
    }
    public List<Book> GetBooks()
    {
        List<Book> books = new List<Book>();
        BookNode current = head; // Assuming you have a 'head' node representing the start of the linked list.

        while (current != null)
        {
            books.Add(current.Data); // Add each book to the list
            current = current.Next; // Move to the next node
        }

        return books;
    }
    public void Clear()
    {
        head = null;  // This will remove all nodes from the list by de-referencing the head
    }

}

