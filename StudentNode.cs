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
    public class StudentNode
    {
        public string Name { get; set; }
        public string Enrollment { get; set; }
        public string Department { get; set; }
        public string Semester { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public StudentNode Next { get; set; }

        public StudentNode(string name, string enrollment, string department, string semester, string contact, string email)
        {
            Name = name;
            Enrollment = enrollment;
            Department = department;
            Semester = semester;
            Contact = contact;
            Email = email;
            Next = null;
        }
    }

}
