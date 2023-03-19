using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware.Data.Tables
{
    public class FinancialReports
    {
        [Key]
        public int ReportId { get; set; }
        public string ReportType { get; set; }
        public DateTime ReportDate { get; set; }
        public string ReportContent { get; set; }
        public int UserId { get; set; }
    }
}
