namespace RentManager.UI.Views;

public partial class ViewGuestPage : ContentPage
{
    public ViewGuestPage(ViewGuestViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}