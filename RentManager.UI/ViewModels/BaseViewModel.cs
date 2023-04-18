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

    #region Constructor

    public BaseViewModel()
    {
    }

    #endregion Constructor

    #region Tasks & Methods

    [RelayCommand]
    public virtual void Appearing()
    {
        try
        {
            // DoSomething
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }
    }

    [RelayCommand]
    public virtual void Disappearing()
    {
        try
        {
            // DoSomething
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }
    }

    protected async Task ShowToast(string text, ToastDuration duration = ToastDuration.Short, double fontSize = 14, CancellationTokenSource? cancellationTokenSource = null)
    {
        Debug.WriteLine($"Toast Message Posted: {text}");
        var toast = Toast.Make(text, duration, fontSize);
        await toast.Show(cancellationTokenSource?.Token ?? default);
    }

    protected Task ShowToastOnMainThread(string text, ToastDuration duration = ToastDuration.Short, double fontSize = 14, CancellationTokenSource? cancellationTokenSource = null)
    {
        Debug.WriteLine($"Toast Message Posted: {text}");
        var toast = Toast.Make(text, duration, fontSize);
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await toast.Show(cancellationTokenSource?.Token ?? default);
        });

        return Task.CompletedTask;
    }

    #endregion Tasks & Methods
}