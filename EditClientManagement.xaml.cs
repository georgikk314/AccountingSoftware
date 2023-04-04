using AccountingSoftware.Data;
using AccountingSoftware.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AccountingSoftware;

public partial class EditClientManagement : ContentPage
{
    private readonly AccountingSoftwareContext _dbContext;
    private readonly EditClientViewModel _model;
    public EditClientManagement(AccountingSoftwareContext dbContext, int userId, EditCustomerVendorViewModel model)
    {
		InitializeComponent();
        _dbContext = dbContext;
        _model = new EditClientViewModel(dbContext, userId);
        _model.Name = model.SelectedItem.Name;
        _model.Phone = model.SelectedItem.Phone;
        _model.Email = model.SelectedItem.Email;
        _model.Address = model.SelectedItem.Address;
        _model.SelectedItem = model.SelectedItem;
        BindingContext = _model;
    }
}