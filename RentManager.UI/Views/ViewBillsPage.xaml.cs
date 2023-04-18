namespace RentManager.UI.Views;

public partial class ViewBillsPage : ContentPage
{
    public ViewBillsPage(ViewBillsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}