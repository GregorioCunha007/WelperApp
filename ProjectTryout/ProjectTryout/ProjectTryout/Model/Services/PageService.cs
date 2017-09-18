using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTryout.Views;
using Xamarin.Forms;

namespace ProjectTryout.Model.Services
{
    public class PageService : IAppNavigation
    {
        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, ok, cancel);
        }

        public async Task PushAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public void InsertBefore(Page nextPage)
        {
            Page p = Application.Current.MainPage.Navigation.NavigationStack.Last();
            Application.Current.MainPage.Navigation.InsertPageBefore(nextPage,p);
        }

        public async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync().ConfigureAwait(false);
        }

        public async Task GoToCardExhibitPage()
        {
            InsertBefore(new CardsExhibitPage());
            await PopAsync();
        }

        public async Task GoToFormPage()
        {
            InsertBefore(new FormPage());
            await PopAsync();
        }

        public async Task ChoosePageToNavigate(IAction action)
        {
            switch (action.Name)
            {
                case "to_text":
                    await PushAsync(new ToTextPage(action));
                    break;
                case "medical_information":
                    await PushAsync(new MedicalHistoryPage(action));
                    break;
                default:
                    break;
            }
        }
    }
}
