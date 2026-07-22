using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

// Book.cs
namespace project
{
    public class Book
    {
        public int BookID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publication { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string PurchaseDate { get; set; }

        public Book(string name, string author, string publication, string price, string quantity, string purchaseDate)
        {
            Name = name;
            Author = author;
            Publication = publication;
            Price = price;
            Quantity = quantity;
            PurchaseDate = purchaseDate;
        }

        public Book(int bookId, string name, string author, string publish, string date, string price, string quantity)
        {
            BookID = bookId;
            Name = name;
            Author = author;
            Publication = publish;
            Price = price;
            Quantity = quantity;
            PurchaseDate = date;

        }

    }
}
