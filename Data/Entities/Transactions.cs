using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware.Data.Tables
{
    public class Transactions
    {
        [Key]
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }
        public int CustomerId { get; set; }
        public int VendorId { get; set; }
        public int UserId { get; set; }
    }
}
