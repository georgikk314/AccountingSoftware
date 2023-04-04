using AccountingSoftware.Data;
using AccountingSoftware.Data.Tables;
using AccountingSoftware.PdfManager;
using AccountingSoftware.ViewModels;

namespace AccountingSoftware;

public partial class InventoryManagement : ContentPage
{
    private readonly AccountingSoftwareContext _dbContext;
    private readonly InventoryViewModel _model;
    private int _userId;
	public InventoryManagement(AccountingSoftwareContext dbContext, InventoryViewModel model, int userId)
	{
		InitializeComponent();
        IsReportDownloadedMessageVisible.IsVisible = false;
        _dbContext = dbContext;
        _userId = userId;
        _model = model;
        //BindingContext = model;
	}

    public InventoryManagement(AccountingSoftwareContext dbContext, int userId, List<Inventory> showedItems)
    {
        InitializeComponent();
        IsReportDownloadedMessageVisible.IsVisible = false;
        _dbContext = dbContext;
        _userId = userId;
        _model = new InventoryViewModel(dbContext, _userId, showedItems);
        BindingContext = _model;
    }

    private void SearchItem(object sender, EventArgs e)
    {
        var Items = _dbContext.Inventory;
        var model = new InventoryViewModel();
        List<Inventory> showedItems = new List<Inventory>();
        foreach (var item in Items)
        {
            if (item.UserId == _userId && item.ItemName.ToLower().Contains(SearchText.Text.ToLower()))
            {
                showedItems.Add(item);
            }
            Application.Current
                .MainPage
                .Navigation.PushAsync(new InventoryManagement(_dbContext, _userId, showedItems));
        }


    }

    private async void OnEditItemClicked(object sender, EventArgs e)
    {
        if(collView.SelectedItem == null) return;
        else
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddItemPage(_dbContext, _userId, _model));
        }
       
    }

    private async void OnDownloadPdfClicked(object sender, EventArgs e)
    {
        PDFManager PDF = new PDFManager();
        string filepath = PDF.PDFWriter("items.pdf", _dbContext.Inventory.ToList(), _userId);
        await PDF.PDFDownloader(filepath);
        IsReportDownloadedMessageVisible.IsVisible = true;
    }

    protected override void OnAppearing()
    {
        _model.Notify();
        base.OnAppearing();
    }
}