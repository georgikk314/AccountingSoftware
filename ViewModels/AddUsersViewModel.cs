using AccountingSoftware.Data;
using AccountingSoftware.Data.Tables;
using AccountingSoftware.Validations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public string PasswordEntry { get; set; }
        public string PasswordHash { get; set; }
        public string ConfirmPasswordEntry { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public bool IsInvalidEntriesMessageVisible { get; set; }
        public bool IsNotMatchingPasswordsMessageVisible { get; set; }
        public bool IsNotCorrectEmailMessageVisible { get; set; }

        private readonly AccountingSoftwareContext _dbContext;

        public AddUsersViewModel(AccountingSoftwareContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICommand OnRegisterClicked => new Command(async () =>
        {
            RegisterFormValidation.Validation(_dbContext, this);
    
            await _dbContext.SaveChangesAsync();

            Username = null;
            PasswordEntry = null;
            PasswordHash = null;
            Email = null;
            ConfirmPasswordEntry = null;
            FirstName = null;
            SecondName = null;
            LastName = null;

            await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterForm(_dbContext, this));
            
        });
    }
}
