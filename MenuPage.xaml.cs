using AccountingSoftware.Data;
using AccountingSoftware.ViewModels;

namespace AccountingSoftware;

public partial class MenuPage : ContentPage
{
    private int _userId;
    private readonly AccountingSoftwareContext _dbContext;
	public MenuPage(AccountingSoftwareContext dbContext, int userId)
    {
        InitializeComponent();
        _userId = userId;
        _dbContext = dbContext;
    }

    private async void OnDashboardClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new Dashboard(_dbContext, _userId));
    }

    private async void OnClientManagementClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new ClientManagement(_dbContext, _userId));
    }

    private async void OnVendorManagementClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new VendorManagement(_dbContext, _userId));
    }

    private async void OnRecordTransactionsClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new RecordTransactions(_dbContext, _userId));
    }

    private async void OnInvoicesClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new Invoices(_dbContext, _userId));
    }

    private async void OnInventoryManagementClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new InventoryManagement(_dbContext, new InventoryViewModel(), _userId));
    }

    private async void OnEditCustomerVendorClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new EditCustomerVendorPage(_dbContext, _userId));
    }

    private void OnExitClicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }

}