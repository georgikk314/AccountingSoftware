namespace AccountingSoftware;

public partial class MenuPage : ContentPage
{
	public MenuPage()
	{
		InitializeComponent();
	}

    private async void OnDashboardClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new Dashboard());
    }

    private async void OnClientManagementClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new ClientManagement());
    }

    private async void OnVendorManagementClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new VendorManagement());
    }

    private async void OnRecordTransactionsClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new RecordTransactions());
    }

    private async void OnInvoicesClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new Invoices());
    }

    private async void OnInventoryManagementClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new InventoryManagement());
    }

    private async void OnGenerateFinancialReportsClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new GenerateFinancialReports());
    }

    private void OnExitClicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }

}