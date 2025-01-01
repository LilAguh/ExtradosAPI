using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class BookLoans
    {
        public int ID { get; set; }
        public string BookName { get; set; } = string.Empty;
        public int UserID { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueTime { get; set; }
        public bool Returned { get; set; } = false;
    }
}
