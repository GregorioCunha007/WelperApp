using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using FFImageLoading;
using Newtonsoft.Json;
using Plugin.Connectivity;
using ProjectTryout.Model;
using ProjectTryout.Model.Action_Types;
using ProjectTryout.Model.DataRepresentation;
using ProjectTryout.Model.Exceptions;
using Xamarin.Forms;
using ProjectTryout.Model.HttpModule;
using ProjectTryout.Model.Services;
using ProjectTryout.Model.Utils;
using ProjectTryout.Views;
using Xamarin.Forms.PlatformConfiguration;

namespace ProjectTryout.ViewModelx
{
    public class CardsExhibitViewModel : BaseViewModel
    {
        /* Services data */
        private readonly IAppNavigation _navigation;

        /* XAML properties */
        public ObservableCollection<Card> Cards { get; set; } = new ObservableCollection<Card>();

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                SetValue(ref _isLoading, value);
            }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetValue(ref _isRefreshing, value); }
        }

        public CardsExhibitViewModel(IAppNavigation navigation) : base(navigation)
        {
            ExecuteActionCommand = new Command<Card>(ExecuteAction);
            RefreshViewCommand = new Command(async () =>
            {
                IsRefreshing = true;
                await RefreshCardsView();
                IsRefreshing = false;
            });
            _navigation = navigation;
        }

        public ICommand ExecuteActionCommand { get; private set; }
        public ICommand RefreshViewCommand { get; private set; }
        public ICommand OpenUserChangeFormCommand { get; private set; }
     
        private async void ExecuteAction(Card card)
        {
            try
            {
                IsLoading = true;
                ActionResult result = await card.Action.ExecuteAction();
                ActionRepresentation.Representate(this,card.Action,result);
                IsLoading = false;
            }
            catch (GpsDisabledException exception)
            {
                Debug.WriteLine(exception.Message);
                var answer = await _navigation.DisplayAlert("Gps not enabled", "Please turn on your Gps service", "Ok", "No");
                if (answer)
                {
                    IEnableLocationService locationService = DependencyService.Get<IEnableLocationService>();
                    locationService.EnableLocationService();
                    MessagingCenter.Subscribe<App, bool>(this, Events.WaitForGps, ((app, b) =>
                    {
                        MessagingCenter.Unsubscribe<App, bool>(this, Events.WaitForGps);
                        ExecuteAction(card);
                    }));                
                }
                IsLoading = false;
            }
            catch (Exception exception)
            {
                IsLoading = false;
                BuildToast(false,"Could not perform action");
                Debug.WriteLine(exception.Message);
            }    
        }

        public async Task RefreshCardsView()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                BuildToast(false,"No internet connection");
                return;
            }

            if (Cards == null)
            {
                Cards = new ObservableCollection<Card>();
            }

            try
            {
                JsonResult cards = (JsonResult) await HttpMiddleman.GetCards();
                RemoveCardsFromView(cards.Data.DeletedCards);
                AddCardsToView(cards.Data.AddedCards);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                BuildToast(false,"Could not refresh");
            }               
        }

        private void AddCardsToView(List<Card> cards)
        {
            foreach (Card c in cards)
            {
                if (!ContainsInCards(c))
                {
                    Cards.Add(c);
                }
                else
                {
                    // Remove the card with the same Id, and replace him by the new one
                    RemoveFromCards(c);
                    Cards.Add(c);
                }
            }
        }

        private void RemoveCardsFromView(List<Archive> cards)
        {
            foreach (Archive a in cards)
            {
                Card c = new Card { Id = a.CardId };
                RemoveFromCards(c);
            }
        }

        private bool ContainsInCards(Card card)
        {
            foreach (Card c in Cards)
            {
                if (c.Id == card.Id)
                {
                    return true;
                }
            }
            return false;
        }

        private void RemoveFromCards(Card card)
        {
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                if (Cards[i].Id == card.Id)
                {
                    Cards.RemoveAt(i);
                }
            }
        }
    }
}
