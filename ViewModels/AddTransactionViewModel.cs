﻿using AccountingSoftware.Data;
using AccountingSoftware.Validations;
using Microsoft.Maui.Graphics.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AccountingSoftware.ViewModels
{
    public class AddTransactionViewModel : BindableObject
    {
        private double _amount;

        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string AmountEntry { get; set; }
        public double Amount 
        { 
            get
            {
                return _amount;
            }
            set
            {
                _amount = double.Parse(AmountEntry);
            }
        }
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


        private readonly AccountingSoftwareContext _dbContext;
        private int _userId;
        public AddTransactionViewModel(AccountingSoftwareContext dbContext, int userId) 
        {
            _dbContext = dbContext;
            _userId = userId;
        }

        public ICommand OnSaveTransactionClicked => new Command(async () =>
        {
            RecordTransactionValidation.Validation(_dbContext, this, _userId);
            Description = "";
            AmountEntry = "";

            await _dbContext.SaveChangesAsync();


        });

    }
}