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

namespace AccountingSoftware.PdfManager
{
    public class PDFManager : IPDFManager
    {
        public string PDFWriter(string name, List<Inventory> collection, int userId)
        {
            // Create a new PDF document
            Document doc = new Document();

            string outputPath = @"C:\pdf\" + name;

            // Create a new PDF writer and bind it to a memory stream
            //MemoryStream memStream = new MemoryStream();
            FileStream fileStream = new FileStream(outputPath, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(doc, fileStream);

            // Open the PDF document
            doc.Open();

            //Type type = typeof(T);

            //switch(type.Name)
            //{

            //  case "Inventory":

            foreach (var item in collection.Cast<Inventory>())
            {
                if (item.UserId == userId)
                {
                    doc.Add(new Paragraph($"Item Name: {item.ItemName}"));
                    doc.Add(new Paragraph($"Quantity: {item.QuantityInStock}"));
                    doc.Add(new Paragraph($"Cost: {item.Cost:f2} lev"));
                    doc.Add(new Paragraph($"Selling Price: {item.SellingPrice:f2} lev"));
                    doc.Add(new Paragraph($"Description: {item.Description}"));
                }
            }

            //    break;
            // }



            // Close the PDF document
            doc.Close();

            // Convert the memory stream to a byte array
            //byte[] pdfBytes = memStream.ToArray();

            // Save the PDF file to disk
            //File.WriteAllBytes(name, pdfBytes);

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string pdfFilePath = System.IO.Path.Combine(documentsPath, name);

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
