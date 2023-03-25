using AccountingSoftware.Data;
using AccountingSoftware.Validations;
using AccountingSoftware.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using System.Security.Cryptography;

namespace AccountingSoftware;

public partial class UserAuthentication : ContentPage
{
    private readonly AccountingSoftwareContext _dbContext;
    private readonly LoginViewModel _model;
    public UserAuthentication(AccountingSoftwareContext dbContext)
	{
        InitializeComponent();
        _dbContext = dbContext;
        _model = new LoginViewModel();
        BindingContext = _model;
    }

    private async void LoginButtonClicked(object sender, EventArgs e)
	{
        if(LoginValidation.Validation(_dbContext, _model))
        {
            
            UsernameEntry.Text = null;
            PasswordEntry.Text = null;

            await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new Dashboard());
        }
        else
        {
            NotMatchingCredentialsMessage.IsVisible = true;
        }
	}

	private async void OnRegisterClicked(object sender, EventArgs e)
	{
        UsernameEntry.Text = null;
        PasswordEntry.Text = null;

       // await Application.Current.MainPage.Navigation.PopAsync();
        await Application.Current.MainPage.Navigation.PushAsync(new RegisterForm(_dbContext, new AddUsersViewModel(_dbContext)));
		GC.Collect(); //optimised 10MB memory leak
    }
}