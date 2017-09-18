using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Plugin.Toasts;
using ProjectTryout.Model;
using ProjectTryout.Model.DataRepresentation;
using ProjectTryout.Model.HttpModule;
using ProjectTryout.Model.Services;
using ProjectTryout.Model.Utils;
using Xamarin.Forms;

namespace ProjectTryout.ViewModelx
{
    public class BaseViewModel : INotifyPropertyChanged, IRepresentation
    {
        private readonly IAppNavigation _navigation;

        private bool _isLoading;

        protected readonly HttpMiddleman HttpMiddleman;

        public event PropertyChangedEventHandler PropertyChanged;


        public BaseViewModel()
        {
            HttpMiddleman = new HttpMiddleman(new HttpConsumer());
        }

        public BaseViewModel(IAppNavigation navigation)
        {
            _navigation = navigation;
            HttpMiddleman = new HttpMiddleman(new HttpConsumer());
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return;

            backingField = value;
            OnPropertyChanged(propertyName);
        }

        protected async void BuildToast(bool success, string message)
        {
            var title = string.Empty;

            title = success ? "Success" : "Error";

            var notificator = DependencyService.Get<IToastNotificator>();
            await notificator.Notify(success ? ToastNotificationType.Success : ToastNotificationType.Error,
                title,
                message,
                TimeSpan.FromSeconds(3));
        }

        public void BuildNotification(ActionResult actionResult)
        {
            if (actionResult.Notify)
            {
                BuildToast(actionResult.Succeed, actionResult.Message);
            }
        }

        public async void BuildView(IAction action)
        {
            await _navigation.ChoosePageToNavigate(action);
        }
    }
}
