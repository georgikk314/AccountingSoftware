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
    public class AddItemViewModel : BindableObject
    {
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string Cost { get; set; }
        public string SellingPrice { get; set; }
        public string QuantityInStock { get; set; }

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

        private bool _isSuccessfulItemAddedMessageVisible = false;

        //property for access to this element in different classes
        public bool IsSuccessfulItemAddedMessageVisible
        {
            get { return _isSuccessfulItemAddedMessageVisible; }
            set
            {
                _isSuccessfulItemAddedMessageVisible = value;
                OnPropertyChanged(nameof(IsSuccessfulItemAddedMessageVisible));
            }
        }
        private readonly AccountingSoftwareContext _dbContext;
        private int _userId;
        public AddItemViewModel(AccountingSoftwareContext dbContext, int userId)
        {
            _dbContext = dbContext;
            _userId = userId;
        }

        public ICommand OnAddClicked => new Command(async () =>
        {
            AddItemValidation.Validation(_dbContext, this, _userId);
            

            await _dbContext.SaveChangesAsync();


        });
    }
}
