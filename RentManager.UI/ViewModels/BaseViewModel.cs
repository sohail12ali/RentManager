namespace RentManager.UI.ViewModels;

using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

using System.Threading;

public partial class BaseViewModel : ObservableObject
{
    #region Fields & Properties

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;

    public bool IsNotBusy => !IsBusy;

    #endregion Fields & Properties

    #region Tasks & Methods

    public BaseViewModel()
    {
    }

    protected async Task ShowToast(string text, ToastDuration duration = ToastDuration.Short, double fontSize = 14, CancellationTokenSource? cancellationTokenSource = null)
    {
        Debug.WriteLine($"Toast Message Posted: {text}");
        var toast = Toast.Make(text, duration, fontSize);
        await toast.Show(cancellationTokenSource?.Token ?? default);
    }

    #endregion Tasks & Methods
}