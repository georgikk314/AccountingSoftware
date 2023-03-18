using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware.Data.Tables
{
    public class AccountsPayable
    {
        public int AccountPayableId { get; set; }
        public int VendorId { get; set; }
        public int TransactionId { get; set; }
        public double AmountDue { get; set; }
        public DateTime DueDate { get; set; }
        public int UserId { get; set; }
    }
}
