using RentManager.Common.Constants;
using RentManager.Common.Models;
using RentManager.DataAccess.DataServices;

namespace RentManager.UI.ViewModels;

public partial class AddGuestViewModel : BaseViewModel
{
    #region Fields & Properties

    [ObservableProperty]
    private string guestName;

    [ObservableProperty]
    private string guestEmail;

    [ObservableProperty]
    private string guestPhone;

    [ObservableProperty]
    private string guestAddress;

    [ObservableProperty]
    private string guestVerifyDocType;

    [ObservableProperty]
    private string guestVerifyDoc;

    private readonly IDataService data;

    #endregion Fields & Properties

    #region Constructor

    public AddGuestViewModel(IDataService data)
    {
        Debug.WriteLine($"Init Class: {GetType().Name}, Method: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
        Title = AppResource.AddGuestPageTitle;
        this.data = data;
    }

    #endregion Constructor

    #region Tasks & Methods

    [RelayCommand(CanExecute = nameof(IsNotBusy))]
    private async Task AddGuestAsync()
    {
        try
        {
            if (IsBusy) return;
            IsBusy = true;

            PayingGuest payingGuest = new()
            {
                GuestAddress = GuestAddress,
                GuestEmail = GuestEmail,
                GuestPhone = GuestPhone,
                GuestName = GuestName,
                GuestVerifyDoc = GuestVerifyDoc,
                GuestVerifyDocType = GuestVerifyDocType,
                Created = DateTime.Now,
                Updated = DateTime.Now,
            };

            var response = await data.AddGuest(payingGuest);
            if (response)
            {
                await ShowToast(AppResource.TMGuestSavedPassed);
                ClearFields();
            }
            else
            {
                await ShowToast(AppResource.TMGuestSavedFailed);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message, ex);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void ClearFields()
    {
        GuestName = string.Empty;
        GuestVerifyDoc = string.Empty;
        GuestVerifyDocType = string.Empty;
        GuestAddress = string.Empty;
        GuestEmail = string.Empty;
        GuestPhone = string.Empty;
    }

    #endregion Tasks & Methods
}