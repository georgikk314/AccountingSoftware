using AccountingSoftware.Data;
using AccountingSoftware.ViewModels;

namespace AccountingSoftware;

public partial class Invoices : ContentPage
{
	private readonly AccountingSoftwareContext _dbContext;
	private readonly InvoiceViewModel _model;
	private int _userId;
	public Invoices(AccountingSoftwareContext dbContext, int userId)
	{
		InitializeComponent();
		_dbContext = dbContext;
		_userId = userId;
		_model = new InvoiceViewModel(dbContext, userId);
		BindingContext = _model;
	}

	
}