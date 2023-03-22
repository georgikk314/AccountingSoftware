using AccountingSoftware.Data;
using AccountingSoftware.ViewModels;

namespace AccountingSoftware;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		var dbContext = new AccountingSoftwareContext();
		var model = new AddUsersViewModel(dbContext);
		MainPage = new NavigationPage(new RegisterForm(dbContext, model));
	}
}
