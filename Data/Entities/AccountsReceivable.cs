using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware.Data.Tables
{
    public class AccountsReceivable
    {
        public int AccountReceivableId { get; set; }
        public int CustomerId { get; set; }
        public int TransactionId { get; set; }
        public double AmountDue { get; set; }
        public DateTime DueDate { get; set; }
        public int UserId { get; set; }
    }
}
