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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace project
{// BookLinkedList.cs
    public class BookLinkedList
    {
        private class Node
        {
            public Book Book { get; set; }
            public Node Next { get; set; }

            public Node(Book book)
            {
                Book = book;
                Next = null;
            }
        }

        private Node head;

        public BookLinkedList()
        {
            head = null;
        }

        public void Insert(Book book)
        {
            Node newNode = new Node(book);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Node current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

        public void DisplayBooks()
        {
            Node current = head;
            while (current != null)
            {
                MessageBox.Show($"Book: {current.Book.Name}, Author: {current.Book.Author}", "Book Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                current = current.Next;
            }
        }


    }

}
