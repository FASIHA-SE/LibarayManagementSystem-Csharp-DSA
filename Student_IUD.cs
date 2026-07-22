using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    public class StudentNode
    {
        public int StudentId;
        public string Sname;
        public string Enroll;
        public string Dep;
        public string Sem;
        public string Contact;
        public string Email;
        public StudentNode Next;

        public StudentNode(int studentId, string sname, string enroll, string dep, string sem, string contact, string email)
        {
            StudentId = studentId;
            Sname = sname;
            Enroll = enroll;
            Dep = dep;
            Sem = sem;
            Contact = contact;
            Email = email;
            Next = null;
        }
    }

    public class Student_IUD
    {
        public StudentNode Head;

        public Student_IUD()
        {
            Head = null;
        }

        // Add a new student to the list
        public void AddStudent(int studentId, string sname, string enroll, string dep, string sem, string contact, string email)
        {
            StudentNode newStudent = new StudentNode(studentId, sname, enroll, dep, sem, contact, email);
            if (Head == null)
            {
                Head = newStudent;
            }
            else
            {
                StudentNode current = Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newStudent;
            }
        }

        // Update student details in the list
        public bool UpdateStudent(int studentId, string sname, string enroll, string dep, string sem, string contact, string email)
        {
            StudentNode current = Head;
            while (current != null)
            {
                if (current.StudentId == studentId)
                {
                    current.Sname = sname;
                    current.Enroll = enroll;
                    current.Dep = dep;
                    current.Sem = sem;
                    current.Contact = contact;
                    current.Email = email;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        // Delete student from the list
        public bool DeleteStudent(int studentId)
        {
            if (Head == null) return false;

            // If the head node itself is the one to be deleted
            if (Head.StudentId == studentId)
            {
                Head = Head.Next;
                return true;
            }

            StudentNode current = Head;
            while (current.Next != null)
            {
                if (current.Next.StudentId == studentId)
                {
                    current.Next = current.Next.Next;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }
    }


