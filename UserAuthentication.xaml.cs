using Microsoft.Maui.Controls;

namespace AccountingSoftware;

public partial class UserAuthentication : ContentPage
{
	public UserAuthentication()
	{
		InitializeComponent();
	}

    private void LoginButtonClicked(object sender, EventArgs e)
	{

	}

	private async void OnRegisterClicked(object sender, EventArgs e)
	{
		//needs optimising for memory leaks (1MB added when changing windows)
        await Navigation.PushAsync(new RegisterForm());
		GC.Collect(); //optimised 10MB memory leak
    }
}