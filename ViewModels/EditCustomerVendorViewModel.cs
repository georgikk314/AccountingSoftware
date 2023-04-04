using AccountingSoftware.Data;
using AccountingSoftware.Data.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware.ViewModels
{
    public class EditCustomerVendorViewModel : BindableObject, INotifyPropertyChanged
    {
        private ObservableCollection<Customers> _customers;
        public ObservableCollection<Customers> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                OnPropertyChanged(nameof(Customers));
            }
        }

        private ObservableCollection<Vendors> _vendors;
        public ObservableCollection<Vendors> Vendors
        {
            get { return _vendors; }
            set
            {
                _vendors = value;
                OnPropertyChanged(nameof(Vendors));
            }
        }

        private Customers _selectedItem;
        public Customers SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; }
        }

        private Vendors _selectedItemV;
        public Vendors SelectedItemV
        {
            get { return _selectedItemV; }
            set { _selectedItemV = value; }
        }

        private readonly AccountingSoftwareContext _dbContext;
        private readonly int _userId;
        public EditCustomerVendorViewModel(AccountingSoftwareContext dbContext, int userId)
        {
            _dbContext = dbContext;
            _userId = userId;
            Customers = new ObservableCollection<Customers>();
            Vendors = new ObservableCollection<Vendors>();

            foreach (var customer in dbContext.Customers)
            {
                if(customer.UserId == userId)
                {
                    Customers.Add(customer);
                }
            }

            foreach (var vendor in dbContext.Vendors)
            {
                if (vendor.UserId == userId)
                {
                    Vendors.Add(vendor);
                }
            }

        }

        public void Notify()
        {
            OnPropertyChanged(nameof(Customers));
            OnPropertyChanged(nameof(Vendors));
        }
    }
}
