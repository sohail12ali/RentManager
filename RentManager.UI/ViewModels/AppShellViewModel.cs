namespace RentManager.UI.ViewModels;

public partial class AppShellViewModel : BaseViewModel
{
    #region Fields & Properties

    [ObservableProperty]
    private string guestSectionTitle;

    [ObservableProperty]
    private string electricBillSectionTitle;

    [ObservableProperty]
    private string rentSectionTitle;

    #endregion Fields & Properties

    #region Constructor

    public AppShellViewModel()
    {
        GuestSectionTitle = AppResource.FTGuestSection;
        ElectricBillSectionTitle = AppResource.FTElectricBillSection;
        RentSectionTitle = AppResource.FTRentSection;
    }

    #endregion Constructor

}