namespace RentManager.UI.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    #region Constructor

    public MainViewModel()
    {
        Debug.WriteLine($"Init Class: {GetType().Name}, Method: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
        Title = "Home Page";
    }
    #endregion Constructor
}