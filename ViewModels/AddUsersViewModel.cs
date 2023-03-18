using AccountingSoftware.Data;
using AccountingSoftware.Data.Tables;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AccountingSoftware.ViewModels
{
    public class AddUsersViewModel : BindableObject
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        private readonly AccountingSoftwareContext _dbContext;

        public AddUsersViewModel(AccountingSoftwareContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICommand SaveCommand => new Command(async () =>
        {
            _dbContext.Add(new Users()
            {
                Username = Username,
                PasswordHash = PasswordHash,
                FirstName = FirstName,
                SecondName = SecondName,
                LastName = LastName
            });

            await _dbContext.SaveChangesAsync();

            
        });
    }
}
