using RentManager.DataAccess.DataServices;
using System.Collections.ObjectModel;

namespace RentManager.UI.ViewModels;

public partial class ViewBillsViewModel : BaseViewModel
{
    #region Field & Properties
    private readonly IDataService dataService;

    [ObservableProperty]
    private ObservableCollection<EBill> electricityBills = new();

    [ObservableProperty]
    private bool isRefreshing;

    #endregion Field & Properties

    #region Constructor

    public ViewBillsViewModel(IDataService dataService)
    {
        Debug.WriteLine($"Init Class: {GetType().Name}, Method: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
        Title = AppResource.PTViewElectricBill;
        this.dataService = dataService;
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
        ElectricityBills.Clear();
        var billsTask = GetElectricityBills();
        var guestsTask = GetPayingGuests();

        Task[] tasks = new Task[] { billsTask, guestsTask };

        await Task.WhenAll(tasks);

        var bills = billsTask.Result;
        var guests = guestsTask.Result;

        if (bills is not null && bills.Any())
        {
            foreach (var bill in bills)
            {
                ElectricityBills.Add(new EBill(bill, guests));
            }
        }
    }

    private async Task<IEnumerable<ElectricityBill>> GetElectricityBills()
    {
        try
        {
            var bills = await dataService.GetAllElectricityBills();
            return bills;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return Enumerable.Empty<ElectricityBill>();
        }
    }

    private async Task<IEnumerable<PayingGuest>> GetPayingGuests()
    {
        try
        {
            var guests = await dataService.GetAllGuests();
            return guests;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return Enumerable.Empty<PayingGuest>();
        }
    }

    #endregion Tasks & Methods
}