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
    public class AddClientViewModel : BindableObject
    {
        public string ClientName { get; set; }
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

        private readonly AccountingSoftwareContext _dbContext;
        private int _userId;

        public AddClientViewModel(AccountingSoftwareContext dbContext , int userId)
        {
            _dbContext = dbContext;
            _userId = userId;
        }

        public ICommand OnSaveClientClicked => new Command(async () =>
        {
            ClientManagementValidation.Validation(_dbContext, this, _userId);

            await _dbContext.SaveChangesAsync(); 
        });
    }
}
