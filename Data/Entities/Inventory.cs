using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware.Data.Tables
{
    public class Inventory
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public double SellingPrice { get; set; }
        public int QuantityInStock { get; set; }
        public int UserId { get; set; }
    }
}
