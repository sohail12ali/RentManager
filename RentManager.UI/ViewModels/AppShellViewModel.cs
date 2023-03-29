namespace RentManager.UI.ViewModels;

public partial class AppShellViewModel : BaseViewModel
{
    [ObservableProperty]
    private string guestSectionTitle;

    [ObservableProperty]
    private string electricBillSectionTitle;

    [ObservableProperty]
    private string rentSectionTitle;

    public AppShellViewModel()
    {
        GuestSectionTitle = AppResource.FTGuestSection;
        ElectricBillSectionTitle = AppResource.FTElectricBillSection;
        RentSectionTitle = AppResource.FTRentSection;
    }
}