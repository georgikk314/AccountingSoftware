using AccountingSoftware.Data;
using AccountingSoftware.Data.Tables;
using AccountingSoftware.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AccountingSoftware.ViewModels
{
    public class EditVendorViewModel : BindableObject
    {
        private Vendors _selectedItem;
        public Vendors SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

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
        private readonly EditCustomerVendorViewModel _model;
        public EditVendorViewModel(AccountingSoftwareContext dbContext, int userId)
        {
            _dbContext = dbContext;
            _userId = userId;
        }

        public ICommand OnEditVendorClicked => new Command(async () =>
        {

            VendorManagementValidation.Validation(_dbContext, this, _userId, _selectedItem);

            await _dbContext.SaveChangesAsync();
        });
    }
}
