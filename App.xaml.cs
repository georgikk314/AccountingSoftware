using AccountingSoftware.Data;

namespace AccountingSoftware;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		var dbContext = new AccountingSoftwareContext();
		MainPage = new NavigationPage(new RegisterForm(dbContext));
	}
}
