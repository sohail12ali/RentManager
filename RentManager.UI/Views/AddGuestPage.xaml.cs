namespace RentManager.UI.Views;

public partial class AddGuestPage : ContentPage
{
    public AddGuestPage(AddGuestViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}