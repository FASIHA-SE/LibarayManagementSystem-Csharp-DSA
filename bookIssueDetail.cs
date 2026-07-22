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
    public class BookIssueDetails
    {
        public string Enroll { get; set; }
        public string Name { get; set; }
        public string BookName { get; set; }
        public string IssueDate { get; set; }
    }

}
