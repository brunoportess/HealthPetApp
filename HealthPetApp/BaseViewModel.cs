using CommunityToolkit.Mvvm.Input;
using HealthPetApp.Models;
using HealthPetApp.Sections.Popups.UIComponents;
using HealthPetApp.Sections.Popups.UICOmponents;
using HealthPetApp.Services;
using HealthPetApp.Services.Navigation;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HealthPetApp
{
    abstract class BaseViewModel : INotifyPropertyChanged
    {
        internal protected static NavigationService Navigation => NavigationService.Current;
        internal protected static DialogService Dialog => DialogService.Current;

        protected static void CommandOnException(Exception ex)
            => MainThread.BeginInvokeOnMainThread(async () => await BaseViewModel.Dialog.DisplayAlert(ex));

        bool _IsBusy = false;
        public bool IsBusy
        {
            get => _IsBusy;
            set => SetProperty(
                backingStore: ref _IsBusy,
                value: value,
                onChanged: () => OnPropertyChanged(nameof(IsNotBusy)));
        }
        public bool IsNotBusy => !_IsBusy;

        string _Title = string.Empty;
        public string Title
        {
            get => _Title;
            set => SetProperty(
                backingStore: ref _Title,
                value: value);
        }

        #region [ GoBackCommand ]

        AsyncRelayCommand? _GoBackCommand;

        public AsyncRelayCommand GoBackCommand => _GoBackCommand
            ??= new AsyncRelayCommand(execute: GoBackCommandExecute, canExecute: GoBackCommandCanExecute);

        async internal Task GoBackCommandExecute() => await NavigationService.Current.GoBack();

        bool GoBackCommandCanExecute() => true;

        #endregion

        #region [ SetProperty ]

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "",
            Action? onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value)) return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

        #region [ INotifyPropertyChanged ]

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region [ AlertPopups ]
        public static async Task ReturnAlertMessage(string titleMessage, string message, ImagensAlerta imageMessage)
            => await MopupService.Instance.PushAsync(new AlertPopupPage(titleMessage, message, imageMessage));
        #endregion

        #region [ LoadingPopup ]
        public static async Task Loading() => await MopupService.Instance.PushAsync(new BusyPopupPage());

        public static async Task RemoveAllPopupLoad()
        {
            var popupsLoad = MopupService.Instance.PopupStack
                .ToList()
                .Where(lbda => lbda.GetType() == typeof(BusyPopupPage));

            //if (popupsLoad is null) throw new RGPopupStackInvalidException("Não há popups de load carregados!.");

            foreach (var pagina in popupsLoad)
                await MopupService.Instance.RemovePageAsync(pagina);
        }
        #endregion

        public virtual Task Initialize(object? args = null) => Task.CompletedTask;
    }
}
