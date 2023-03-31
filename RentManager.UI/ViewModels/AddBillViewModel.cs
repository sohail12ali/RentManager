using RentManager.Common.Enums;
using RentManager.Common.Helpers;
using RentManager.Common.Models;
using RentManager.DataAccess.DataServices;

using System.Collections.ObjectModel;

namespace RentManager.UI.ViewModels;

public partial class AddBillViewModel : BaseViewModel
{
    #region Fields & Properties

    private readonly IDataService dataService;

    [ObservableProperty]
    private ObservableCollection<PayingGuest> payingGuests = new();

    [ObservableProperty]
    private PayingGuest selectedGuest = null;

    [ObservableProperty]
    private List<string> months = new();

    [ObservableProperty]
    private string selectedMonth = string.Empty;

    [ObservableProperty]
    private DateTimeOffset billStartDate = DateTimeOffset.Now;

    [ObservableProperty]
    private DateTimeOffset billEndDate = DateTimeOffset.Now;

    [ObservableProperty]
    private string pricePerUnit;

    [ObservableProperty]
    private string lastUnit;

    [ObservableProperty]
    private string currentUnit;

    [ObservableProperty]
    private string totalPayableUnits;

    #endregion Fields & Properties

    public AddBillViewModel(IDataService dataService)
    {
        Title = AppResource.PTAddBill;
        this.dataService = dataService;
        Months.AddRange(GetMonths());
    }

    #region Tasks & Methods

    public override async void Appearing()
    {
        await Refresh();
    }

    [RelayCommand]
    private async Task Refresh()
    {
        PayingGuests.Clear();
        var guests = await GetPayingGuests();
        if (guests is not null && guests.Any())
        {
            foreach (var guest in guests)
            {
                PayingGuests.Add(guest);
            }
        }
    }

    private async Task<IEnumerable<PayingGuest>> GetPayingGuests()
    {
        try
        {
            var guests = await dataService.GetAllGuests();
            if (guests is null || !guests.Any())
            {
                await ShowToast(AppResource.TMNoGuestFound);
            }
            return guests;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return Enumerable.Empty<PayingGuest>();
        }
    }

    private IEnumerable<string> GetMonths()
    {
        return EnumHelper.GetValuesStringList<Month>();
    }

    [RelayCommand(CanExecute = nameof(IsNotBusy))]
    private async Task AddElectricBillAsync()
    {
        try
        {
            if (IsBusy) return;
            IsBusy = true;

            await Task.Delay(5000);

            //PayingGuest payingGuest = new()
            //{
            //    GuestAddress = GuestAddress,
            //    GuestEmail = GuestEmail,
            //    GuestPhone = GuestPhone,
            //    GuestName = GuestName,
            //    GuestVerifyDoc = GuestVerifyDoc,
            //    GuestVerifyDocType = GuestVerifyDocType,
            //    Created = DateTime.Now,
            //    Updated = DateTime.Now,
            //};

            //var response = await data.AddGuest(payingGuest);
            if (true)
            {
                await ShowToast(AppResource.TMGuestSavedPassed);
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

    #endregion Tasks & Methods
}