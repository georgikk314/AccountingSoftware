using AccountingSoftware.Data;
using AccountingSoftware.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AccountingSoftware.ViewModels
{
    public class AddVendorViewModel : BindableObject
    {
        public string VendorName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

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

        private bool _isNotCorrectPhoneMessageVisible = false;

        //property for access to this element in different classes
        public bool IsNotCorrectPhoneMessageVisible
        {
            get { return _isNotCorrectPhoneMessageVisible; }
            set
            {
                _isNotCorrectPhoneMessageVisible = value;
                OnPropertyChanged(nameof(IsNotCorrectPhoneMessageVisible));
            }
        }

        private bool _isNotCorrectEmailMessageVisible = false;

        //property for access to this element in different classes
        public bool IsNotCorrectEmailMessageVisible
        {
            get { return _isNotCorrectEmailMessageVisible; }
            set
            {
                _isNotCorrectEmailMessageVisible = value;
                OnPropertyChanged(nameof(IsNotCorrectEmailMessageVisible));
            }
        }

        private bool _isAddedVendorVisible = false;

        //property for access to this element in different classes
        public bool IsAddedVendorVisible
        {
            get { return _isAddedVendorVisible; }
            set
            {
                _isAddedVendorVisible = value;
                OnPropertyChanged(nameof(IsAddedVendorVisible));
            }
        }

        private readonly AccountingSoftwareContext _dbContext;
        private int _userId;

        public AddVendorViewModel(AccountingSoftwareContext dbContext, int userId)
        {
            _dbContext = dbContext;
            _userId = userId;
        }

        public ICommand OnSaveVendorClicked => new Command(async () =>
        {
            VendorManagementValidation.Validation(_dbContext, this, _userId);

            VendorName = "";
            PhoneNumber = "";
            Address = "";
            Email = "";

            await _dbContext.SaveChangesAsync();
        });
    }
}
