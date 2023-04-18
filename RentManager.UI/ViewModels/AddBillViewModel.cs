using RentManager.Common.Enums;
using RentManager.DataAccess.DataServices;
using System.Collections.ObjectModel;
using static RentManager.Common.Helpers.EnumHelper;

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
    [NotifyPropertyChangedFor(nameof(TotalPayableAmount))]
    private string pricePerUnit;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalPayableUnits))]
    [NotifyPropertyChangedFor(nameof(TotalPayableAmount))]
    private string lastUnit;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalPayableUnits))]
    [NotifyPropertyChangedFor(nameof(TotalPayableAmount))]
    private string currentUnit;

    public string TotalPayableUnits => PayableUnits(out _, out _);
    public string TotalPayableAmount => PayableAmount();

    #endregion Fields & Properties

    #region Constructor

    public AddBillViewModel(IDataService dataService)
    {
        Title = AppResource.PTAddBill;
        this.dataService = dataService;
        Months.AddRange(GetMonths());
    }

    #endregion Constructor

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

    private IEnumerable<string> GetMonths() => GetValuesStringList<Month>();

    [RelayCommand(CanExecute = nameof(IsNotBusy))]
    private async Task AddElectricBillAsync()
    {
        try
        {
            if (IsBusy) return;
            IsBusy = true;
            bool isValid = await IsValid(out float lUnit, out float cUnit, out float price);
            if (!isValid) return;

            ElectricityBill bill = new()
            {
                BillForMonth = (int)SelectedMonth.ParseEnum<Month>(),
                GuestId = SelectedGuest.GuestId,
                BillStartDate = this.BillStartDate,
                BillEndDate = this.BillEndDate,
                PricePerUnit = price,
                CurrentUnit = (int)cUnit,
                LastUnit = (int)lUnit,
                BilledUnits = int.Parse(TotalPayableUnits),
                TotalPayableAmount = int.Parse(TotalPayableAmount)
            };

            var response = await dataService.AddElectricityBill(bill);
            if (response)
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

    private string PayableUnits(out float cUnits, out float lUnits)
    {
        if (float.TryParse(CurrentUnit?.Trim(), out cUnits)
            && float.TryParse(LastUnit?.Trim(), out lUnits)
            && cUnits >= lUnits)
        {
            return $"{cUnits - lUnits:00}";
        }
        cUnits = 0;
        lUnits = 0;
        return string.Empty;
    }

    private string PayableAmount()
    {
        if (!string.IsNullOrEmpty(PayableUnits(out float cUnits, out float lUnits))
            && float.TryParse(PricePerUnit?.Trim(), out float price))
        {
            return $"{(cUnits - lUnits) * price:00}";
        }
        return string.Empty;
    }

    private Task<bool> IsValid(out float lUnit, out float cUnit, out float price)
    {
        bool isValid = true;
        lUnit = 0;
        cUnit = 0;
        price = 0;

        if (SelectedGuest is null)
        {
            ShowToastOnMainThread(AppResource.LTSelectGuest);
            isValid = false;
        }
        else if (SelectedMonth is null)
        {
            ShowToastOnMainThread(AppResource.LTSelectMonth);
            isValid = false;
        }
        else if (string.IsNullOrWhiteSpace(LastUnit)
            || !float.TryParse(LastUnit?.Trim(), out lUnit))
        {
            ShowToastOnMainThread(AppResource.LTLastBilled);
            isValid = false;
        }
        else if (string.IsNullOrWhiteSpace(CurrentUnit)
           || !float.TryParse(CurrentUnit?.Trim(), out cUnit))
        {
            ShowToastOnMainThread(AppResource.LTCurrentUnits);
            isValid = false;
        }
        else if (string.IsNullOrWhiteSpace(PricePerUnit)
            || !float.TryParse(PricePerUnit?.Trim(), out price))
        {
            ShowToastOnMainThread(AppResource.LTPricePerUnit);
            isValid = false;
        }
        return Task.FromResult(isValid);
    }

    #endregion Tasks & Methods
}