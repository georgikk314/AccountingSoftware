namespace AccountingSoftware;

public partial class RegisterForm : ContentPage
{
	public RegisterForm()
	{
		InitializeComponent();
	}

    private void OnRegisterClicked(object sender, EventArgs e)
    {
        //TODO:
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        //needs optimising for memory leaks (1MB added when changing windows)
        await Navigation.PushAsync(new UserAuthentication());

        GC.Collect(); //optimised 10MB memory leak
    }
}