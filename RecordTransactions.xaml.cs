using AccountingSoftware.Data;
using AccountingSoftware.ViewModels;

namespace AccountingSoftware;

public partial class RecordTransactions : ContentPage
{
	private readonly AccountingSoftwareContext _dbContext;
	private readonly AddTransactionViewModel _model;
	private int _userId;
	public RecordTransactions(AccountingSoftwareContext dbContext, int userId)
	{
		InitializeComponent();
		_dbContext = dbContext;
	    _userId = userId;
		_model = new AddTransactionViewModel(dbContext, userId);
		BindingContext = _model;
	}

}