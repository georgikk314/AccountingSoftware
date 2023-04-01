using AccountingSoftware.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware.PdfManager
{
    public interface IPDFManager
    {
        string PDFWriter(string name, List<Inventory> content, int userId);
        string PDFWriter(string name, List<Customers> customers, List<Transactions> transactions, List<Users> users, int userId, string customerName, DateTime StartDate, DateTime EndDate);
        Task PDFDownloader(string filename);
    }
}
