using RentManager.Common.Constants;

namespace RentManager.UI.ViewModels;

public partial class ViewGuestViewModel : BaseViewModel
{
    public ViewGuestViewModel()
    {
        Debug.WriteLine($"Init Class: {GetType().Name}, Method: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
        Title = AppResource.ViewGuestPageTitle;
    }
}