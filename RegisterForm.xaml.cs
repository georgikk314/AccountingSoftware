using AccountingSoftware.Data;
using AccountingSoftware.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using XAct;
using XAct.Users;
using XSystem.Security.Cryptography;

namespace AccountingSoftware;

public partial class RegisterForm : ContentPage
{

    private bool _isInvalidEntriesMessageVisible = false;
    public bool IsInvalidEntriesMessageVisible
    {
        get { return _isInvalidEntriesMessageVisible; }
        set
        {
            _isInvalidEntriesMessageVisible = value;
            OnPropertyChanged(nameof(IsInvalidEntriesMessageVisible));
        }
    }

    private bool _isNotMatchingPasswordsMessageVisible = false;
    public bool IsNotMatchingPasswordsMessageVisible
    {
        get { return _isNotMatchingPasswordsMessageVisible; }
        set
        {
            _isNotMatchingPasswordsMessageVisible = value;
            OnPropertyChanged(nameof(IsNotMatchingPasswordsMessageVisible));
        }
    }

    private readonly AccountingSoftwareContext _dbContext;
    private readonly AddUsersViewModel _model;
    public RegisterForm(AccountingSoftwareContext dbContext, AddUsersViewModel model)
    {
        InitializeComponent();
        _dbContext = dbContext;
        _model = model;
        BindingContext = _model;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        //needs optimising for memory leaks (1MB added when changing windows)
        await Navigation.PopAsync();
        UsernameEntry.Text = null;
        PasswordEntry.Text = null;
        ConfirmPasswordEntry.Text = null;
        EmailEntry.Text = null;
        FirstNameEntry.Text = null;
        SecondNameEntry.Text = null;
        LastNameEntry.Text = null;
        await Navigation.PushAsync(new UserAuthentication(_dbContext, _model));

        GC.Collect(); //optimised 10MB memory leak
    }
}