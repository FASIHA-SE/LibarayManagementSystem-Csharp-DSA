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
    public class CustomStack<T>
    {
        private List<T> elements = new List<T>();

        public void Push(T item)
        {
            elements.Add(item);
        }

        public T Pop()
        {
            if (elements.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }

            T topElement = elements[elements.Count - 1];
            elements.RemoveAt(elements.Count - 1);
            return topElement;
        }

        public int Count
        {
            get { return elements.Count; }
        }
    }
}
