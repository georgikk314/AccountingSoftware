using AccountingSoftware.Data;
using AccountingSoftware.ViewModels;

namespace AccountingSoftware;

public partial class EditVendorManagement : ContentPage
{
    private readonly AccountingSoftwareContext _dbContext;
    private readonly EditVendorViewModel _model;
    public EditVendorManagement(AccountingSoftwareContext dbContext, int userId, EditCustomerVendorViewModel model)
	{
        InitializeComponent();
        _dbContext = dbContext;
        _model = new EditVendorViewModel(dbContext, userId);
        _model.Name = model.SelectedItemV.Name;
        _model.Phone = model.SelectedItemV.Phone;
        _model.Email = model.SelectedItemV.Email;
        _model.Address = model.SelectedItemV.Address;
        _model.SelectedItem = model.SelectedItemV;
        BindingContext = _model;
    }
}