using AccountingSoftware.Data;
using AccountingSoftware.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using XAct;
using XSystem.Security.Cryptography;

namespace AccountingSoftware;

public partial class RegisterForm : ContentPage
{
    private readonly AccountingSoftwareContext _dbContext;
   // private readonly AddUsersViewModel _model;
    public RegisterForm(AccountingSoftwareContext dbContext)
	{
        InitializeComponent();
        _dbContext = dbContext;
        BindingContext = new AddUsersViewModel(dbContext);
        
    }

    private void AddRegisteredUsersToDatabase()
    {
        /* validation
        if (!((UsernameEntry.Text != "" && (UsernameEntry.Text[0] >= 'a' && UsernameEntry.Text[0] <= 'z') || (UsernameEntry.Text[0] >= 'A' && UsernameEntry.Text[0] <= 'Z'))
          && PasswordEntry.Text != "" && PasswordEntry.Text.Length < 50 
          && FirstNameEntry.Text != "" &&
            FirstNameEntry.Text.Length < 50 
          && SecondNameEntry.Text.Length < 50
          && LastNameEntry.Text != "" && LastNameEntry.Text.Length < 50))
        {
            ErrorMessageForInvalidEntries.IsVisible = true;
        }

        else if(PasswordEntry.Text != ConfirmPasswordEntry.Text)
        {
            ErrorMessageForNotMatchingPasswords.IsVisible = true;
        }

        else
        {
            var md5 = new MD5CryptoServiceProvider();
            _model.Username = UsernameEntry.Text;
            byte[] Password = Encoding.ASCII.GetBytes(PasswordEntry.Text);
            _model.PasswordHash = Convert.ToBase64String(md5.ComputeHash(Password));
            _model.FirstName = FirstNameEntry.Text;
            _model.SecondName = SecondNameEntry.Text;
            _model.LastName = LastNameEntry.Text;
            //_dbContext.SaveChanges();
        }
        */
    }

    private void OnRegisterClicked(object sender, EventArgs e)
    {
        AddRegisteredUsersToDatabase();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        //needs optimising for memory leaks (1MB added when changing windows)
        await Navigation.PushAsync(new UserAuthentication(_dbContext));

        GC.Collect(); //optimised 10MB memory leak
    }
}