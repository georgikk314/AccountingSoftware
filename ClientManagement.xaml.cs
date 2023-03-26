using AccountingSoftware.Data;
using AccountingSoftware.ViewModels;

namespace AccountingSoftware;

public partial class ClientManagement : ContentPage
{
    private readonly AccountingSoftwareContext _dbContext;
    private readonly AddClientViewModel _model;

    public ClientManagement(AccountingSoftwareContext dbContext, int userId)
	{
		InitializeComponent();
        _dbContext = dbContext;
        _model = new AddClientViewModel(dbContext, userId);
        BindingContext = _model;
	}

}