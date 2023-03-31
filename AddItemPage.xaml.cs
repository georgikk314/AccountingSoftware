using AccountingSoftware.Data;
using AccountingSoftware.ViewModels;

namespace AccountingSoftware;

public partial class AddItemPage : ContentPage
{
	private readonly AccountingSoftwareContext _dbContext;
	private readonly AddItemViewModel _model;
	private int _userId;
	public AddItemPage(AccountingSoftwareContext dbContext, int userId)
	{
		InitializeComponent();
		_dbContext = dbContext;
		_model = new AddItemViewModel(dbContext, userId);
		_userId = userId;
		BindingContext = _model;
	}
}