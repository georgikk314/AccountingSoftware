using AccountingSoftware.Data;
using AccountingSoftware.ViewModels;

namespace AccountingSoftware;

public partial class Dashboard : ContentPage
{
	private readonly AccountingSoftwareContext _dbContext;
	private readonly DashboardViewModel _model;
	private int _userId;
	public Dashboard(AccountingSoftwareContext dbContext, int userId)
	{
		InitializeComponent();
		_dbContext = dbContext;
		_userId = userId;
		_model = new DashboardViewModel(dbContext, userId);
		BindingContext = _model;

	}

    protected override void OnAppearing()
    {
        _model.Notify();
        base.OnAppearing();
    }

}