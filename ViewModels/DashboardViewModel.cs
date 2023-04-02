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
    public class DashboardViewModel : BindableObject, INotifyPropertyChanged
    {
        private ObservableCollection<Transactions> _transactions;
        public ObservableCollection<Transactions> Transactions
        {
            get { return _transactions; }
            set
            {
                _transactions = value;
                OnPropertyChanged(nameof(Transactions));
            }
        }

        private string _totalRevenue;
        public string TotalRevenue 
        {
            get
            {
                return _totalRevenue;
            }
            set
            {
                _totalRevenue = value;
                OnPropertyChanged(nameof(TotalRevenue));
            }
        }

        private string _balance;
        public string Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                _balance = value;
                OnPropertyChanged(nameof(Balance));
            }
        }

        private string _profit;
        public string Profit
        {
            get
            {
                return _profit;
            }
            set
            {
                _profit = value;
                OnPropertyChanged(nameof(Profit));
            }
        }

        /*
        private string _clientName;
        public string ClientName
        {
            get
            {
                return _clientName;
            }
            set { OnPropertyChanged(nameof(ClientName));}
        }

        private string _transactionType;
        public string TransactionType
        {
            get
            {
                return _transactionType;
            }
            set
            {
                OnPropertyChanged(nameof(TransactionType));
            }
        }

        private DateTime _transactionDate;
        public DateTime TransactionDate
        {
            get
            {
                return _transactionDate;
            }
            set { OnPropertyChanged(nameof(TransactionDate));}
        }

        private string _itemName;
        public string ItemName
        {
            get
            {
                return _itemName;
            }
            set
            {
                OnPropertyChanged(nameof(_itemName));
            }
        }

        private string _quantity;
        public string Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private string _price;
        public string Price
        {
            get
            {
                return _price;
            }
            set { OnPropertyChanged(nameof(Price));}
        }*/

        private readonly AccountingSoftwareContext _dbContext;
        private int _userId;

        public DashboardViewModel(AccountingSoftwareContext dbContext, int userId)
        {
            _dbContext = dbContext;
            _userId = userId;
            Transactions = new ObservableCollection<Transactions>();

            double totalRev = 0.00;
            double totalBal = 0.00;
            double totalProf = 0.00;

            foreach (var user in dbContext.Users)
            {
                if (user.UserId == userId)
                {
                    foreach (var item in dbContext.Inventory)
                    {
                        totalRev += item.SellingPrice * item.QuantityInStock;
                        totalBal += item.Cost * item.QuantityInStock;
                    }
                    TotalRevenue = Math.Round(totalRev, 2).ToString();
                    Balance = Math.Round(totalBal, 2).ToString();

                    foreach (var transaction in dbContext.Transactions)
                    {
                        Transactions.Add(transaction);
                        if (transaction.UserId == userId)
                        {
                            if (transaction.TransactionType == "Expense")
                            {
                                totalProf = totalProf - transaction.Quantity * transaction.Price;

                            }

                            if (transaction.TransactionType == "Income")
                            {
                                totalProf = totalProf + transaction.Quantity * transaction.Price;
                            }
                        }
                    }
                    totalProf = totalProf - totalBal;
                    Profit = Math.Round(totalProf, 2).ToString();
                }
            }
        }

        public void Notify()
        {
            OnPropertyChanged(nameof(Transactions));
            OnPropertyChanged(nameof(TotalRevenue));
            OnPropertyChanged(nameof(Balance));
            OnPropertyChanged(nameof(Profit));
        }

    }
}
