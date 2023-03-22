using AccountingSoftware.Data;
using AccountingSoftware.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;

namespace AccountingSoftware;

public partial class UserAuthentication : ContentPage
{
    private readonly AccountingSoftwareContext _dbContext;
    private readonly AddUsersViewModel _model;
    public UserAuthentication(AccountingSoftwareContext dbContext, AddUsersViewModel model)
	{
        InitializeComponent();
        _dbContext = dbContext;
        _model = model;
        BindingContext = _model;
    }

    private void LoginButtonClicked(object sender, EventArgs e)
	{

	}

	private async void OnRegisterClicked(object sender, EventArgs e)
	{
        //needs optimising for memory leaks (1MB added when changing windows)
        await Navigation.PopAsync();
        UsernameEntry.Text = null;
        PasswordEntry.Text = null;
        await Navigation.PushAsync(new RegisterForm(_dbContext, _model));
		GC.Collect(); //optimised 10MB memory leak
    }
}