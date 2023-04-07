using AccountingSoftware.Data;
using AccountingSoftware.Data.Tables;
using AccountingSoftware.Validations;
using Microsoft.Maui.Graphics.Text;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AccountingSoftware.ViewModels
{
    public class AddTransactionViewModel : BindableObject
    {
        public int TransactionId { get; set; }

        private DateTime _transactionDate = DateTime.Today;
        public DateTime TransactionDate 
        {   
            get
            {
                return _transactionDate;
            }
            set
            {
                _transactionDate = value;
            }
        }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string VendorOrCustomerName { get; set; }
        public string TransactionType { get; set; }


        private bool _isEmptyFieldsMessageVisible = false;

        //property for access to this element in different classes
        public bool IsEmptyFieldsMessageVisible
        {
            get { return _isEmptyFieldsMessageVisible; }
            set
            {
                _isEmptyFieldsMessageVisible = value;
                OnPropertyChanged(nameof(IsEmptyFieldsMessageVisible));
            }
        }

        private bool _isChosenTransactionTypeMessageVisible = false;

        //property for access to this element in different classes
        public bool IsChosenTransactionTypeMessageVisible
        {
            get { return _isChosenTransactionTypeMessageVisible; }
            set
            {
                _isChosenTransactionTypeMessageVisible = value;
                OnPropertyChanged(nameof(IsChosenTransactionTypeMessageVisible));
            }
        }

        private bool _isWrongAmountEnteredMessageVisible = false;

        //property for access to this element in different classes
        public bool IsWrongAmountEnteredMessageVisible
        {
            get { return _isWrongAmountEnteredMessageVisible; }
            set
            {
                _isWrongAmountEnteredMessageVisible = value;
                OnPropertyChanged(nameof(IsWrongAmountEnteredMessageVisible));
            }
        }

        private bool _isNonExistingVendorMessageVisible = false;

        //property for access to this element in different classes
        public bool IsNonExistingVendorMessageVisible
        {
            get { return _isNonExistingVendorMessageVisible; }
            set
            {
                _isNonExistingVendorMessageVisible = value;
                OnPropertyChanged(nameof(IsNonExistingVendorMessageVisible));
            }
        }

        private bool _isNonExistingCustomerMessageVisible = false;

        //property for access to this element in different classes
        public bool IsNonExistingCustomerMessageVisible
        {
            get { return _isNonExistingCustomerMessageVisible; }
            set
            {
                _isNonExistingCustomerMessageVisible = value;
                OnPropertyChanged(nameof(IsNonExistingCustomerMessageVisible));
            }
        }

        private bool _isSuccessfulTransactionMessageVisible = false;

        //property for access to this element in different classes
        public bool IsSuccessfulTransactionMessageVisible
        {
            get { return _isSuccessfulTransactionMessageVisible; }
            set
            {
                _isSuccessfulTransactionMessageVisible = value;
                OnPropertyChanged(nameof(IsSuccessfulTransactionMessageVisible));
            }
        }

        private bool _isNonExistingItemMessageVisible = false;

        //property for access to this element in different classes
        public bool IsNonExistingItemMessageVisible
        {
            get { return _isNonExistingItemMessageVisible; }
            set
            {
                _isNonExistingItemMessageVisible = value;
                OnPropertyChanged(nameof(IsNonExistingItemMessageVisible));
            }
        }

        private bool _isInvalidQuantityMessageVisible = false;

        //property for access to this element in different classes
        public bool IsInvalidQuantityMessageVisible
        {
            get { return _isInvalidQuantityMessageVisible; }
            set
            {
                _isInvalidQuantityMessageVisible = value;
                OnPropertyChanged(nameof(IsInvalidQuantityMessageVisible));
            }
        }

        private readonly AccountingSoftwareContext _dbContext;
        private int _userId;
        public AddTransactionViewModel(AccountingSoftwareContext dbContext, int userId) 
        {
            _dbContext = dbContext;
            _userId = userId;
        }

        public AddTransactionViewModel() { }


        public ICommand OnSaveTransactionClicked => new Command(async () =>
        {
            RecordTransactionValidation.Validation(_dbContext, this, _userId);
            Description = "";
            

            await _dbContext.SaveChangesAsync();


        });

    }
}
