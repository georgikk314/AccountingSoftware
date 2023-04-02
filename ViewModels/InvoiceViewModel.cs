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
        public string NumberOfInvoice { get; set; }

        private DateTime _startDate = DateTime.Today;
        public DateTime StartDate 
        {
            get
            { return _startDate; }
            set { _startDate = value; }
        }

        private DateTime _endDate = DateTime.Today;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set { _endDate = value; }
        }

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
            int characters = NumberOfInvoice.Length;
            if(characters < 10)
            {
                for (int i = 0; i < 10 - characters; i++)
                {
                    NumberOfInvoice = NumberOfInvoice.Insert(0, "0");
                }
            }
            PDFManager PDF = new PDFManager();
            string filepath = PDF.PDFWriter(NumberOfInvoice, _dbContext.Customers.ToList(), _dbContext.Transactions.ToList(), _dbContext.Users.ToList(), _userId, ClientName, StartDate, EndDate);
            await PDF.PDFDownloader(filepath);
            IsInvoiceDownloadedMessageVisible = true;
            
        });
    }
}
