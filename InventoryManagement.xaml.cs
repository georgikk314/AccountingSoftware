using AccountingSoftware.Data;

namespace AccountingSoftware;

public partial class InventoryManagement : ContentPage
{
    private readonly AccountingSoftwareContext _dbContext;
    private int _userId;
	public InventoryManagement(AccountingSoftwareContext dbContext, int userId)
	{
		InitializeComponent();
        _dbContext = dbContext;
        _userId = userId;
	}

    private async void OnAddItemClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new AddItemPage(_dbContext, _userId));
    }

    private void OnDownloadPdfClicked(object sender, EventArgs e)
    {
        //TODO:
    }
}