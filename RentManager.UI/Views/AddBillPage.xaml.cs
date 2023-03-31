namespace RentManager.UI.Views;

public partial class AddBillPage : ContentPage
{
    public AddBillPage(AddBillViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}