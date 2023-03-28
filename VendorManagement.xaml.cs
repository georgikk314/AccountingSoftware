using AccountingSoftware.Data;
using AccountingSoftware.ViewModels;

namespace AccountingSoftware;

public partial class VendorManagement : ContentPage
{
    private readonly AccountingSoftwareContext _dbContext;
    private readonly AddVendorViewModel _model;
    public VendorManagement(AccountingSoftwareContext dbContext, int userId)
	{
		InitializeComponent();
        _dbContext = dbContext;
        _model = new AddVendorViewModel(dbContext, userId);
        BindingContext = _model;
    }

}