using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reactive.Linq;
using Akavache;
using Newtonsoft.Json;
using ProjectTryout.Model;
using ProjectTryout.Model.HttpModule;
using ProjectTryout.Model.Services;
using ProjectTryout.Model.Utils;
using ProjectTryout.ViewModelx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectTryout.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardsExhibitPage : ContentPage
    {
        public CardsExhibitPage()
        {
            InitializeComponent();
            BindingContext = new CardsExhibitViewModel(new PageService());
        }

        protected override async void OnAppearing()
        {
            ObservableCollection<Card> oldCards = null;
            // Means this is the first start
            if (MyCardsExhibitViewModel.Cards.Count == 0)
            {
                try
                {
                    oldCards = await BlobCache.LocalMachine.GetObject<ObservableCollection<Card>>("Cards");
                }
                catch (KeyNotFoundException key)
                {
                    // do nothing
                }

                if (oldCards != null)
                {
                    foreach (Card c in oldCards)
                    {
                        MyCardsExhibitViewModel.Cards.Add(c);
                    }
                }
            }
            // Refresh grid
            await MyCardsExhibitViewModel.RefreshCardsView();
        }
        
        protected override async void OnDisappearing()
        {
            await BlobCache.LocalMachine.InsertObject("Cards", MyCardsExhibitViewModel.Cards);
        }

        public CardsExhibitViewModel MyCardsExhibitViewModel
        {
            get { return BindingContext as CardsExhibitViewModel; }
            set { BindingContext = value; }
        }
    }
}
