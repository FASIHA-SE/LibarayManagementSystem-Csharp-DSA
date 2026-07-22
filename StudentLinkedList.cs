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
{

    public class StudentLinkedList
    {
        private StudentNode head;

        public void InsertStudent(string name, string enrollment, string department, string semester, string contact, string email)
        {
            StudentNode newNode = new StudentNode(name, enrollment, department, semester, contact, email);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                StudentNode temp = head;
                while (temp.Next != null)
                {
                    temp = temp.Next;
                }
                temp.Next = newNode;
            }
        }



        public List<StudentNode> GetStudents()
        {
            List<StudentNode> students = new List<StudentNode>();
            StudentNode temp = head;
            while (temp != null)
            {
                students.Add(temp);
                temp = temp.Next;
            }
            return students;
        }
    }
}
