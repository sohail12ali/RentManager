﻿using RentManager.DataAccess.DataServices;

using System.Collections.ObjectModel;

namespace RentManager.UI.ViewModels;

public partial class ViewGuestViewModel : BaseViewModel
{
    #region Field & Properties
    private readonly IDataService dataService;

    [ObservableProperty]
    private ObservableCollection<PayingGuest> payingGuests = new();

    [ObservableProperty]
    private bool isRefreshing;

    #endregion Field & Properties

    #region Constructor

    public ViewGuestViewModel(IDataService dataService)
    {
        Debug.WriteLine($"Init Class: {GetType().Name}, Method: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
        Title = AppResource.PTViewGuest;
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