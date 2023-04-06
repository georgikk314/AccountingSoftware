using AccountingSoftware.Data;
using AccountingSoftware.ViewModels;

namespace AccountingSoftware;

public partial class App : Application
{
	public App()
	{
		//entry point of the software
		InitializeComponent();
		var dbContext = new AccountingSoftwareContext();
		//MainPage = new NavigationPage(new UserAuthentication(dbContext));
		MainPage = new NavigationPage(new UserAuthentication(dbContext));
	}
}
