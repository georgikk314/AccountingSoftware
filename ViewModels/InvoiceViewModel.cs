using AccountingSoftware.Data;
using AccountingSoftware.PdfManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AccountingSoftware.ViewModels
{
    public class InvoiceViewModel : BindableObject
    {
        public string ClientName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        private bool _isInvoiceDownloadedMessageVisible = false;

        //property for access to this element in different classes
        public bool IsInvoiceDownloadedMessageVisible
        {
            get { return _isInvoiceDownloadedMessageVisible; }
            set
            {
                _isInvoiceDownloadedMessageVisible = value;
                OnPropertyChanged(nameof(IsInvoiceDownloadedMessageVisible));
            }
        }

        private readonly AccountingSoftwareContext _dbContext;
        private int _userId;

        public InvoiceViewModel(AccountingSoftwareContext dbContext, int userId)
        {
            _dbContext = dbContext;
            _userId = userId;

        }

        public ICommand OnGenerateInvoiceClicked => new Command(async () =>
        {
            PDFManager PDF = new PDFManager();
            string filepath = PDF.PDFWriter("0000000002", _dbContext.Customers.ToList(), _dbContext.Transactions.ToList(), _dbContext.Users.ToList(), _userId, ClientName, StartDate, EndDate);
            await PDF.PDFDownloader(filepath);
            IsInvoiceDownloadedMessageVisible = true;
            
        });
    }
}
