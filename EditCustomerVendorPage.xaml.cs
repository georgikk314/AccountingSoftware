using AccountingSoftware.Data;
using AccountingSoftware.ViewModels;

namespace AccountingSoftware;

public partial class EditCustomerVendorPage : ContentPage
{
    private readonly AccountingSoftwareContext _dbContext;
    private readonly EditCustomerVendorViewModel _model;
    private readonly int _userId;
    public EditCustomerVendorPage(AccountingSoftwareContext dbContext, int userId)
	{
		InitializeComponent();
        _dbContext = dbContext;
        _userId = userId;
        _model = new EditCustomerVendorViewModel(dbContext, userId);
        BindingContext = _model;

	}

    private async void OnEditCustomerVendorClicked(object sender, EventArgs e)
    {
        if (CustomersList.SelectedItem != null)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EditClientManagement(_dbContext, _userId, _model));
        }
        else if(VendorsList.SelectedItem != null)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EditVendorManagement(_dbContext, _userId, _model));
        }

    }

    protected override void OnAppearing()
    {
        _model.Notify();
        base.OnAppearing();
    }

}