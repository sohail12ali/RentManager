namespace RentManager.UI.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private bool isBusy;

}
