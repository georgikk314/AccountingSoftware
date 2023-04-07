using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AccountingSoftware.Data.Tables;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.Maui.Storage;
using Microsoft.EntityFrameworkCore;

namespace AccountingSoftware.PdfManager
{
    public class PDFManager : IPDFManager
    {
        public string PDFWriter(string name, List<Inventory> collection, int userId)
        {
            // Create a new PDF document
            Document doc = new Document();

            string outputPath = @"C:\pdf\" + name;

            FileStream fileStream = new FileStream(outputPath, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(doc, fileStream);

            // Open the PDF document
            doc.Open();

            foreach (var item in collection.Cast<Inventory>())
            {
                if (item.UserId == userId)
                {
                    doc.Add(new Paragraph($"Item Name: {item.ItemName}"));
                    doc.Add(new Paragraph($"Quantity: {item.QuantityInStock}"));
                    doc.Add(new Paragraph($"Cost: {item.Cost:f2} lev"));
                    doc.Add(new Paragraph($"Selling Price: {item.SellingPrice:f2} lev"));
                    doc.Add(new Paragraph($"Description: {item.Description}"));
                    doc.Add(new Paragraph(" "));
                }
            }

            // Close the PDF document
            doc.Close();

            return outputPath;
        }

        public string PDFWriter(string name, List<Customers> customers, List<Transactions> transactions, List<Users> users, int userId, string customerName, DateTime StartDate, DateTime EndDate)
        {
            // Create a new PDF document
            Document doc = new Document();

            string outputPath = @"C:\pdf\" + name + ".pdf";

            FileStream fileStream = new FileStream(outputPath, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(doc, fileStream);

            // Open the PDF document
            doc.Open();
            
            Paragraph p = new Paragraph($"Invoice {name}");
            p.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            doc.Add(p);

            Paragraph dates = new Paragraph($"From {StartDate.Day.ToString()}/{StartDate.Month.ToString()}/{StartDate.Year.ToString()} to {StartDate.Day.ToString()}/{StartDate.Month.ToString()}/{StartDate.Year.ToString()}");
            dates.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            doc.Add(dates);

            foreach (var user in users)
            {
                if(user.UserId == userId)
                {
                    Paragraph pSellerName = new Paragraph($"Seller name: {user.FirstName} {user.SecondName} {user.LastName}");
                    doc.Add(pSellerName);
                }
            }
            

            foreach (var customer in customers)
            {
                if (customer.UserId == userId && customer.Name == customerName)
                { //we have fount our wanted customer
                    Paragraph pCustomerName = new Paragraph($"Customer name: {customerName}");
                    pCustomerName.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    doc.Add(pCustomerName);

                    Paragraph pAddress = new Paragraph($"Adress: {customer.Address}");
                    pAddress.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    doc.Add(pAddress);

                    Paragraph pEmail = new Paragraph($"Email: {customer.Email}");
                    pEmail.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    doc.Add(pEmail);

                    Paragraph pPhone = new Paragraph($"Phone: {customer.Phone}");
                    pPhone.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    doc.Add(pPhone);

                    Paragraph space1 = new Paragraph(" ");
                    doc.Add(space1);

                    Paragraph description = new Paragraph($"Item name {"",5} Quantity {"",5} Price {"",5} Tax rate {"", 5} Total");
                    doc.Add(description);

                    foreach (var transaction in transactions)
                    { //which transactions include him from a certain date to another
                        
                        if (transaction.CustomerId == customer.CustomerId && StartDate.CompareTo(transaction.TransactionDate) <= 0 &&
                            EndDate.CompareTo(transaction.TransactionDate) >= 0)
                        {

                            Paragraph space = new Paragraph(" ");
                            doc.Add(space);

                            Paragraph pTransaction = new Paragraph($"{transaction.ItemName} {"", 10} {transaction.Quantity} {"", 10} {transaction.Price:f2} lev {"", 10} 20% {"" , 10} {(transaction.Price * transaction.Quantity * 1.2):f2} lev");
                            doc.Add(pTransaction);

                        }
                    }
                }
            }

            // Close the PDF document
            doc.Close();

            return outputPath;
        }


        public async Task PDFDownloader(string filePath)
        {

            byte[] fileBytes = File.ReadAllBytes(filePath);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(fileBytes)
            };

            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = System.IO.Path.GetFileName(filePath)
            };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            var bytes = await response.Content.ReadAsByteArrayAsync();

            // Save the file to the Downloads folder
            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string downloadedFilePath = System.IO.Path.Combine(downloadsPath, System.IO.Path.GetFileName(filePath));
            File.WriteAllBytes(downloadedFilePath, bytes);
        }

        
    }
}
