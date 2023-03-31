using AccountingSoftware.Data;
using AccountingSoftware.Data.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using XAct;

namespace AccountingSoftware.ViewModels
{
    public class InventoryViewModel : BindableObject, INotifyPropertyChanged
    {
        private ObservableCollection<Inventory> _items;
        public ObservableCollection<Inventory> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        public string SearchText { get; set; }

        private readonly AccountingSoftwareContext _dbContext;
        private int _userId;
        public InventoryViewModel()
        {

        }
        public InventoryViewModel(AccountingSoftwareContext dbContext, int userId, List<Inventory> showedItems) 
        {
            Items = new ObservableCollection<Inventory>(showedItems);
            _dbContext = dbContext;
            _userId = userId;
        }
        /*
        public void SearchItem(object sender, EventArgs e)
        {
                foreach (var item in Items)
                {
                    if(item.UserId == _userId && item.ItemName.Contains(SearchText))
                    {
                        Application.Current
                        .MainPage
                        .Navigation.PushAsync(new InventoryManagement(_dbContext, this, _userId));
                    }
                }
                
                
            }*/
        

        public void Notify()
        {
            OnPropertyChanged(nameof(Items));
        }
    }
}
