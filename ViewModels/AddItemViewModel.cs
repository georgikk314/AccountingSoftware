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
    public class AddItemViewModel : BindableObject
    {
        private string _itemName;
        public string ItemName
        {
            get
            {
                return _itemName;
            }
            set
            {
                _itemName = value;
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set { _description = value; }
        }

        private string _sellingPrice;
        public string SellingPrice
        {
            get
            {
                return _sellingPrice;
            }
            set
            {
                _sellingPrice = value;
            }
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
        private Inventory _selectedItem;
        public AddItemViewModel(AccountingSoftwareContext dbContext, int userId, InventoryViewModel itemModel)
        {
            _dbContext = dbContext;
            _userId = userId;
            Inventory selectedItem = itemModel.SelectedItem;
            if(selectedItem != null)
            {
                _selectedItem = selectedItem;
                _itemName = _selectedItem.ItemName;
                _sellingPrice = _selectedItem.SellingPrice.ToString();
                _description = _selectedItem.Description;
                
            }

        }

        public ICommand OnEditClicked => new Command(async () =>
        {
            
            AddItemValidation.Validation(_dbContext, this, _userId, _selectedItem);
            

            await _dbContext.SaveChangesAsync();


        });
    }
}
